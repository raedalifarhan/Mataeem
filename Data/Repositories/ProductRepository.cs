using AutoMapper;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.Interfaces;
using Mataeem.Lib;
using Microsoft.EntityFrameworkCore;

namespace Mataeem.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly string _pictureBase;

        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _pictureBase = "products-images";
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = _context.Products
                .Include(x => x.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Rate = p.Rate,
                    PictureUrl = p.PictureUrl ?? string.Empty,
                    RegularPrice = p.RegularPrice,
                    SellingPrice = p.SellingPrice,
                    CategoryName = p.Category!.CategoryName,
                    Options = _context.Products.Where(x => x.ParentId == p.Id).Select(pr => new OptionDto
                    {
                        Id = pr.Id,
                        ProductName = pr.ProductName,
                        Type = pr.OptionType,
                        IsMandatory = pr.IsMandatory,
                        MandatoryCount = 3,
                        Values = _context.Products.Where(x => x.ParentId == pr.Id).Select(op => new OptionValueDto
                        {
                            Id = op.Id,
                            ProductName = op.ProductName,
                            RegularPrice = op.RegularPrice,
                            SellingPrice = op.SellingPrice,
                        })
                        .ToList() ?? new List<OptionValueDto>()
                    })
                    .ToList() ?? new List<OptionDto>()
                })
            .AsQueryable();

            return await products.ToListAsync();
        }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var product = _context.Products
                .Include(x => x.Category)
                .Where(r => r.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Rate = p.Rate,
                    PictureUrl = p.PictureUrl ?? string.Empty,
                    RegularPrice = p.RegularPrice,
                    SellingPrice = p.SellingPrice,
                    CategoryName = p.Category!.CategoryName,
                    Options = _context.Products.Where(x => x.ParentId == p.Id).Select(pr => new OptionDto
                    {
                        Id = pr.Id,
                        ProductName = pr.ProductName,
                        Type = pr.OptionType,
                        IsMandatory = pr.IsMandatory,
                        MandatoryCount = 3,
                        Values = _context.Products.Where(x => x.ParentId == pr.Id).Select(op => new OptionValueDto
                        {
                            Id = op.Id,
                            ProductName = op.ProductName,
                            RegularPrice = op.RegularPrice,
                            SellingPrice = op.SellingPrice,
                        }).ToList() ?? new List<OptionValueDto>()
                    }).ToList() ?? new List<OptionDto>()
                })
            .FirstOrDefault();

            return product ?? null!;
        }

        public async Task<bool> CreateProduct(Guid categoryId, Guid cuisineId, ProductSaveDto model)
        {
            var productId = await ProductHelper.CreateNewProduct(model, categoryId, cuisineId, _context, _pictureBase);

            if (productId == Guid.Empty) return false;

            if (model.Options != null)
            {
                foreach (var options in model.Options)
                {
                    var optionId = await ProductHelper.CreateNewOption(options, productId, _context);

                    if (options.Values != null)
                    {
                        foreach (var vals in options.Values)
                        {
                            await ProductHelper.CreateNewValue(_context, vals, optionId);
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }

            return true;
        }

        /*
        * get all product ids.
        * loop on options
        * if without id ==> new option
        * if id ==> update option info and remove optionId from 'allChildIds'
        * repeat last two steps for values too.
        * last step is loop on remains ids and remove product (removed options or values)
        */

        public async Task<bool> UpdateProduct(ProductSaveDto model)
        {
            if (model == null) return false;

            var product = await _context.Products
                .Include(x => x.Products)!
                .ThenInclude(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == model.Id);


            if (product == null) return false;
            try
            {
                IList<Guid> allChildIds = product!.GetAllChildIds();

                // Update Parent
                _mapper.Map(model, product);

                if (!string.IsNullOrEmpty(model.PictureBase64) && !string.IsNullOrEmpty(model.PictureExtension))
                {
                    try
                    {
                        product.PictureUrl = await ProductHelper.ConvertBase64ToIFormFileThenSave(
                            model.PictureBase64, model.PictureExtension, _pictureBase, model.ProductName, product.PictureUrl!);

                        if (!string.IsNullOrEmpty(product.PictureUrl ))
                        {
                            return false;
                        }
                    }
                    catch (Exception) {
                        return false;
                    }
                }

                if (model.Options != null && model.Options.Any())
                {
                    foreach (var option in model.Options)
                    {
                        // if exists in origin or new.
                        if (option.Id == Guid.Empty) // Create New Option with his childs.
                        {
                            var optionId = await ProductHelper.CreateNewOption(option, model.Id, _context);

                            if (option.Values != null && option.Values.Any())
                            {
                                option.Values?.ForEach(async vals =>
                                {
                                    await ProductHelper.CreateNewValue(_context, vals, optionId);
                                });

                                await _context.SaveChangesAsync();
                            }
                        }
                        else // update exists option.
                        { 
                            var ProductOption = await _context.Products.FindAsync(option.Id);

                            _mapper.Map(option, ProductOption);

                            allChildIds.Remove(option.Id);

                            if (option.Values != null && option.Values.Any())
                            {
                                foreach (var vals in option.Values)
                                {
                                    if (vals.Id == Guid.Empty) // create new value.
                                    {
                                        await ProductHelper.CreateNewValue(_context, vals, option.Id);
                                    } else // update exist value.
                                    {
                                        var ProductOptionValue = await _context.Products.FindAsync(vals.Id);
                                        _mapper.Map(vals, ProductOptionValue);

                                        allChildIds.Remove(vals.Id);
                                    }
                                }
                            }
                        }
                    }
                    // Delete romoved option and values
                    foreach (var id in allChildIds)
                    {
                        await DeleteProduct(id);
                    }
                    await _context.SaveChangesAsync(); // Save changes after all updates are complete
                }

            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            if (id == Guid.Empty) return false;

            var product = await _context.Products
                .Include(x => x.Products)!
                .ThenInclude(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null) return false;

            IEnumerable<Guid> allChildIds = product!.GetAllChildIds();

            await _context.Products
                .Where(x => allChildIds.Contains(x.Id) || x.Id == id)
                .ExecuteDeleteAsync();

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
    }
}

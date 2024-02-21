using AutoMapper;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.Interfaces;
using Mataeem.Lib;
using Mataeem.Models;
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

        public async Task<List<ProductListDto>> GetAllProducts()
        {
            var query = await _context.Products
                .ToListAsync();

            return _mapper.Map<List<ProductListDto>>(query);
        }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            var product = _context.Products
                .Where(r => r.Id == id)
                .Select(p => new ProductDto
                {
                    ProductName = p.ProductName,
                    Rate = p.Rate,
                    PictureUrl = p.PictureUrl ?? string.Empty,
                    Options = _context.Products.Where(x => x.ParentId == p.Id).Select(pr => new OptionDto
                    {
                        ProductName = pr.ProductName,
                        Type = pr.OptionType,
                        IsMandatory = pr.IsMandatory,
                        MandatoryCount = 3,
                        Values = _context.Products.Where(x => x.ParentId == pr.Id).Select(op => new OptionValueDto
                        {
                            ProductName = op.ProductName,
                            RegularPrice = op.RegularPrice,
                            SellingPrice = op.SellingPrice,
                        }).ToList() ?? new List<OptionValueDto>()
                    }).ToList() ?? new List<OptionDto>()
                })
            .FirstOrDefault();

            return product ?? null!;
        }

        public async Task<bool> CreateProduct(Guid categoryId, ProductSaveDto model)
        {
            var productId = await ProductHelper.ConstructNewProduct(model, categoryId, _context, _pictureBase);

            if (productId == Guid.Empty) return false;

            model.Options?.ForEach(async options =>
            {
                var optionId = await ProductHelper.ConstructNewOption(options, productId, _context);

                options.Values?.ForEach(vals =>
                {
                    var value = new Product
                    {
                        ProductName = vals.ProductName,
                        RegularPrice = vals.RegularPrice,
                        SellingPrice = vals.SellingPrice,
                        ParentId = optionId,
                    };
                    _context.Products.Add(value);
                });

                await _context.SaveChangesAsync();
            });

            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            if (id == Guid.Empty) return false;

            var product = await _context.Products.FindAsync(id);

            if (product == null) return false;

            IEnumerable<Guid> allChildIds = product!.GetAllChildIds();

            await _context.Products
                .Where(x => allChildIds.Contains(x.Id))
                .ExecuteDeleteAsync() ;

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return false;

            return true;
        }
    }
}

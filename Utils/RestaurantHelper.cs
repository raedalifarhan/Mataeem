using Mataeem.Data;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.Models;

namespace Mataeem.Lib
{
    public class ProductHelper
    {
        public static async Task<Guid> ConstructNewOption(OptionDto options, Guid productId, DataContext context)
        {
            var option = new Product
            {
                ProductName = options.ProductName,
                OptionType = options.Type,
                IsMandatory = options.IsMandatory,
                MandatoryCount = options.MandatoryCount,
                ParentId = productId,
            };

            context.Products.Add(option);
            var result = await context.SaveChangesAsync() > 0;

            return result ? option.Id : Guid.Empty;
        }

        public static async Task<Guid> ConstructNewProduct(ProductSaveDto model, Guid categoryId, DataContext context, string folderName)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                RegularPrice = model.RegularPrice,
                SellingPrice = model.SellingPrice,
                CreateDate = DateTime.Now,
                CategoryId = categoryId,
            };

            if (model.FormFile != null)
            {
                var fileName = await FileHelper.SaveFileToServer(model.FormFile, folderName, product.PictureUrl);

                if (fileName == null) return Guid.Empty;

                product.PictureUrl = fileName;
            }

            context.Products.Add(product);
            var result = await context.SaveChangesAsync() > 0;

            return result ? product.Id : Guid.Empty;
        }
    }
}

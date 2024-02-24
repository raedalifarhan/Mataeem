using Mataeem.Data;
using Mataeem.DTOs.ProductDTOs;
using Mataeem.Models;

namespace Mataeem.Lib
{
    public class ProductHelper
    {
        public static async Task<Guid> CreateNewOption(OptionDto options, Guid productId, DataContext context)
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

        public static async Task<Guid> CreateNewProduct(ProductSaveDto model, Guid categoryId, Guid cuisineId, DataContext context, string folderName)
        {
            var product = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                RegularPrice = model.RegularPrice,
                SellingPrice = model.SellingPrice,
                CreateDate = DateTime.Now,
                CategoryId = categoryId,
                CuisineId = cuisineId,
            };


            if (!string.IsNullOrEmpty(model.PictureBase64) && !string.IsNullOrEmpty(model.PictureExtension!))
            {
                try
                {
                    product.PictureUrl = await ConvertBase64ToIFormFileThenSave(
                        model.PictureBase64, model.PictureExtension, folderName, model.ProductName, product.PictureUrl!);

                    if (string.IsNullOrEmpty(product.PictureUrl)) 
                    {
                        return Guid.Empty;
                    }
                }
                catch (Exception) {
                    return Guid.Empty;
                }
            }

            context.Products.Add(product);
            var result = await context.SaveChangesAsync() > 0;

            return result ? product.Id : Guid.Empty;
        }

        public static async Task<string> ConvertBase64ToIFormFileThenSave(
            string base64, string extension, string folderName, string fileName, string pictureUrl)
        {
            var iFormFile = FileHelper.ConvertBase64ToIFormFile(base64!, extension!, fileName);

            return await FileHelper
                .SaveFileToServer(iFormFile, folderName, pictureUrl);
        }

        public static async Task<Guid> CreateNewValue(DataContext context, OptionValueDto vals, Guid optionId)
        {
            var value = new Product
            {
                ProductName = vals.ProductName,
                RegularPrice = vals.RegularPrice,
                SellingPrice = vals.SellingPrice,
                ParentId = optionId,
            };

            context.Products.Add(value);

            var result = await context.SaveChangesAsync() > 0;

            if (!result) return Guid.Empty;

            return value.Id;
        }
    }
}

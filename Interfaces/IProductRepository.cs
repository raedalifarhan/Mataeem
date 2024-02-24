using Mataeem.DTOs.ProductDTOs;

namespace Mataeem.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProducts();

        Task<ProductDto> GetProduct(Guid id);

        Task<bool> CreateProduct(Guid categoryId, Guid cuisineId, ProductSaveDto model);

        Task<bool> UpdateProduct(ProductSaveDto model);

        Task<bool> DeleteProduct(Guid id);

    }
}

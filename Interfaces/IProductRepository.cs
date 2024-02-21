using Mataeem.DTOs.ProductDTOs;

namespace Mataeem.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductListDto>> GetAllProducts();

        Task<ProductDto> GetProduct(Guid id);

        Task<bool> CreateProduct(Guid categoryId, ProductSaveDto model);

        Task<bool> DeleteProduct(Guid id);

    }
}

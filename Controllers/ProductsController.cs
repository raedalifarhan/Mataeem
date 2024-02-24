using Mataeem.DTOs.ProductDTOs;
using Mataeem.Interfaces;
using Mataeem.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            return Ok(await _productRepository.GetProduct(id));
        }

        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        [HttpPost("category/{categoryId}/cuisine/{cuisineId}")]
        public async Task<ActionResult> CreateProduct(Guid categoryId, Guid cuisineId, ProductSaveDto model)
        {
            var result = await _productRepository.CreateProduct(categoryId, cuisineId, model);

            if (!result) return BadRequest("An error accured during Save Product, " +
                "check model field and picture extencion.");

            return Ok("Created Successfully");
        }

        [HttpPut]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> UpdateProduct(ProductSaveDto model)
        {
            var result = await _productRepository.UpdateProduct(model);

            if (!result) return BadRequest("An error accured during Save Product, " +
                "check model field and picture extencion.");

            return Ok("Update Successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesNames.SUPERADMIN}, {RolesNames.IT}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {

            var result = await _productRepository.DeleteProduct(id);

            if (!result) return BadRequest("An error accured during dedctivate Product ");

            return Ok("deactivated Successfully!");
        }

    }
}
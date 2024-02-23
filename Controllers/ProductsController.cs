using Mataeem.DTOs.ProductDTOs;
using Mataeem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mataeem.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            return Ok(await _productRepository.GetProduct(id));
        }

        [HttpPost("{categoryId}")]
        public async Task<ActionResult> CreateProduct(Guid categoryId, ProductSaveDto model)
        {
            var result = await _productRepository.CreateProduct(categoryId, model);

            if (!result) return BadRequest("An error accured during Save Product, " +
                "check model field and picture extencion.");

            return Ok("Created Successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductSaveDto model)
        {
            var result = await _productRepository.UpdateProduct(model);

            if (!result) return BadRequest("An error accured during Save Product, " +
                "check model field and picture extencion.");

            return Ok("Update Successfully");
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateProduct(Guid id, ProductSaveDto model)
        //{

        //    var result = await _productRepository.UpdateProduct(id, model);

        //    if (!result) return BadRequest("An error accured during Save Product ");

        //    return Ok("Updated Successfully");
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {

            var result = await _productRepository.DeleteProduct(id);

            if (!result) return BadRequest("An error accured during dedctivate Product ");

            return Ok("deactivated Successfully!");
        }

    }
}
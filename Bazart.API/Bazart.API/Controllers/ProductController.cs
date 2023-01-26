using Bazart.Models.Dto.ProductDto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.API.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] string userId, [FromRoute] int productId)
        {
            var productById = await _productRepository.GetProductByIsAsync(userId, productId);
            return Ok(productById);
        }

        [HttpGet("{categoryName}")]
        public async Task<ActionResult<ProductDto>> GetProductsByCategory([FromRoute] string userId, [FromRoute] string categoryName)
        {
            var productsByCategory = await _productRepository.GetProductsByCategoryNameAsync(userId, categoryName);
            return Ok(productsByCategory);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromRoute] string userId, [FromBody] CreateProductDto createProductDto)
        {
            var createProductId = await _productRepository.CreateProductAsync(userId, createProductDto);
            return Created($"/api/user/{userId}/products/{createProductId}", null);
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] string userId, [FromRoute] int productId)
        {
            await _productRepository.DeleteProductAsync(userId, productId);

            return NoContent();
        }

        [HttpPut("{productId:int}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] string userId, [FromRoute] int productId, [FromBody] UpdateProductDto updateProductDto)
        {
            await _productRepository.UpdateProductAsync(userId, productId, updateProductDto);
            return Ok();
        }

        [HttpPatch("{productId:int}")]
        public async Task<ActionResult> UpdatePartialProduct([FromRoute] string userId, [FromRoute] int productId, [FromBody] JsonPatchDocument<UpdateProductDto> updatePartialProductDto)
        {
            await _productRepository.UpdatePartialProduct(userId, productId, updatePartialProductDto);

            return Ok();
        }
    }
}
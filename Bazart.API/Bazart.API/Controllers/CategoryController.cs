using Bazart.API.Repository.IRepository;
using Bazart.Models.Dto.CategoryDto;
using Bazart.Models.Dto.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCategoryWithProduct([FromRoute] string categoryName)
        {
            var categoryWithProducts = await _categoryRepository.GetCategoriesWithProductsByCategoryNameAsync(categoryName);
            return Ok(categoryWithProducts);
        }
    }
}
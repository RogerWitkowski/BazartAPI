using System.Collections;
using AutoMapper;
using Bazart.DataAccess.DataAccess;
using Bazart.Models.Dto.CategoryDto;
using Bazart.Models.Dto.ProductDto;
using Bazart.Repository.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bazart.Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BazartDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(BazartDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IQueryable> GetAllCategoriesAsync()
        {
            var categories = await _dbContext
                .Categories
                .ToListAsync();

            var categoriesByUniqueName = Task.FromResult(categories
                .DistinctBy(c => c.Name)
                .Take(6)
                .OrderBy(c => c.Name)).Result;

            var categoriesByUniqueNameDto = _mapper.Map<List<CategoryDto>>(categoriesByUniqueName).AsQueryable();

            return categoriesByUniqueNameDto;
        }

        public async Task<IQueryable> GetCategoriesWithProductsByCategoryNameAsync(string categoryName)
        {
            var productsByCategory = await _dbContext
                .Products
                .Include(c => c.Category)
                .Include(u => u.CreatedBy)
                .Where(c => c.Category.Name.ToLower() == categoryName.ToLower())
                .ToListAsync();

            var productsDtoByCategory = _mapper.Map<List<ProductDto>>(productsByCategory).AsQueryable();

            return productsDtoByCategory;
        }
    }
}
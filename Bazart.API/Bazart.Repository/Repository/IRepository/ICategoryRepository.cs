using System.Collections;

namespace Bazart.Repository.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<IQueryable> GetAllCategoriesAsync();

        public Task<IQueryable> GetCategoriesWithProductsByCategoryNameAsync(string categoryName);
    }
}
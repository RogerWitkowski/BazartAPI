using System.Collections;

namespace Bazart.Repository.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable> GetAllCategoriesAsync();

        public Task<IEnumerable> GetCategoriesWithProductsByCategoryNameAsync(string categoryName);
    }
}
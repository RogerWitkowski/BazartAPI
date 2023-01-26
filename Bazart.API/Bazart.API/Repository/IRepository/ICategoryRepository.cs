using System.Collections;

namespace Bazart.API.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable> GetAllCategoriesAsync();

        public Task<IEnumerable> GetCategoriesWithProductsByCategoryNameAsync(string categoryName);
    }
}
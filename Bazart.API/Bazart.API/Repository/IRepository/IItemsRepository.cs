using System.Collections;

namespace Bazart.API.Repository.IRepository
{
    public interface IItemsRepository
    {
        public Task<IEnumerable> GetAllProductsAsync();

        public Task<IEnumerable> GetAllEventsAsync();

        public Task<IEnumerable> Get5LatestProductsAsync();

        public Task<IEnumerable> Get5LatestEventsAsync();
    }
}
using System.Collections;
using Bazart.Models.Dto.EventDto;
using Bazart.Models.Dto.ProductDto;

namespace Bazart.Repository.Repository.IRepository
{
    public interface IItemsRepository
    {
        public Task<EventDto> GetEventByIdAsync(int eventId);

        public Task<IEnumerable> GetAllProductsAsync();

        public Task<IEnumerable> GetAllEventsAsync();

        public Task<IEnumerable> Get5LatestProductsAsync();

        public Task<IEnumerable> Get5LatestEventsAsync();
    }
}
using Bazart.API.Repository.IRepository;
using Bazart.Models.Dto.EventDto;
using Bazart.Models.Dto.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.API.Controllers
{
    namespace Bazart.API.Controllers
    {
        [ApiController]
        [Route("api/items")]
        public class ItemsController : ControllerBase
        {
            private readonly IItemsRepository _itemsRepository;

            public ItemsController(IItemsRepository itemsRepository)
            {
                _itemsRepository = itemsRepository;
            }

            [HttpGet("all-products")]
            public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
            {
                var products = await _itemsRepository.GetAllProductsAsync();
                return Ok(products);
            }

            [HttpGet("all-events")]
            public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
            {
                var events = await _itemsRepository.GetAllEventsAsync();
                return Ok(events);
            }

            [HttpGet("latest-products")]
            public async Task<ActionResult<IEnumerable<ProductDto>>> GetLatest5Products()
            {
                var latestProducts = await _itemsRepository.Get5LatestProductsAsync();
                return Ok(latestProducts);
            }

            [HttpGet("latest-events")]
            public async Task<ActionResult<IEnumerable<EventDto>>> GetLatest5Events()
            {
                var latestEvents = await _itemsRepository.Get5LatestEventsAsync();
                return Ok(latestEvents);
            }
        }
    }
}
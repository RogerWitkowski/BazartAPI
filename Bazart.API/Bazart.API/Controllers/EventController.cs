using Bazart.API.Repository.IRepository;
using Bazart.Models.Dto.EventDto;
using Microsoft.AspNetCore.Mvc;

namespace Bazart.API.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet("{eventId:int}")]
        public async Task<ActionResult<EventDto>> GetEventById([FromRoute] string userId, [FromRoute] int eventId)
        {
            var eventById = await _eventRepository.GetEventIdByUserIdAsync(userId, eventId);

            return Ok(eventById);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsByUserId([FromRoute] string userId)
        {
            var eventsByUser = await _eventRepository.GetEventsByUserIdAsync(userId);

            return Ok(eventsByUser);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEvent([FromRoute] string userId, [FromBody] CreateEventDto createEventDto)
        {
            var createEventId = await _eventRepository.CreateEventAsync(userId, createEventDto);
            return Created($"api/user/{userId}/events/{createEventId}", null);
        }

        [HttpDelete("{eventId:int}")]
        public async Task<ActionResult> DeleteEvent([FromRoute] string userId, [FromRoute] int eventId)
        {
            await _eventRepository.DeleteEventAsync(userId, eventId);
            return NoContent();
        }

        [HttpPut("{eventId:int}")]
        public async Task<ActionResult> UpdateEvent([FromRoute] string userId, [FromRoute] int eventId, [FromBody] UpdateEventDto updateEventDto)
        {
            await _eventRepository.UpdateEventAsync(userId, eventId, updateEventDto);
            return Ok();
        }
    }
}
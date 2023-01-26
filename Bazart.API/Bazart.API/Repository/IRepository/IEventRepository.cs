using System.Collections;
using Bazart.Models.Dto.EventDto;

namespace Bazart.API.Repository.IRepository
{
    public interface IEventRepository
    {
        public Task<EventDto> GetEventIdByUserIdAsync(string userId, int eventId);

        public Task<IEnumerable> GetEventsByUserIdAsync(string userId);

        public Task<string> CreateEventAsync(string userId, CreateEventDto createEventDto);

        public Task DeleteEventAsync(string userId, int eventId);

        public Task UpdateEventAsync(string userId, int eventId, UpdateEventDto updateEventDto);
    }
}
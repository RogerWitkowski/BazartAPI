using System.Collections;
using AutoMapper;
using Bazart.API.Exceptions;
using Bazart.API.Repository.IRepository;
using Bazart.DataAccess.DataAccess;
using Bazart.Models.Dto.EventDto;
using Bazart.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace Bazart.API.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly BazartDbContext _dbContext;
        private readonly IMapper _mapper;

        public EventRepository(BazartDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EventDto> GetEventIdByUserIdAsync(string userId, int eventId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new NotFoundException("User not found");
            }
            var events = await _dbContext
                .Events
                .Include(ed => ed.EventDetails)
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(e => e.Id == eventId && e.CreatedById == userId);
            if (events is null)
            {
                throw new NotFoundException("User not found");
            }

            var eventDto = _mapper.Map<EventDto>(events);

            return eventDto;
        }

        public async Task<IEnumerable> GetEventsByUserIdAsync(string userId)
        {
            var events = await _dbContext
                .Events
                .Include(ed => ed.EventDetails)
                .Include(u => u.CreatedBy)
                .Where(e => e.CreatedBy.Id == userId)
                .ToListAsync();
            var eventsDto = _mapper.Map<List<EventDto>>(events);

            if (!eventsDto.Any())
            {
                throw new NotFoundException("User not found");
            }
            return eventsDto;
        }

        public async Task<string> CreateEventAsync(string userId, CreateEventDto createEventDto)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var isCreated = _mapper.Map<Event>(createEventDto);
            isCreated.CreatedById = userId;

            await _dbContext.Events.AddAsync(isCreated);
            await _dbContext.SaveChangesAsync();
            return isCreated.Id.ToString();
        }

        public async Task DeleteEventAsync(string userId, int eventId)
        {
            var user = await _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            var eventToDelete = await _dbContext
                .Events
                .Include(ed => ed.EventDetails)
                .FirstOrDefaultAsync(e => e.CreatedById == user.Id && e.Id == eventId);

            if (eventToDelete == null)
            {
                throw new NotFoundException("Not found");
            }

            await Task.FromResult(_dbContext.Events.Remove(eventToDelete));
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(string userId, int eventId, UpdateEventDto updateEventDto)
        {
            var eventToUpdate = await _dbContext
                .Events
                .Include(ed => ed.EventDetails)
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(e => e.Id == eventId && e.CreatedById == userId);

            if (eventToUpdate == null)
            {
                throw new NotFoundException("Not found");
            }

            eventToUpdate.Name = updateEventDto.Name;
            eventToUpdate.Description = updateEventDto.Description;
            eventToUpdate.EventDetails.Country = updateEventDto.Country;
            eventToUpdate.EventDetails.City = updateEventDto.City;
            eventToUpdate.EventDetails.Street = updateEventDto.Street;
            eventToUpdate.EventDetails.HouseOrFlatNumber = updateEventDto.HouseOrFlatNumber;
            eventToUpdate.EventDetails.PostalCode = updateEventDto.PostalCode;
            eventToUpdate.EventDetails.ImageUrl = updateEventDto.ImageUrl;

            await _dbContext.SaveChangesAsync();
        }
    }
}
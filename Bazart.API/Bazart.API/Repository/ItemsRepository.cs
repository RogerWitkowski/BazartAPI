﻿using System.Collections;
using AutoMapper;
using Bazart.DataAccess.DataAccess;
using Bazart.Models.Dto.EventDto;
using Bazart.Models.Dto.ProductDto;
using Microsoft.EntityFrameworkCore;

namespace Bazart.API.Repository
{
    namespace Bazart.API.Repository
    {
        public class ItemsRepository : IRepository.IItemsRepository
        {
            private readonly BazartDbContext _dbContext;
            private readonly IMapper _mapper;

            public ItemsRepository(BazartDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<IEnumerable> GetAllProductsAsync()
            {
                var products = await _dbContext
                    .Products
                    .Include(c => c.Category)
                    .Include(u => u.CreatedBy)
                    .ToListAsync();

                var productsDto = _mapper.Map<List<ProductDto>>(products);

                return productsDto;
            }

            public async Task<IEnumerable> GetAllEventsAsync()
            {
                var events = await _dbContext
                    .Events
                    .Include(ed => ed.EventDetails)
                    .Include(u => u.CreatedBy)
                    .ToListAsync();

                var eventsDto = _mapper.Map<List<EventDto>>(events);

                return eventsDto;
            }

            public async Task<IEnumerable> Get5LatestProductsAsync()
            {
                var latestProducts = await _dbContext
                    .Products
                    .Include(c => c.Category)
                    .Include(u => u.CreatedBy)
                    .OrderByDescending(p => p.Id)
                    .Take(5)
                    .ToListAsync();
                var latestProductsDto = _mapper.Map<List<ProductDto>>(latestProducts);

                return latestProductsDto;
            }

            public async Task<IEnumerable> Get5LatestEventsAsync()
            {
                var latestEvents = await _dbContext
                    .Events
                    .Include(ed => ed.EventDetails)
                    .Include(u => u.CreatedBy)
                    .OrderByDescending(i => i.Id)
                    .Take(5)
                    .ToListAsync();

                var latestEventsDto = _mapper.Map<List<EventDto>>(latestEvents);

                return latestEventsDto;
            }
        }
    }
}
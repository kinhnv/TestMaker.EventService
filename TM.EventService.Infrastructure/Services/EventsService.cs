﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Business.Admin.Domain.Models;
using TestMaker.Business.Admin.Domain.Models.Candidate;
using TestMaker.Business.Admin.Domain.Models.Event;
using TestMaker.Business.Admin.Domain.Services;
using TM.EventService.Infrastructure.Attributes;
using TM.EventService.Infrastructure.Entities;
using TM.EventService.Infrastructure.Extensions;

namespace TM.EventService.Infrastructure.Services
{
    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EventsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #region Public
        public async Task<EventForDetails> CreateEventAsync(EventForCreating e)
        {
            var entity = _mapper.Map<Event>(e);
            entity.Code = CreateCode(8);
            _dbContext.Events.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await GetEventAsync(entity.EventId);
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var e = await _dbContext.Events.FindAsync(eventId);
            if (e != null)
            {
                _dbContext.Events.Remove(e);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditEventAsync(EventForEditing e)
        {
            var entity = await _dbContext.Events.FindAsync(e.EventId);

            entity.EventId = e.EventId;
            entity.Name = e.Name;
            entity.Type = e.Type;
            entity.TestId = e.TestId;

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<EventForDetails> GetEventAsync(Guid eventId)
        {
            var e = _dbContext.Events.SingleOrDefault(x => x.EventId == eventId);

            if (e == null)
                return null;

            return await Task.FromResult(_mapper.Map<EventForDetails>(e));
        }

        public async Task<IEnumerable<EventForList>> GetEventsAsync()
        {
            var events = _dbContext.Events.ToList().Select(e => _mapper.Map<EventForList>(e));
            return await Task.FromResult(events);
        }

        public async Task<bool> EventExistsAsync(Guid eventId)
        {
            return await _dbContext.Events.AnyAsync(e => e.EventId == eventId);
        }

        public async Task<IEnumerable<SelectOption>> GetEventTypeAsSelectOptionsAsync()
        {
            var result = new List<SelectOption>();
            foreach (EventType value in Enum.GetValues(typeof(EventType)))
            {
                result.Add(new SelectOption
                {
                    Title = value.GetName(),
                    Value = ((int)value).ToString(),
                });
            }

            return await Task.FromResult(result);
        }
        #endregion

        #region Private
        private string CreateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
        #endregion
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Event;
using TestMaker.EventService.Domain.Services;
using TestMaker.EventService.Infrastructure.Attributes;
using TestMaker.EventService.Infrastructure.Entities;
using TestMaker.EventService.Infrastructure.Extensions;
using TestMaker.EventService.Infrastructure.Repositories.Events;

namespace TestMaker.EventService.Infrastructure.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventsService(IEventsRepository eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        #region Private
        private string CreateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
        #endregion

        #region Public
        public async Task<ServiceResult<EventForDetails>> CreateEventAsync(EventForCreating e)
        {
            var entity = _mapper.Map<Event>(e);
            entity.Code = CreateCode(8);
            await _eventsRepository.CreateAsync(entity);

            return await GetEventAsync(entity.EventId);
        }

        public async Task<ServiceResult> DeleteEventAsync(Guid eventId)
        {
            var entity = await _eventsRepository.GetAsync(eventId);
            if (entity == null || entity.IsDeleted == true)
            {
                return new ServiceNotFoundResult<EventForDetails>(eventId);
            }
            entity.IsDeleted = true;
            await EditEventAsync(_mapper.Map<EventForEditing>(entity));
            return new ServiceResult();
        }

        public async Task<ServiceResult<EventForDetails>> EditEventAsync(EventForEditing e)
        {
            var entity = _mapper.Map<Event>(e);

            var result = await _eventsRepository.GetAsync(e.EventId);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<EventForDetails>(e.EventId);
            }

            await _eventsRepository.UpdateAsync(entity);
            return await GetEventAsync(e.EventId);
        }

        public async Task<ServiceResult<EventForDetails>> GetEventAsync(Guid eventId)
        {
            var e = await _eventsRepository.GetAsync(eventId);

            if (e == null)
                return new ServiceNotFoundResult<EventForDetails>(eventId);

            return await Task.FromResult(new ServiceResult<EventForDetails>(_mapper.Map<EventForDetails>(e)));
        }

        public async Task<ServiceResult<GetPaginationResult<EventForList>>> GetEventsAsync(GetEventsParams getEventsParams)
        {
            Expression<Func<Event, bool>> predicate = x => x.IsDeleted == getEventsParams.IsDeleted;

            var quetsions = (await _eventsRepository.GetAsync(predicate, getEventsParams.Skip, getEventsParams.Take))
                .Select(section => _mapper.Map<EventForList>(section));
            var count = await _eventsRepository.CountAsync(predicate);
            var result = new GetPaginationResult<EventForList>
            {
                Data = quetsions.ToList(),
                Page = getEventsParams.Page,
                Take = getEventsParams.Take,
                TotalPage = count
            };

            return new ServiceResult<GetPaginationResult<EventForList>>(result);
        }

        public async Task<ServiceResult<IEnumerable<SelectOption<int>>>> GetEventTypeAsSelectOptionsAsync()
        {
            var result = new List<SelectOption<int>>();
            foreach (EventType value in Enum.GetValues(typeof(EventType)))
            {
                result.Add(new SelectOption<int>
                {
                    Title = value.GetName(),
                    Value = (int)value,
                });
            }

            return new ServiceResult<IEnumerable<SelectOption<int>>>(await Task.FromResult(result));
        }

        public async Task<ServiceResult<PreparedData>> GetPreparedCandidateByCodeAsync(PrepareCode code)
        {
            var eventAndCandidate = await _eventsRepository.GetEventAndCandidateAsync(code.EventCode, code.CandidateCode);

            if (eventAndCandidate != null)
                return new ServiceResult<PreparedData>(new PreparedData
                {
                    EventId = eventAndCandidate.Event.EventId,
                    EventCode = eventAndCandidate.Event.Code,
                    EventType = eventAndCandidate.Event.Type,
                    CandidateId = eventAndCandidate.Candidate.CandidateId,
                    CandidateCode = eventAndCandidate.Candidate.Code,
                    TestId = eventAndCandidate.Event.TestId
                });
            return null;
        }

        public async Task<ServiceResult<List<PreparedData>>> GetPublicEventsAndCandidatesAsync()
        {
            var eventsAndCandidates = await _eventsRepository.GetEventsAndCandidatesAsync(new EventsAndCandidatesParams
            {
                Type = (int)EventType.Public,
                CandidateStatus = CandidateStatus.Open
            });

            return new ServiceResult<List<PreparedData>>(eventsAndCandidates.Select(eventAndCandidate => new PreparedData
            {
                EventId = eventAndCandidate.Event.EventId,
                EventCode = eventAndCandidate.Event.Code,
                EventType = eventAndCandidate.Event.Type,
                CandidateId = eventAndCandidate?.Candidate?.CandidateId ?? Guid.Empty,
                CandidateCode = eventAndCandidate?.Candidate?.Code ?? string.Empty,
                TestId = eventAndCandidate.Event.TestId
            }).ToList());
        }

        #endregion
    }
}

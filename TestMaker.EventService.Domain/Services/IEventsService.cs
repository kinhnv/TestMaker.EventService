using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Event;

namespace TestMaker.EventService.Domain.Services
{
    public interface IEventsService
    {
        Task<IEnumerable<EventForList>> GetEventsAsync();

        Task<EventForDetails> GetEventAsync(Guid eventId);

        Task<EventForDetails> CreateEventAsync(EventForCreating e);

        Task EditEventAsync(EventForEditing e);

        Task DeleteEventAsync(Guid eventId);

        Task<bool> EventExistsAsync(Guid eventId);

        Task<IEnumerable<SelectOption>> GetEventTypeAsSelectOptionsAsync();

        Task<PreparedData> GetPreparedCandidateByCodeAsync(PrepareCode code);

        Task<List<PreparedData>> GetPublicEventsAndCandidatesAsync();
    }
}

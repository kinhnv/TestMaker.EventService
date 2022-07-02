using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Event;

namespace TestMaker.EventService.Domain.Services
{
    public interface IEventsService
    {
        Task<ServiceResult<GetPaginationResult<EventForList>>> GetEventsAsync(GetEventsParams getEventsParams);

        Task<ServiceResult<EventForDetails>> GetEventAsync(Guid eventId);

        Task<ServiceResult<EventForDetails>> CreateEventAsync(EventForCreating e);

        Task<ServiceResult<EventForDetails>> EditEventAsync(EventForEditing e);

        Task<ServiceResult> DeleteEventAsync(Guid eventId);

        Task<ServiceResult<IEnumerable<SelectOption<int>>>> GetEventTypeAsSelectOptionsAsync();

        Task<ServiceResult<PreparedData>> GetPreparedCandidateByCodeAsync(PrepareCode code);

        Task<ServiceResult<List<PreparedData>>> GetPublicEventsAndCandidatesAsync();
    }
}

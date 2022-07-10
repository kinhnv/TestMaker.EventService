using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Services;

namespace TestMaker.EventService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ICandidatesService _candidatesService;

        public EventsController(IEventsService eventsService,
            ICandidatesService candidatesService)
        {
            _eventsService = eventsService;
            _candidatesService = candidatesService;
        }

        [HttpGet]
        [Route("GetPreparedCandidateByCode")]
        public async Task<IActionResult> GetPreparedCandidateByCodeAsync([FromQuery] PrepareCode code)
        {
            var result = await _eventsService.GetPreparedCandidateByCodeAsync(code);

            return Ok(new ApiResult<PreparedData>(result));
        }

        [HttpGet]
        [Route("GetPublicEventsAndCandidates")]
        public async Task<IActionResult> GetPublicEventsAndCandidatesAsync()
        {
            var result = await _eventsService.GetPublicEventsAndCandidatesAsync();
            return Ok(new ApiResult<List<PreparedData>>(result));
        }

        [HttpPost]
        [Route("CreateCandidate")]
        public async Task<IActionResult> CreateCandidateAsync([FromQuery]Guid eventId)
        {
            await _candidatesService.CreateCandidateAsync(new Domain.Models.Candidate.CandidateForCreating
            {
                EventId = eventId
            });
            return Ok(new ApiResult());
        }
    }
}

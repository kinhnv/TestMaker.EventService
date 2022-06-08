using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Services;

namespace TestMaker.EventService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        [Route("GetPreparedCandidateByCode")]
        public async Task<IActionResult> GetPreparedCandidateByCodeAsync([FromQuery] PrepareCode code)
        {
            var result = await _eventsService.GetPreparedCandidateByCodeAsync(code);

            return Ok(result);
        }
    }
}

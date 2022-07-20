using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Event;
using TestMaker.EventService.Domain.Services;

namespace TestMaker.EventService.Api.Controllers.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;

        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetEvents([FromQuery] GetEventsParams filter)
        {
            var result = await _eventsService.GetEventsAsync(new GetEventsParams
            {
                IsDeleted = filter.IsDeleted,
                Page = filter.Page,
                Take = filter.Take,
            });

            return Ok(new ApiResult<GetPaginationResult<EventForList>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEvent(Guid id)
        {
            var result = await _eventsService.GetEventAsync(id);

            return Ok(new ApiResult<EventForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, EventForEditing e)
        {
            if (id != e.EventId)
            {
                return Ok(new ApiResult());
            }

            var result = await _eventsService.EditEventAsync(e);

            return Ok(new ApiResult<EventForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostEvent(EventForCreating e)
        {
            var result = await _eventsService.CreateEventAsync(e);
            return Ok(new ApiResult<EventForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _eventsService.DeleteEventAsync(id);
            return Ok(new ApiResult(result));
        }

        [HttpGet]
        [Route("ScopeType")]
        public async Task<IActionResult> GetScopeTypeAsSelectOptions()
        {
            var result = await _eventsService.GetEventScopeTypeAsSelectOptionsAsync();
            return Ok(new ApiResult<IEnumerable<SelectOption<int>>>(result));
        }

        [HttpGet]
        [Route("QuestionContentType")]
        public async Task<IActionResult> GetQuestionContentTypeAsSelectOptions()
        {
            var result = await _eventsService.GetEventQuestionContentTypeAsSelectOptionsAsync();
            return Ok(new ApiResult<IEnumerable<SelectOption<int>>>(result));
        }

        [HttpGet]
        [Route("MarkingType")]
        public async Task<IActionResult> GetEventMarkingTypeAsSelectOptions()
        {
            var result = await _eventsService.GetEventMarkingTypeAsSelectOptionsAsync();
            return Ok(new ApiResult<IEnumerable<SelectOption<int>>>(result));
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Business.Admin.Domain.Models.Event;
using TestMaker.Business.Admin.Domain.Services;

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
        public async Task<ActionResult> GetEvents()
        {
            return Ok(await _eventsService.GetEventsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEvent(Guid id)
        {
            var e = await _eventsService.GetEventAsync(id);

            if (e == null)
            {
                return NotFound();
            }

            return Ok(e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, EventForEditing e)
        {
            if (id != e.EventId)
            {
                return BadRequest();
            }

            try
            {
                await _eventsService.EditEventAsync(e);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _eventsService.EventExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostEvent(EventForCreating e)
        {
            return Ok(await _eventsService.CreateEventAsync(e));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            if (!await _eventsService.EventExistsAsync(id))
            {
                return NotFound();
            }

            await _eventsService.DeleteEventAsync(id);

            return NoContent();
        }

        [HttpGet("Type")]
        public async Task<IActionResult> GetEventTypeAsSelectOptions()
        {
            var result = await _eventsService.GetEventTypeAsSelectOptionsAsync();
            return Ok(result);
        }
    }
}

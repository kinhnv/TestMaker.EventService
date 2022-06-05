using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Business.Admin.Domain.Models.Candidate;
using TestMaker.Business.Admin.Domain.Services;

namespace TestMaker.EventService.Api.Controllers.Admin
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesService _candidatesService;

        public CandidatesController(ICandidatesService candidatesService)
        {
            _candidatesService = candidatesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCandidates([FromQuery] GetCandidateFilter filter)
        {
            return Ok(await _candidatesService.GetCandidatesAsync(filter));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCandidate(Guid id)
        {
            var e = await _candidatesService.GetCandidateAsync(id);

            if (e == null)
            {
                return NotFound();
            }

            return Ok(e);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(Guid id, CandidateForEditing candidate)
        {
            if (id != candidate.CandidateId)
            {
                return BadRequest();
            }

            try
            {
                await _candidatesService.EditCandidateAsync(candidate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _candidatesService.CandidateExistsAsync(id))
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
        public async Task<ActionResult> PostCandidate(CandidateForCreating candidate)
        {
            return Ok(await _candidatesService.CreateCandidateAsync(candidate));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            if (!await _candidatesService.CandidateExistsAsync(id))
            {
                return NotFound();
            }

            await _candidatesService.DeleteCandidateAsync(id);

            return NoContent();
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Services;

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
        public async Task<ActionResult> GetCandidates([FromQuery] GetCandidatesParams filter)
        {
            var result = await _candidatesService.GetCandidatesAsync(new GetCandidatesParams
            {
                Take = filter.Take,
                Page = filter.Page,
                IsDeleted = filter.IsDeleted,
                EventId = filter.EventId,
            });

            return Ok(new ApiResult<GetPaginationResult<CandidateForList>>(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCandidate(Guid id)
        {
            var result = await _candidatesService.GetCandidateAsync(id);

            return Ok(new ApiResult<CandidateForDetails>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(Guid id, CandidateForEditing candidate)
        {
            if (id != candidate.CandidateId)
            {
                return Ok(new ApiResult());
            }

            var result = await _candidatesService.EditCandidateAsync(candidate);

            return Ok(new ApiResult<CandidateForDetails>(result));
        }

        [HttpPost]
        public async Task<ActionResult> PostCandidate(CandidateForCreating candidate)
        {
            var result = await _candidatesService.CreateCandidateAsync(candidate);
            return Ok(new ApiResult<CandidateForDetails>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(Guid id)
        {
            var result = await _candidatesService.DeleteCandidateAsync(id);
            return Ok(new ApiResult(result));
        }
    }
}

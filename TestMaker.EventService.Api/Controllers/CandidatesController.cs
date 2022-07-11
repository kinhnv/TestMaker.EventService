using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.Common.Models;
using TestMaker.EventService.Domain.Models;
using TestMaker.EventService.Domain.Models.Candidate;
using TestMaker.EventService.Domain.Services;

namespace TestMaker.EventService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidatesService _candidatesService;

        public CandidatesController(ICandidatesService candiadatesService)
        {
            _candidatesService = candiadatesService;
        }

        [HttpGet]
        [Route("GetAnswer")]
        public async Task<IActionResult> GetAnswerAsync(Guid candidateId, Guid questionId)
        {
            var result = await _candidatesService.GetAnswerAsync(candidateId, questionId);
            return Ok(new ApiResult<string>(result));
        }

        [HttpGet]
        [Route("GetAnswers")]
        public async Task<IActionResult> GetAnswersAsync(Guid candidateId)
        {
            var result = await _candidatesService.GetAnswersAsync(candidateId);
            return Ok(new ApiResult<List<TestMaker.EventService.Domain.Models.CandidateAnswer>>(result));
        }

        [HttpPost]
        [Route("SubmitQuestion")]
        public async Task<IActionResult> SubmitQuestionAsync(CandidateAnswerForSubmit answer)
        {
            var result = await _candidatesService.SubmitQuestionAsync(answer);
            return Ok(new ApiResult(result));
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> SubmitCandidateAsync(Guid candidateId)
        {
            var result = await _candidatesService.SubmitCandidateAsync(candidateId);
            return Ok(new ApiResult(result));
        }

        [HttpPost]
        [Route("Clear")]
        public async Task<IActionResult> ClearAsync(Guid candidateId)
        {
            var result = await _candidatesService.ClearAnswersOfCandidateAsync(candidateId);
            return Ok(new ApiResult(result));
        }

        [HttpPost]
        [Route("CreatePreparedTestTemp")]
        public async Task<IActionResult> CreatePreparedTestTempAsync([FromQuery]Guid candidateId, [FromBody]PreparedTest preparedTest)
        {
            var result = await _candidatesService.CreatePreparedTestTempAsync(candidateId, preparedTest);
            return Ok(new ApiResult(result));
        }

        [HttpGet]
        [Route("GetPreparedTestTemp")]
        public async Task<IActionResult> GetPreparedTestTempAsync(Guid candidateId)
        {
            var result = await _candidatesService.GetPreparedTestTempAsync(candidateId);
            return Ok(new ApiResult(result));
        }
    }
}

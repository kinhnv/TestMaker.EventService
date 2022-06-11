using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TestMaker.EventService.Domain.Models;
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
            return Ok(await _candidatesService.GetAnswerAsync(candidateId, questionId));
        }

        [HttpGet]
        [Route("GetAnswers")]
        public async Task<IActionResult> GetAnswersAsync(Guid candidateId)
        {
            return Ok(await _candidatesService.GetAnswersAsync(candidateId));
        }

        [HttpPost]
        [Route("SubmitQuestion")]
        public async Task<IActionResult> SubmitQuestionAsync(CandidateAnswerForSubmit answer)
        {
            await _candidatesService.SubmitQuestionAsync(answer);
            return Ok();
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> SubmitCandidateAsync(Guid candidateId)
        {
            await _candidatesService.SubmitCandidateAsync(candidateId);
            return Ok();
        }

        [HttpPost]
        [Route("Clear")]
        public async Task<IActionResult> ClearAsync(Guid candidateId)
        {
            await _candidatesService.ClearAnswersOfCandidateAsync(candidateId);
            return Ok();
        }
    }
}

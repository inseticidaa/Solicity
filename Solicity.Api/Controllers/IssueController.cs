using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solicity.Domain.DTOs;
using Solicity.Domain.Services;

namespace Solicity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private IIssueService _issueService;
        private IConfiguration _configuration;

        public IssueController(IConfiguration config, IIssueService issueService)
        {
            _configuration = config;
            _issueService = issueService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetIssues([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var issues = await _issueService.GetIssuesAsync(search, page, pageSize, requesterId);
               
                return Ok(issues);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{issueId}")]
        [Authorize]
        public async Task<IActionResult> GetIssue(Guid issueId)
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var issues = await _issueService.GetIssueDetailAsync(issueId, requesterId);

                return Ok(issues);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OpenIssue([FromBody] IssueCreationDTO issueCreationDTO)
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var issue = await _issueService.OpenIssueAsync(issueCreationDTO, requesterId);

                return CreatedAtAction(null, issue);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{issueId}/AddComment")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromRoute] Guid issueId, [FromBody] string comment)
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var issueCommentCreationDTO = new IssueCommentCreationDTO
                {
                    IssueId = issueId,
                    Comment = comment
                };

                var issue = await _issueService.AddCommentAsync(issueCommentCreationDTO, requesterId);

                return CreatedAtAction(null, issue);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

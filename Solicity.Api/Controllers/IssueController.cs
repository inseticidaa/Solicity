using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTopic([FromBody] IssueCreationDTO issueCreationDTO)
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
    }
}

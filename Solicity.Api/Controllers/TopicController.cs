using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solicity.Domain.DTOs;
using Solicity.Domain.Services;

namespace Solicity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private ITopicService _topicService;
        private IConfiguration _configuration;

        public TopicController(IConfiguration config, ITopicService topicService)
        {
            _configuration = config;
            _topicService = topicService;

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTopic([FromBody] TopicCreationDTO topicCreationDTO)
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var topic = await _topicService.CreateTopicAsync(topicCreationDTO, requesterId);

                return CreatedAtAction(null, topic);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var requesterId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value);

                var topics = await _topicService.GetAllAsync();

                return Ok(topics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

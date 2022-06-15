using Microsoft.Extensions.Configuration;
using Solicity.Domain.DTOs;
using Solicity.Domain.Entities;
using Solicity.Domain.Ports;
using Solicity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Application.Services
{
    public class TopicService : ITopicService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public TopicService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<TopicDTO> CreateTopicAsync(TopicCreationDTO topicCreationDTO, Guid requestBy)
        {
            var titleInUse = await _unitOfWork.Topics.GetByNameAsync(topicCreationDTO.Name);
            if (titleInUse != null) throw new Exception("Name aready in use");

            var codeInUse = await _unitOfWork.Topics.GetByNameAsync(topicCreationDTO.Name);
            if (codeInUse != null) throw new Exception("Code aready in use");

            var user = await _unitOfWork.Users.GetAsync(requestBy);
            if (user == null) throw new Exception("User not exists");

            var topic = new Topic 
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = user.Id,

                Name = topicCreationDTO.Name,
                Code = topicCreationDTO.Code.ToUpper(),
                Description = topicCreationDTO.Description,
                Enabled = true
            };

            await _unitOfWork.Topics.InsertAsync(topic);
            return (TopicDTO)topic;
        }

        public async Task<IList<TopicDTO>> GetAllAsync()
        {
            var topics = await _unitOfWork.Topics.GetAllAsync();

            return topics.Select(x => (TopicDTO)x).ToList();
        }
    }
}

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
    public class IssueService : IIssueService
    {
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public IssueService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<IssueDTO> OpenIssueAsync(IssueCreationDTO issueCreationDTO, Guid requestBy)
        {
            var user = await _unitOfWork.Users.GetAsync(requestBy);
            if (user == null) throw new Exception("User not exists");

            var topic = await _unitOfWork.Topics.GetAsync(issueCreationDTO.TopicId);
            if (topic == null) throw new Exception("Topic not exists");

            var count = await _unitOfWork.Issues.CountByTopicAsync(topic.Id);

            var issue = new Issue
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = user.Id,

                TopicId = topic.Id,
                Title = issueCreationDTO.Title,
                Code = $"{topic.Code}-{count + 1}",
                IsClosed = false
            };

            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.Issues.InsertAsync(issue);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }


            return (IssueDTO)issue;
        }
    }
}

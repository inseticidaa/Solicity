using FluentValidation;
using Microsoft.Extensions.Configuration;
using Solicity.Domain.DTOs;
using Solicity.Domain.Entities;
using Solicity.Domain.Enums;
using Solicity.Domain.Ports;
using Solicity.Domain.Services;
using Solicity.Domain.Validators;

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

        public async Task<IssueCommentDTO> AddCommentAsync(IssueCommentCreationDTO issueCommentCreationDTO, Guid requestBy)
        {
            var user = await _unitOfWork.Users.GetAsync(requestBy);
            if (user == null) throw new Exception("User not exists");

            var issue = await _unitOfWork.Issues.GetAsync(issueCommentCreationDTO.IssueId);
            if (issue == null) throw new Exception("Issue not exists");
            if (issue.Status == IssueStatusEnum.Cancelled) throw new Exception("Issue is closed");
            if (issue.Status == IssueStatusEnum.Done) throw new Exception("Issue is done");

            var issueComment = new IssueComment
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = user.Id,

                Comment = issueCommentCreationDTO.Comment,
                IssueId = issue.Id
            };

            var issueCommentValidator = new IssueCommentValidator();
            await issueCommentValidator.ValidateAndThrowAsync(issueComment);

            await _unitOfWork.IssuesComments.InsertAsync(issueComment);

            return (IssueCommentDTO)issueComment;
        }

        public async Task<IssueDetailDTO> GetIssueDetailAsync(Guid issueId, Guid requestBy)
        {
            var issue = await _unitOfWork.Issues.GetAsync(issueId);
            if (issue == null) throw new Exception("Issue not exists");

            var issueDetailDTO = (IssueDetailDTO)issue;

            var comments = await _unitOfWork.IssuesComments.GetAllByIssueAsync(issue.Id);

            issueDetailDTO.Comments = comments.ToList().OrderByDescending(x => x.CreatedAt);

            return issueDetailDTO;
        }

        public async Task<IList<IssueDTO>> GetIssuesAsync(string search, int page, int pageSize, Guid requestBy)
        {
            var filters = new Dictionary<string, string>();

            var issues = await _unitOfWork.Issues.GetAllAsync(page, pageSize, filters);

            return issues.Select(x => (IssueDTO)x).ToList();
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
                Topic = topic,
                Title = issueCreationDTO.Title,
                Code = $"{topic.Code}-{count + 1}",
                Status = IssueStatusEnum.Open,
                Author = user,
            };

            var issueComment = new IssueComment
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                CreatedBy = user.Id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = user.Id,
                Author = user,

                Comment = issueCreationDTO.Comment,
                IssueId = issue.Id
            };

            var issueValidator = new IssueValidator();
            await issueValidator.ValidateAndThrowAsync(issue);

            var issueCommentValidator = new IssueCommentValidator();
            await issueCommentValidator.ValidateAndThrowAsync(issueComment);

            _unitOfWork.BeginTransaction();

            try
            {
                await _unitOfWork.Issues.InsertAsync(issue);
                await _unitOfWork.IssuesComments.InsertAsync(issueComment);
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

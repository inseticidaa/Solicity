using Solicity.Domain.Entities;
using Solicity.Domain.Enums;

namespace Solicity.Domain.DTOs
{
    public class IssueDetailDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public UserDTO Author { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid TopicId { get; set; }
        public TopicDTO Topic { get; set; }
        public string Code { get; set; }
        public IssueStatusEnum Status { get; set; }
        public string Title { get; set; }

        public IEnumerable<IssueComment> Comments { get; set; }

        public static implicit operator IssueDetailDTO(Issue issue)
        {
            return new IssueDetailDTO
            {
                Id = issue.Id,
                CreatedAt = issue.CreatedAt,
                CreatedBy = issue.CreatedBy,
                UpdatedAt = issue.UpdatedAt,

                Code = issue.Code,
                Title = issue.Title,
                Status = issue.Status,
                TopicId = issue.TopicId,
                Topic = (TopicDTO)issue.Topic,
                Author = (UserDTO)issue.Author,
            };
        }
    }

    public class IssueDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public UserDTO Author { get; set; } 
        public DateTime UpdatedAt { get; set; }

        public Guid TopicId { get; set; }
        public TopicDTO Topic { get; set; }
        public string Code { get; set; }
        public IssueStatusEnum Status { get; set; }
        public string Title { get; set; }

        public static implicit operator IssueDTO(Issue issue)
        {
            return new IssueDTO
            {
                Id = issue.Id,
                CreatedAt = issue.CreatedAt,
                CreatedBy = issue.CreatedBy,
                UpdatedAt = issue.UpdatedAt,
                
                Code = issue.Code,
                Title = issue.Title,
                Status = issue.Status,
                TopicId = issue.TopicId,
                Topic = (TopicDTO)issue.Topic,
                Author = (UserDTO)issue.Author,
            };
        }
    }

    public class IssueCreationDTO
    {
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }

}

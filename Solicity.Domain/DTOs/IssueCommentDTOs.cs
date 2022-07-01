using Solicity.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Solicity.Domain.DTOs
{
    public class IssueCommentDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid IssueId { get; set; }
        public string Comment { get; set; }

        public static implicit operator IssueCommentDTO(IssueComment issueComment)
        {
            return new IssueCommentDTO
            {
                Id = issueComment.Id,
                CreatedAt = issueComment.CreatedAt,
                CreatedBy = issueComment.CreatedBy,
                UpdatedAt = issueComment.UpdatedAt,

                IssueId = issueComment.IssueId,
                Comment = issueComment.Comment,
            };
        }
    }

    public class IssueCommentCreationDTO
    {
        [Required]
        public Guid IssueId { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}

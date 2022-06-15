using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.DTOs
{
    public class IssueDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid TopicId { get; set; }
        public string Code { get; set; }
        public bool IsClosed { get; set; }
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
                IsClosed = issue.IsClosed,
                TopicId = issue.TopicId,
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

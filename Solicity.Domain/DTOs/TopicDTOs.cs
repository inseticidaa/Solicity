using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.DTOs
{
    public class TopicDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Enabled { get; set; }


        public static implicit operator TopicDTO(Topic topic)
        {
            return new TopicDTO 
            {
                Id = topic.Id,
                CreatedAt = topic.CreatedAt,
                CreatedBy = topic.CreatedBy,
                UpdatedAt = topic.UpdatedAt,
                UpdatedBy = topic.UpdatedBy,

                Name = topic.Name,
                Description = topic.Description,
                Code = topic.Code,
                Enabled = topic.Enabled,
            };
        }
    }

    public class TopicCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}

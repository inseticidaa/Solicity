using Solicity.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Services
{
    public interface ITopicService
    {
        Task<TopicDTO> CreateTopicAsync(TopicCreationDTO topicCreationDTO, Guid requestBy);
        Task<IList<TopicDTO>> GetAllAsync();
    }
}

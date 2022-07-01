using Solicity.Domain.DTOs;

namespace Solicity.Domain.Services
{
    public interface ITopicService
    {
        Task<TopicDTO> CreateTopicAsync(TopicCreationDTO topicCreationDTO, Guid requestBy);
        Task<IList<TopicDTO>> GetAllAsync(int page, int pageSize, string search);
    }
}

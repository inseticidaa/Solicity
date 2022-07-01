using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;

namespace Solicity.Domain.Ports.Repositories
{
    public interface IIssueRepository : IGenericRepository<Issue>
    {
        Task<int> CountByTopicAsync(Guid topicId);
    }
}

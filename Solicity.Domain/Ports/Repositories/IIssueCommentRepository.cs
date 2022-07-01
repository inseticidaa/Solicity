using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;

namespace Solicity.Domain.Ports.Repositories
{
    public interface IIssueCommentRepository : IGenericRepository<IssueComment>
    {
        public Task<IEnumerable<IssueComment>> GetAllByIssueAsync(Guid issueId);
    }
}

using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;

namespace Solicity.Domain.Ports.Repositories
{
    public interface ITopicRepository : IGenericRepository<Topic>
    {
        Task<Topic> GetByNameAsync(string name);
        Task<Topic> GetByCodeAsync(string code);
    }

}

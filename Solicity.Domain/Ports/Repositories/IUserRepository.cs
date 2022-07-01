using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;

namespace Solicity.Domain.Ports.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
    }
}

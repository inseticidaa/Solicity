using Solicity.Domain.Entities;

namespace Solicity.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(int page, int pageSize, IDictionary<string, string> filters);
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task UpdateAsync(T t);
        Task InsertAsync(T t);
    }
}

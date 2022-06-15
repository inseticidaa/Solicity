using Solicity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task UpdateAsync(T t);
        Task InsertAsync(T t);
    }
}

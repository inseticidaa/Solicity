using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Ports.Repositories
{
    public interface ITopicRepository: IGenericRepository<Topic>
    {
        Task<Topic> GetByNameAsync(string name);
        Task<Topic> GetByCodeAsync(string code);
    }

}

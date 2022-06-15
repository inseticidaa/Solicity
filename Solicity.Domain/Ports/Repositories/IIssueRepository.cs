using Solicity.Domain.Entities;
using Solicity.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Ports.Repositories
{
    public interface IIssueRepository: IGenericRepository<Issue>
    {
        Task<int> CountByTopicAsync(Guid topicId);
    }
}

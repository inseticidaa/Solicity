using Solicity.Domain.Ports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; set; }
        ITopicRepository Topics { get; set; }
        IIssueRepository Issues { get; set; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}

using Solicity.Domain.Ports;
using Solicity.Domain.Ports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public IUserRepository Users { get; set; }
        public ITopicRepository Topics { get; set; }
        public IIssueRepository Issues { get; set; }

        public UnitOfWork(DbSession session, 
            IUserRepository users, 
            ITopicRepository topics, 
            IIssueRepository issues)
        {
            _session = session;
            Users = users;
            Topics = topics;
            Issues = issues;
        }

        public void BeginTransaction()
        { 
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}

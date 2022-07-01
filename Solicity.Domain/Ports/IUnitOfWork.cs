using Solicity.Domain.Ports.Repositories;

namespace Solicity.Domain.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; set; }
        ITopicRepository Topics { get; set; }
        IIssueRepository Issues { get; set; }
        IIssueCommentRepository IssuesComments { get; set; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}

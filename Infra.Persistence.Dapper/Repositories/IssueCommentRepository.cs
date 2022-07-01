using Dapper;
using Solicity.Domain.Entities;
using Solicity.Domain.Ports.Repositories;

namespace Infra.Persistence.Dapper.Repositories
{
    public class IssueCommentRepository : IIssueCommentRepository
    {
        private DbSession _session;

        public IssueCommentRepository(DbSession session)
        {
            _session = session;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IssueComment>> GetAllAsync(int page, int pageSize, IDictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IssueComment>> GetAllByIssueAsync(Guid issueId)
        {
            var query = @"
                SELECT * FROM [dbo].[IssuesComments] IC
                INNER JOIN [dbo].[Users] U ON U.Id = IC.CreatedBy
                WHERE IC.IssueId = @IssueId;
            ";

            return await _session.Connection.QueryAsync<IssueComment, User, IssueComment>(
                query, 
                (comment, user) => { 
                    comment.Author = user;
                    return comment;
                }, 
                new { IssueId = issueId }, 
                _session.Transaction);
        }

        public Task<IssueComment> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(IssueComment t)
        {
            var query = @"
                INSERT INTO [dbo].[IssuesComments]
                    ([Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[IssueId]
                    ,[Comment])
                VALUES
                    (@Id
                    ,@UpdatedAt
                    ,@UpdatedBy
                    ,@CreatedAt
                    ,@CreatedBy
                    ,@IssueId
                    ,@Comment);";

            await _session.Connection.ExecuteAsync(query, t, _session.Transaction);
        }

        public Task UpdateAsync(IssueComment t)
        {
            throw new NotImplementedException();
        }
    }
}

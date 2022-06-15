using Solicity.Domain.Entities;
using Solicity.Domain.Ports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infra.Persistence.Dapper.Repositories
{
    public class IssueRepository: IIssueRepository
    {
        private DbSession _session;

        public IssueRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<int> CountByTopicAsync(Guid topicId)
        {
            var query = @"SELECT COUNT(*) FROM [dbo].[Issues] WHERE [TopicId] = @TopicId";

            return await _session.Connection.QueryFirstOrDefaultAsync<int>(query, new { TopicId = topicId }, _session.Transaction);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Issue>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Issue> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(Issue t)
        {
            var query = @"
                INSERT INTO [dbo].[Issues]
                    ([Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[TopicId]
                    ,[Code]
                    ,[IsClosed]
                    ,[Title])
                VALUES
                    (@Id
                    ,@UpdatedAt
                    ,@UpdatedBy
                    ,@CreatedAt
                    ,@CreatedBy
                    ,@TopicId
                    ,@Code
                    ,@IsClosed
                    ,@Title)";

            await _session.Connection.ExecuteAsync(query, t, _session.Transaction);
        }

        public Task UpdateAsync(Issue t)
        {
            throw new NotImplementedException();
        }
    }
}

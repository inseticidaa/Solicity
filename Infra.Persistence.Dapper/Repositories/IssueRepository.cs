using Dapper;
using Solicity.Domain.Entities;
using Solicity.Domain.Ports.Repositories;

namespace Infra.Persistence.Dapper.Repositories
{
    public class IssueRepository : IIssueRepository
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

        public async Task<IEnumerable<Issue>> GetAllAsync(int page, int pageSize, IDictionary<string, string> filters)
        {
            var builder = new SqlBuilder();
            var parameters = new DynamicParameters();

            var query = builder.AddTemplate(@"
                SELECT *
                FROM [dbo].[Issues] I
                INNER JOIN [dbo].[Topics] T ON I.TopicId = T.Id
                INNER JOIN [dbo].[Users] U ON I.CreatedBy = U.Id
                ");

            foreach (var filter in filters)
            {
                parameters.Add(filter.Key, filter.Value);
                builder.Where($"{filter.Key} LIKE @{filter.Key}");
            }

            return await _session.Connection.QueryAsync<Issue, Topic, User, Issue>(
                query.RawSql, 
                (issue, topic, user) => { 
                    issue.Topic = topic; 
                    issue.Author = user; 
                    return issue; 
                }, 
                parameters, 
                _session.Transaction);
        }

        public async Task<Issue> GetAsync(Guid id)
        {
            var query = @"
                SELECT *
                FROM [dbo].[Issues] I
                INNER JOIN [dbo].[Topics] T ON I.TopicId = T.Id
                INNER JOIN [dbo].[Users] U ON I.CreatedBy = U.Id
                WHERE I.[Id] = @IssueId
                ";


            var result = await _session.Connection.QueryAsync<Issue, Topic, User, Issue>(
                query,
                (issue, topic, user) =>
                {
                    issue.Topic = topic;
                    issue.Author = user;
                    return issue;
                },
                new { IssueId = id },
                _session.Transaction);

            return result.FirstOrDefault();
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
                    ,[Status]
                    ,[Title])
                VALUES
                    (@Id
                    ,@UpdatedAt
                    ,@UpdatedBy
                    ,@CreatedAt
                    ,@CreatedBy
                    ,@TopicId
                    ,@Code
                    ,@Status
                    ,@Title);";

            await _session.Connection.ExecuteAsync(query, t, _session.Transaction);
        }

        public Task UpdateAsync(Issue t)
        {
            throw new NotImplementedException();
        }
    }
}

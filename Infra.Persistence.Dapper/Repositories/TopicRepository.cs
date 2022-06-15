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
    public class TopicRepository: ITopicRepository
    {
        private DbSession _session;

        public TopicRepository(DbSession session)
        {
            _session = session;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Name]
                    ,[Description]
                    ,[Code]
                    ,[Enabled]
                FROM [dbo].[Topics];";

            return await _session.Connection.QueryAsync<Topic>(query, null, _session.Transaction);
        }

        public async Task<Topic> GetAsync(Guid id)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Name]
                    ,[Description]
                    ,[Code]
                    ,[Enabled]
                FROM [dbo].[Topics]
                WHERE [Id] = @Id";

            return await _session.Connection.QueryFirstOrDefaultAsync<Topic>(query, new { Id = id }, _session.Transaction);
        }

        public async Task<Topic> GetByCodeAsync(string code)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Name]
                    ,[Description]
                    ,[Code]
                    ,[Enabled]
                FROM [dbo].[Topics]
                WHERE [Code] = @Code";

            return await _session.Connection.QueryFirstOrDefaultAsync<Topic>(query, new { Code = code }, _session.Transaction);
        }

        public async Task<Topic> GetByNameAsync(string name)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Name]
                    ,[Description]
                    ,[Code]
                    ,[Enabled]
                FROM [dbo].[Topics]
                WHERE [Name] = @Name";

            return await _session.Connection.QueryFirstOrDefaultAsync<Topic>(query, new { Name = name }, _session.Transaction);
        }

        public async Task InsertAsync(Topic t)
        {
            var query = @"
                INSERT INTO [dbo].[Topics]
                       ([Id]
                       ,[UpdatedAt]
                       ,[UpdatedBy]
                       ,[CreatedAt]
                       ,[CreatedBy]
                       ,[Name]
                       ,[Description]
                       ,[Code]
                       ,[Enabled])
                 VALUES
                       (@Id
                       ,@UpdatedAt
                       ,@UpdatedBy
                       ,@CreatedAt
                       ,@CreatedBy
                       ,@Name
                       ,@Description
                       ,@Code
                       ,@Enabled)";

            await _session.Connection.ExecuteAsync(query, t, _session.Transaction);
        }

        public Task UpdateAsync(Topic t)
        {
            throw new NotImplementedException();
        }
    }
}

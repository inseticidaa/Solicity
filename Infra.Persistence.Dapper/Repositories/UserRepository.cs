using Dapper;
using Solicity.Domain.Entities;
using Solicity.Domain.Ports.Repositories;

namespace Infra.Persistence.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSession _session;

        public UserRepository(DbSession session)
        {
            _session = session;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync(int page, int pageSize, IDictionary<string, string> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Username]
                    ,[Email]
                    ,[Hash]
                    ,[FirstName]
                    ,[LastName]
                    ,[ProfileImage]
                    ,[Enabled]
                FROM [dbo].[Users]
                WHERE [Id] = @Id";

            return await _session.Connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id }, _session.Transaction);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Username]
                    ,[Email]
                    ,[Hash]
                    ,[FirstName]
                    ,[LastName]
                    ,[ProfileImage]
                    ,[Enabled]
                FROM [dbo].[Users]
                WHERE [Email] = @Email";

            return await _session.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email }, _session.Transaction);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var query = @"
                SELECT [Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Username]
                    ,[Email]
                    ,[Hash]
                    ,[FirstName]
                    ,[LastName]
                    ,[ProfileImage]
                    ,[Enabled]
                FROM [dbo].[Users]
                WHERE [Username] = @Username";

            return await _session.Connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username }, _session.Transaction);
        }

        public async Task InsertAsync(User t)
        {
            var query = @"
                INSERT INTO [dbo].[Users]
                    ([Id]
                    ,[UpdatedAt]
                    ,[UpdatedBy]
                    ,[CreatedAt]
                    ,[CreatedBy]
                    ,[Username]
                    ,[Email]
                    ,[Hash]
                    ,[FirstName]
                    ,[LastName]
                    ,[ProfileImage]
                    ,[Enabled])
                VALUES
                    (@Id
                    ,@UpdatedAt
                    ,@UpdatedBy
                    ,@CreatedAt
                    ,@CreatedBy
                    ,@Username
                    ,@Email
                    ,@Hash
                    ,@FirstName
                    ,@LastName
                    ,@ProfileImage
                    ,@Enabled)";

            await _session.Connection.ExecuteAsync(query, t, _session.Transaction);
        }

        public Task UpdateAsync(User t)
        {
            throw new NotImplementedException();
        }
    }
}

using Infra.Persistence.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Solicity.Domain.Ports;
using Solicity.Domain.Ports.Repositories;

namespace Infra.Persistence.Dapper
{
    public static class PersistenceModuleDependency
    {
        public static void AddPersistenceModule(this IServiceCollection services)
        {
            services.AddScoped<DbSession>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();
            services.AddTransient<IIssueRepository, IssueRepository>();
            services.AddTransient<IIssueCommentRepository, IssueCommentRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

using Infra.Persistence.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Solicity.Domain.Ports;
using Solicity.Domain.Ports.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

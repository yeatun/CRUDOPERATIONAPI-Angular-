using ContactList.Application.Contracts.Repositories.Command.Base;
using ContactList.Application.Contracts.Repositories.Query.Base;
using ContactList.Application.Contracts.Repositories;
using ContactList.Infrastructure.Persistance;
using ContactList.Infrastructure.Repositories.Command.Base;
using ContactList.Infrastructure.Repositories.Query.Base;
using ContactList.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactList.Infrastructure.Configs;
using ContactList.Application.Contracts.Repositories.Command;
using ContactList.Infrastructure.Repositories.Command;
using ContactList.Application.Contracts.Repositories.Query;
using ContactList.Infrastructure.Repositories.Query;
using ConfigurationSettings = ContactList.Infrastructure.Configs.ConfigurationSettings;

namespace ContactList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationSettings>(configuration);
            var serviceProvider = services.BuildServiceProvider();
            var opt = serviceProvider.GetRequiredService<IOptions<ConfigurationSettings>>().Value;

            // For SQLServer Connection
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    opt.ConnectionStrings.ConfigurationDbConnection,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                    });
            });

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<IUnitofWork, UnitOfWork>();
            services.AddScoped<Func<ApplicationDbContext>>((provider) => provider.GetService<ApplicationDbContext>);
            services.AddScoped<DbFactory>();


            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISuperVillainCommandRepository, SuperVillainCommandRepository>()
                .AddScoped<ISuperVillainQueryRepository, SuperVillainQueryRepository>();

            return services;
        }
    }
}

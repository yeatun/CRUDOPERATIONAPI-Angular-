using ContactList.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ContactList.Core.Repositories.Command.Base;
using ContactList.Core.Repositories.Command.Query;
using ContactList.Infrastructure.Data;
using ContactList.Infrastructure.Identity;
using ContactList.Infrastructure.Repositories.Command;
using ContactList.Infrastructure.Repositories.Command.Base;
using ContactList.Infrastructure.Repositories.Query;
using ContactList.Infrastructure.Repositories.Query.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ContactList.Infrastructure.Configs;

namespace ContactList.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ConfigurationSettings>(configuration);
            var serviceProvider = services.BuildServiceProvider();
            var opt = serviceProvider.GetRequiredService<IOptions<ConfigurationSettings>>().Value;
            services.AddDbContext<SuperVillainDbContext>(options =>
            {
                options.UseSqlServer(
                    opt.ConnectionStrings.ConfigurationDbConnection,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                    });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SuperVillainDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // For special character
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.User.RequireUniqueEmail = true;
            });


            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddTransient<ISuperVillainQueryRepository, SuperVillainQueryRepository>();
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<ISuperVillainCommandRepository, SuperVillainCommandRepository>();


            return services;
        }
    }
}

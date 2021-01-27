﻿using ConfigurationService.Application;
using ConfigurationService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationService.ServiceCollectionConfiguring
{
    public static class ServiceCollectionExtensions
    {
        private const string DbName = "ConfigurationStorage";

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProjects, Projects>();
            services.AddTransient<IConfigurations, Configurations>();
            services.AddTransient<IEnvironments, Environments>();
            services.AddTransient<IOptionGroups, OptionGroups>();
            services.AddTransient<IOptions, Options>();
            services.AddDbContext<ConfigurationServiceContext>(options =>
            {
                options.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalDb;Initial Catalog={DbName};");
                //options.UseInMemoryDatabase(DbName);
            });

            return services;
        }
    }
}

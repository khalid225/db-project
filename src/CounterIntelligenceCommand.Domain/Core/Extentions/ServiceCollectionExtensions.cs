using System;
using System.Collections.Generic;
using System.Text;
using CounterIntelligenceCommand.Data.Entities;
using CounterIntelligenceCommand.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CounterIntelligenceCommand.Domain.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
                );
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStaffRepository, StaffRepository>();
            return services;
        }
    }
}

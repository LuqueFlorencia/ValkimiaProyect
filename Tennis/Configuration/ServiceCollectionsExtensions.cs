﻿using Microsoft.EntityFrameworkCore;
using Tennis.Repository;

namespace Tennis.Configuration
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddTennisDbConfiguration(this IServiceCollection services)
        {
            IConfiguration _configuration;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _configuration = serviceScope.ServiceProvider.GetService<IConfiguration>()!;
            }

            var applicationOptions = new ApplicationOptions();
            _configuration.GetSection(ApplicationOptions.Section).Bind(applicationOptions);

            services.AddDbContext<TennisContext>(options => options.UseSqlServer(applicationOptions.ConnectionString));
        }
    }
}

using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MUCounter.Database;

namespace MUCounter.Configuration
{
    public static class DatabaseMigrationConfiguration
    {
        public static void UpgradeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                foreach (var databaseType in GetDbContextToMigrate())
                {
                    using (var context = (DbContext)serviceScope.ServiceProvider.GetService(databaseType))
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }

        private static IEnumerable<Type> GetDbContextToMigrate()
        {
            return new[]
            {
               typeof(MUCDatabaseContext)
            };
        }
    }
}

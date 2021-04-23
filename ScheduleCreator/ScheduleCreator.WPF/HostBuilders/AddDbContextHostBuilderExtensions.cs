using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleCreator.EntityFramework;
using System;

namespace ScheduleCreator.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string ConnectionString = context.Configuration.GetConnectionString("sqlite");

                Action<DbContextOptionsBuilder> configureDbContext = options => options.UseSqlite(ConnectionString);
                services.AddDbContext<ScheduleCreatorDbContext>(configureDbContext);
                services.AddSingleton<ScheduleCreatorDbContextFactory>(new ScheduleCreatorDbContextFactory());
                //services.AddScoped<ScheduleCreatorDbContextFactory>(configureDbContext);
                //services.AddSingleton<ScheduleCreatorDbContextFactory>(new ScheduleCreatorDbContextFactory(configureDbContext));
            });
            return host;
        }
    }
}

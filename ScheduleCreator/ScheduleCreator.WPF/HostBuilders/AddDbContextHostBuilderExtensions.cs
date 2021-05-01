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
            string databasePath = string.Concat("Data Source = ", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\ScheduleCreator\\ScheduleCreator.db");
            
            host.ConfigureServices((context, services) =>
            {
                string ConnectionString = context.Configuration.GetConnectionString("sqlite");

                //Action<DbContextOptionsBuilder> configureDbContext = options => options.UseSqlite(ConnectionString);
                Action<DbContextOptionsBuilder> configureDbContext = options => options.UseSqlite(databasePath);
                services.AddDbContext<ScheduleCreatorDbContext>(configureDbContext);
                services.AddSingleton<ScheduleCreatorDbContextFactory>(new ScheduleCreatorDbContextFactory());
            });
            return host;
        }
    }
}

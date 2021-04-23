using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleCreator.EntityFramework;
using ScheduleCreator.EntityFramework.Repositories.DateRepository;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using ScheduleCreator.EntityFramework.Repositories.PreferenceRepository;
using ScheduleCreator.EntityFramework.Repositories.ScheduleRepository;

namespace ScheduleCreator.WPF.HostBuilders
{
    public static class AddRepositoriesHostBuilderExtensions
    {
        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
                services.AddSingleton<IPreferenceRepository, PreferenceRepository>();
                services.AddSingleton<IPreferenceDayRepository, PreferenceDayRepository>();
                services.AddSingleton<IScheduleRepository, ScheduleRepository>();
            });

            return host;
        }
    }
}

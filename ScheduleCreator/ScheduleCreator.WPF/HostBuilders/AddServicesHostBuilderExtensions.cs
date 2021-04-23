using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Services;

namespace ScheduleCreator.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IEmployeeService, EmployeeService>();
                services.AddSingleton<IPreferenceService, PreferenceService>();
                services.AddSingleton<IPreferenceDayService, PreferenceDayService>();
                services.AddSingleton<IScheduleService, ScheduleService>();
            });

            return host;
        }
    }
}

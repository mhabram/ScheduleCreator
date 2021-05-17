using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;

namespace ScheduleCreator.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IScheduleCreatorViewModelFactory, ScheduleCreatorViewModelFactory>();

                services.AddSingleton<PreferenceViewModel>();
                services.AddSingleton<EmployeeViewModel>();
                services.AddSingleton<ScheduleViewModel>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<HelpViewModel>();

                //Delegates
                services.AddSingleton<CreateViewModel<PreferenceViewModel>>(services => () => CreatePreferenceViewModel(services));
                services.AddSingleton<CreateViewModel<EmployeeViewModel>>(services => () => CreateEmployeeViewModel(services));
                services.AddSingleton<CreateViewModel<ScheduleViewModel>>(services => () => CreateScheduleViewModel(services));
                services.AddSingleton<CreateViewModel<HelpViewModel>>(services =>
                {
                    return () => services.GetRequiredService<HelpViewModel>();
                });
            });

            return host;
        }

        private static PreferenceViewModel CreatePreferenceViewModel(IServiceProvider services)
        {
            return new PreferenceViewModel(
                services.GetRequiredService<IPreferenceService>(),
                services.GetRequiredService<IEmployeeService>()
                );
        }

        private static EmployeeViewModel CreateEmployeeViewModel(IServiceProvider services)
        {
            return new EmployeeViewModel(services.GetRequiredService<IEmployeeService>());
        }

        private static ScheduleViewModel CreateScheduleViewModel(IServiceProvider services)
        {
            return new ScheduleViewModel(
                        services.GetRequiredService<IEmployeeService>(),
                        services.GetRequiredService<IScheduleService>(),
                        services.GetRequiredService<IPreferenceService>()
                        );
        }
    }
}

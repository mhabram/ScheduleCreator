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
                //probably to delete
                services.AddSingleton<HelpViewModel>();
                services.AddSingleton<CreateScheduleViewModel>();

                //Delegates
                services.AddSingleton<CreateViewModel<PreferenceViewModel>>(services => () => CreatePreferenceViewModel(services));
                services.AddSingleton<CreateViewModel<EmployeeViewModel>>(services => () => CreateEmployeeViewModel(services));
                services.AddSingleton<CreateViewModel<ScheduleViewModel>>(services => () => CreateScheduleViewModel(services));

                //probably to delete
                services.AddSingleton<CreateViewModel<HelpViewModel>>(services =>
                {
                    return () => services.GetRequiredService<HelpViewModel>();
                });
                services.AddSingleton<CreateViewModel<CreateScheduleViewModel>>(services => // this one can be refactored in future.
                {
                    return () => services.GetRequiredService<CreateScheduleViewModel>();
                });
            });

            return host;
        }

        private static PreferenceViewModel CreatePreferenceViewModel(IServiceProvider services)
        {
            return new PreferenceViewModel(
                        services.GetRequiredService<IPreferenceService>(),
                        services.GetRequiredService<IEmployeeService>(),
                        services.GetRequiredService<IPreferenceDayService>()
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
                        services.GetRequiredService<IScheduleService>()
                        );
        }
    }
}

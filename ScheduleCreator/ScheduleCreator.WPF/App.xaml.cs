using Microsoft.Extensions.DependencyInjection;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework;
using ScheduleCreator.EntityFramework.Repositories.DateRepository;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using ScheduleCreator.EntityFramework.Repositories.PreferenceRepository;
using ScheduleCreator.EntityFramework.Repositories.ScheduleRepository;
using ScheduleCreator.EntityFramework.Services;
using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Windows;

namespace ScheduleCreator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();
            
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
             IServiceCollection services = new ServiceCollection();

            //Database
            services.AddSingleton<ScheduleCreatorDbContextFactory>();

            //Services
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IPreferenceService, PreferenceService>();
            services.AddSingleton<IPreferenceDayService, PreferenceDayService>();
            services.AddSingleton<IScheduleService, ScheduleService>();

            //Repositoriess
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IPreferenceRepository, PreferenceRepository>();
            services.AddSingleton<IPreferenceDayRepository, PreferenceDayRepository>();
            services.AddSingleton<IScheduleRepository, ScheduleRepository>();

            //Factories
            services.AddSingleton<IScheduleCreatorViewModelFactory, ScheduleCreatorViewModelFactory>();
            services.AddSingleton<PreferenceViewModel>();
            services.AddSingleton<EmployeeViewModel>();
            services.AddSingleton<ScheduleViewModel>();
            //probably to delete
            services.AddSingleton<HelpViewModel>();
            services.AddSingleton<CreateScheduleViewModel>();

            //Delegates
            services.AddSingleton<CreateViewModel<PreferenceViewModel>>(services =>
            {
                return () => new PreferenceViewModel(
                    services.GetRequiredService<IPreferenceService>(),
                    services.GetRequiredService<IEmployeeService>(),
                    services.GetRequiredService<IPreferenceDayService>()
                    );
            });

            services.AddSingleton<CreateViewModel<EmployeeViewModel>>(services =>
            {
                return () => new EmployeeViewModel(services.GetRequiredService<IEmployeeService>());
            });

            services.AddSingleton<CreateViewModel<ScheduleViewModel>>(services =>
            {
                return () => new ScheduleViewModel(
                    services.GetRequiredService<IEmployeeService>(),
                    services.GetRequiredService<IScheduleService>()
                    );
            });

            //probably to delete
            services.AddSingleton<CreateViewModel<HelpViewModel>>(services =>
            {
                return () => services.GetRequiredService<HelpViewModel>();
            });
            services.AddSingleton<CreateViewModel<CreateScheduleViewModel>>(services => // this one can be refactored in future.
            {
                return () => services.GetRequiredService<CreateScheduleViewModel>();
            });

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}

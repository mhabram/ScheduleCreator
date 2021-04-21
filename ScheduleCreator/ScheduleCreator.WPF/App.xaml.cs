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
            services.AddSingleton<IRootScheduleCreatorViewModelFactory, RootScheduleCreatorViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<HelpViewModel>, HelpViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<PreferenceViewModel>, PreferenceViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<CreateScheduleViewModel>, CreateScheduleViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<EmployeeViewModel>, EmployeeViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<ScheduleViewModel>, ScheduleViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using ScheduleCreator.EntityFramework.Services;
using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            //Repositories
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

            //Factories
            services.AddSingleton<IRootScheduleCreatorViewModelFactory, RootScheduleCreatorViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<HelpViewModel>, HelpViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<ConditionsViewModel>, ConditionsViewModelFacoty>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<CreateScheduleViewModel>, CreateScheduleViewModelFactory>();
            services.AddSingleton<IScheduleCreatorViewModelFactory<EmployeeViewModel>, EmployeeViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}

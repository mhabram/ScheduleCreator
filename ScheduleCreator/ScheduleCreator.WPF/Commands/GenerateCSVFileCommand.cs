using ScheduleCreator.Domain.GenerateToExcel;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class GenerateCSVFileCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IScheduleService _scheduleService;

        public GenerateCSVFileCommand(ScheduleViewModel viewModel, IScheduleService scheduleService)
        {
            _viewModel = viewModel;
            _scheduleService = scheduleService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            ICollection<Employee> employees = await _scheduleService.GetSchedule();
            bool isSaved = false;
            Domain.GenerateToExcel.Schedule schedule = new Domain.GenerateToExcel.Schedule(employees);
            isSaved = schedule.Create(@"C:\Temp\Schedule.xlsx");

            if (isSaved)
                MessageBox.Show("File xlsx has been created.");
            else
                MessageBox.Show("File has not been created");
        }
    }
}

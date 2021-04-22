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
    class GenerateExcelFileCommand : AsyncCommandBase
    {
        private readonly IScheduleService _scheduleService;

        public GenerateExcelFileCommand(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            IList<Employee> employees = await _scheduleService.GetSchedule();
            bool isSaved = false;
            Schedule schedule = new Schedule(employees);
            isSaved = schedule.Create(@"C:\Temp\Schedule.xlsx");

            if (isSaved)
                MessageBox.Show("File xlsx created.");
            else
                MessageBox.Show("File has not been created");
        }
    }
}

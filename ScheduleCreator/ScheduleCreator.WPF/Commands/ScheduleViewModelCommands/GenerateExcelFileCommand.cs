using ScheduleCreator.Domain.GenerateToExcel;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ScheduleCreator.WPF.Commands.ScheduleViewModelCommands
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
            try
            {
                IList<Employee> employees = await _scheduleService.GetSchedule();
                Schedule schedule = new(employees);
                schedule.Create(@"C:\Temp\Schedule.xlsx");

                MessageBox.Show("File xlsx created.");
            }
            catch (Exception)
            {
                MessageBox.Show("File has not been created");
            }
        }
    }
}

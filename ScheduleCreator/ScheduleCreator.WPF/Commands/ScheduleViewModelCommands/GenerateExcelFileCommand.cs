using ScheduleCreator.Domain.GenerateToExcel;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
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

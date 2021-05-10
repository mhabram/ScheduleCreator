using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.Commands.ScheduleViewModelCommands
{
    class RandomSplitCommand : AsyncCommandBase
    {
        private readonly ScheduleViewModel _viewModel;

        public RandomSplitCommand(ScheduleViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parametr)
        {
            CalendarDateDTO calendarDate;
            EmployeeDTO employee;
            Random random = new ();
            int[] numbers = { 1, 2, 3, 4, 5 };


        }

        private void AddWorkingDays(EmployeeDTO employee)
        {

            //for (int i = 0; i < _viewModel.CalendarDates.Count; i++)
            //{
            //    calendarDate = _viewModel.CalendarDates[i];

            //    for (int j = 0; j < calendarDate.Employees.Count; j++)
            //    {
            //        employee = calendarDate.Employees[j];

            //        if (calendarDate.)


            //    }
            //}
            //_viewModel.CalendarUpdateDayShiftCommand.Execute(employee);
        }
    }
}

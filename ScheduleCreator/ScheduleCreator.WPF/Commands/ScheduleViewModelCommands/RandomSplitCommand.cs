using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Helpers;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            AddWorkingDays();
        }

        private void AddWorkingDays()
        {
            Random random = new();
            CalendarDateDTO calendarDate;
            EmployeeDTO employee;

            for (int i = 0; i < _viewModel.CalendarDates.Count; i++)
            {
                calendarDate = _viewModel.CalendarDates[i];

                for (int j = 0; j < calendarDate.Employees.Count; j++)
                {
                    employee = calendarDate.Employees[j];

                    RandomShift(random, employee);
                }
            }
        }

        private void RandomShift(Random random, EmployeeDTO employee)
        {
            Array values = Enum.GetValues(typeof(Shift));
            Shift randomShift = (Shift)values.GetValue(random.Next(values.Length));
            int min = 1;
            int max = 5;

            if (!employee.IsAssigned())
            {
                switch (randomShift)
                {
                    case Shift.Day:
                        employee.Day = true;
                        for (int i = 0; i < random.Next(min, max); i++)
                        {
                            _viewModel.CalendarUpdateDayShiftCommand.Execute(employee);
                            employee.CalendarDateDTOId++;
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                        }
                        break;
                    case Shift.Swing:
                        employee.Swing = true;
                        for (int i = 0; i < random.Next(min, max); i++)
                        {
                            _viewModel.CalendarUpdateSwingShiftCommand.Execute(employee);
                            employee.CalendarDateDTOId++;
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                        }
                        break;
                    case Shift.Night:
                        employee.Night = true;
                        for (int i = 0; i < random.Next(min, max); i++)
                        {
                            _viewModel.CalendarUpdateNightShiftCommand.Execute(employee);
                            employee.CalendarDateDTOId++;
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                            //if (employee.IsAssigned())
                            //{
                            //    break;
                            //    //min = 0;
                            //    //max = 0;
                            //    //goto case Shift.Day;
                            //}
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

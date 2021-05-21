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

            for (int i = 0; i < _viewModel.CalendarDates.Count; i++)
            {
                calendarDate = _viewModel.CalendarDates[i];

                for (int j = 0; j < calendarDate.Employees.Count; j++)
                {
                    RandomShift(random, i, j);
                }
            }
        }

        private void RandomShift(Random random, int calendarIndex, int employeeIndex)
        {
            Array values = Enum.GetValues(typeof(Shift));
            EmployeeDTO employee = _viewModel.CalendarDates[calendarIndex].Employees[employeeIndex];
            Shift randomShift = (Shift)values.GetValue(random.Next(values.Length));
            int randomNumber = random.Next(1, 4); //default (1, 4)
            int index = 0;
            if (!employee.IsAssigned())
            {
                switch (randomShift)
                {
                    case Shift.Day:
                        while (index < randomNumber)
                        {
                            employee.Day = true;
                            _viewModel.CalendarUpdateDayShiftCommand.Execute(employee);
                            if (!employee.IsAssigned() && randomShift.ToString() != "Swing")
                            {
                                employee.Shift = "";
                                goto case Shift.Swing;
                            }
                            employee = _viewModel.CalendarDates[++index].Employees[employeeIndex];
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                        }
                        break;
                    case Shift.Swing:
                        while (index < randomNumber)
                        {
                            employee.Swing = true;
                            _viewModel.CalendarUpdateSwingShiftCommand.Execute(employee);
                            if (!employee.IsAssigned() && randomShift.ToString() != "Night")
                            {
                                employee.Shift = "";
                                goto case Shift.Night;
                            }
                            employee = _viewModel.CalendarDates[++index].Employees[employeeIndex];
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                        }
                        break;
                    case Shift.Night:
                        while (index < randomNumber)
                        {
                            employee.Night = true;
                            _viewModel.CalendarUpdateNightShiftCommand.Execute(employee);
                            if (!employee.IsAssigned() && randomShift.ToString() != "Day")
                            {
                                employee.Shift = "";
                                goto case Shift.Day;
                            }
                            employee = _viewModel.CalendarDates[++index].Employees[employeeIndex];
                            if (employee.CalendarDateDTOId > (_viewModel.CalendarDates.Count - 1))
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

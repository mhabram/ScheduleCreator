using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class GetCalendarEmployeeDetailsCommand : ICommand
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        private readonly CalendarHelper _calendarHelper = new();

        public GetCalendarEmployeeDetailsCommand(ScheduleViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            ICollection<DateTime> calendar = _calendarHelper.CalendarDate();
            Collection<Employee> employees = await _employeeService.GetDetails();
            Collection<DateTime> tempPreferenceDays = new();
            Collection<EmployeeDTO> tempEmployees;
            sbyte freeWorkingDays;

            foreach (Employee e in employees)
            {
                string fullName = String.Concat(e.Name, " ", e.LastName);
                freeWorkingDays = _calendarHelper.WorkingDaysInMonth(e.Preferences.FreeWorkingDays);
                _viewModel.Employees.Add(new EmployeeViewDTO { FullName = fullName, WorkingDays = freeWorkingDays });
            }

            foreach (DateTime d in calendar)
            {
                tempEmployees = new();
                foreach (Employee e in employees)
                {
                    string fullName = String.Concat(e.Name, " ", e.LastName);
                    freeWorkingDays = _calendarHelper.WorkingDaysInMonth(e.Preferences.FreeWorkingDays);

                    foreach (Date date in e.Preferences.Dates)
                    {
                        tempPreferenceDays.Add(date.FreeDayChosen);
                    }
                    
                    tempEmployees.Add(new EmployeeDTO
                    {
                        FullName = fullName,
                        WorkingDays = 0,
                        Date = d,
                        IsWorking = false,
                        PreferenceDays = tempPreferenceDays
                    });

                    tempPreferenceDays = new();
                }

                _viewModel.CalendarDates.Add(new CalendarDateDTO { Employees = tempEmployees, Date = d });
            }
        }
    }
}

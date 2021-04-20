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
        private readonly IScheduleService _scheduleService;
        private readonly IPreferenceService _preferenceService;

        public GetCalendarEmployeeDetailsCommand(ScheduleViewModel viewModel, IEmployeeService employeeService, IScheduleService scheduleService, IPreferenceService preferenceService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
            _scheduleService = scheduleService;
            _preferenceService = preferenceService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        { 
            CalendarHelper calendarHelper = new();
            //Collection<DateTime> calendar = calendarHelper.CalendarDate();
            IList<Employee> employees = await _scheduleService.GetSchedule();
            ObservableCollection<EmployeeDTO> tempEmployees;
            //ICollection<Preferences> preferences = await _preferenceService.GetPreferences(); // bad Idea
            int freeWorkingDays;
            string fullName;

            //Preferences
            for (int i = 0; i < employees.Count; i++)
            {
                _viewModel.Preferences.Add(employees[i].Preferences);
            }

            //if (employees.ElementAt(0).Weeks.Count == 0)
            //    employees = await _employeeService.GetDetails(); // probaby this is not needed. TBChanged asap.

            //EmployeeViewDTO - working while data in database.
            for (int i = 0; i < employees.Count; i++)
            {
                fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                freeWorkingDays = calendarHelper.WorkingDaysInMonth(employees[i].Preferences.FreeWorkingDays);
                
                for (int j = 0; j < employees[i].Weeks.Count; j++)
                {
                    freeWorkingDays = freeWorkingDays - employees[i].Weeks[j].Days.Where(d => d.IsWorking == true).ToList().Count;
                }

                _viewModel.Employees.Add(new EmployeeViewDTO { FullName = fullName, WorkingDays = freeWorkingDays });
            }

            //EmployeeViewDTO - working all the time
            //for (int i = 0; i < employees.Count; i++)
            //{
            //    fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
            //    freeWorkingDays = calendarHelper.WorkingDaysInMonth(employees[i].Preferences.FreeWorkingDays);
            //    _viewModel.Employees.Add(new EmployeeViewDTO { FullName = fullName, WorkingDays = freeWorkingDays });
            //}

            //CalendarDates - working while data in database
            for (int i = 0; i < calendarHelper.CalendarDate().Count; i++)
            {
                tempEmployees = new();
                for (int j = 0; j < employees.Count; j++)
                {
                    fullName = String.Concat(employees[j].Name, " ", employees[j].LastName);



                    tempEmployees.Add(new EmployeeDTO()
                    {
                        EmployeeId = j,
                        FullName = fullName,
                        CalendarDateDTOId = i,
                    });
                }
            }

            //CalendarDates - working all the time
            for (int i = 0; i < calendarHelper.CalendarDate().Count; i++)
            {
                tempEmployees = new();
                for (int j = 0; j < employees.Count; j++)
                {
                    fullName = String.Concat(employees[j].Name, " ", employees[j].LastName);

                    tempEmployees.Add(new EmployeeDTO()
                    {
                        EmployeeId = j,
                        FullName = fullName,
                        CalendarDateDTOId = i,
                    });
                }
                _viewModel.CalendarDates.Add(new CalendarDateDTO() { Id = i, Date = calendarHelper.CalendarDate()[i], Employees = tempEmployees });
            }
        }
    }
}

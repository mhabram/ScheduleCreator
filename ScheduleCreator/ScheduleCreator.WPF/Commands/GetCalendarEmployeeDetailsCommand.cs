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
            Collection<DateTime> calendar = calendarHelper.CalendarDate();
            Collection<Employee> employees = await _employeeService.GetDetails();
            ObservableCollection<EmployeeDTO> tempEmployees;
            //ICollection<Preferences> preferences = await _preferenceService.GetPreferences(); // bad Idea
            sbyte freeWorkingDays;
            string fullName;

            
            //if (employees.ElementAt(0).Weeks.Count == 0)
            //    employees = await _employeeService.GetDetails(); // probaby this is not needed. TBChanged asap.

            //if (employees.ElementAt(0).Weeks.Count == 0)
            //{
            for(int i = 0; i < employees.Count; i++)
            {
                fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                freeWorkingDays = calendarHelper.WorkingDaysInMonth(employees[i].Preferences.FreeWorkingDays);
                _viewModel.Employees.Add(new EmployeeViewDTO { FullName = fullName, WorkingDays = freeWorkingDays });
            }
            
            for (int i = 0; i < calendar.Count; i++)
            {
                tempEmployees = new();
                for (int j = 0; j < employees.Count; j++)
                {
                    _viewModel.Preferences.Add(employees[j].Preferences);
                    fullName = String.Concat(employees[j].Name, " ", employees[j].LastName);

                    tempEmployees.Add(new EmployeeDTO
                    {
                        FullName = fullName,
                        Date = calendar[i],
                        IsWorking = false,
                    });
                }
                _viewModel.CalendarDates.Add(new CalendarDateDTO { Employees = tempEmployees, Date = calendar[i]});
            }

            //else
            //{
            //    foreach (DateTime day in calendar)
            //    {
            //        foreach (Employee e in employees)
            //        {
            //            tempEmployees = new();
            //            fullName = String.Concat(e.Name, " ", e.LastName);
            //            foreach (Week w in e.Weeks)
            //            {
            //                foreach (Day d in w.Days)
            //                {
            //                    test.Date = d.WorkingDay;
            //                    test.Employees.Add(new EmployeeDTO()
            //                    {
            //                        FullName = fullName,
            //                        Shift = d.Shift,
            //                        IsWorking = d.IsWorking,
            //                        Date = d.WorkingDay,
            //                        WorkingDays = w.Days.Count()
            //                    });
            //                    //tempEmployees.Add(new EmployeeDTO
            //                    //{
            //                    //    FullName = fullName,
            //                    //    Shift = d.Shift,
            //                    //    IsWorking = d.IsWorking,
            //                    //    Date = d.WorkingDay,
            //                    //    WorkingDays = w.Days.Count
            //                    //});
            //                    //_viewModel.CalendarDates.Add(new CalendarDateDTO
            //                    //{
            //                    //    Employees = new Collection<EmployeeDTO>().Add(test),
            //                    //    Date = d.WorkingDay
            //                    //});
            //                }
            //            }
            //        }
            //        _viewModel.CalendarDates.Add(new CalendarDateDTO { Employees = tempEmployees, Date = day });
            //    }
            //}
        }
    }
}

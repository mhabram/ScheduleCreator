using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands.ScheduleViewModelCommands
{
    class GetCalendarEmployeeDetailsCommand : AsyncCommandBase
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;
        private readonly IScheduleService _scheduleService;
        private readonly IPreferenceService _preferenceService;

        public GetCalendarEmployeeDetailsCommand(ScheduleViewModel viewModel,
            IEmployeeService employeeService,
            IScheduleService scheduleService,
            IPreferenceService preferenceService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
            _scheduleService = scheduleService;
            _preferenceService = preferenceService;
        }

        public override async Task ExecuteAsync(object parameter)
        { 
            CalendarHelper calendarHelper = new();
            IList<Employee> employees = new List<Employee>();

            try
            {
                _viewModel.Preferences = await _preferenceService.GetPreferences();
                employees = await _employeeService.GetDetails();
                await LoadDataSchedule(employees, calendarHelper);
                GetEmployeeViewDTO(employees);
            }
            catch(Exception) { }
        }

        private void GetEmployeeViewDTO(IList<Employee> employees)
        {
            for (int i = 0; i < _viewModel.CalendarDates[0].Employees.Count; i++)
            {
                EmployeeViewDTO employeeViewDTO = new();
                List<DateTime> preferenceDays = new();
                CalendarHelper calendarHelper = new();
                EmployeeDTO employeeDTO = _viewModel.CalendarDates[0].Employees[i];
                string lastName = employeeDTO.FullName.Split()[1];
                string fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                PreferencesDTO pref = _viewModel.Preferences.Where(e => e.LastName == lastName.ToLower()).FirstOrDefault();

                for (int j = 0; j < pref.PreferenceDays.Count; j++)
                {
                    preferenceDays.Add(pref.PreferenceDays[j].FreeDayChosen);
                }

                employeeViewDTO.PreferenceDays = preferenceDays;
                employeeViewDTO.FullName = fullName;
                employeeViewDTO.WorkingDays = calendarHelper.WorkingDaysInMonth() - pref.FreeWorkingDays;
                _viewModel.Employees.Add(employeeViewDTO);

                //for (int j = 0; j < _viewModel.CalendarDates.Count; j++)
                //{
                //    _viewModel.CalendarDates[j].UpdateEmployeeView(_viewModel.Employees[i]);
                //}
                //employeeViewDTO.FullName = fullName;
            }
        }

        private async Task LoadDataSchedule(IList<Employee> employees, CalendarHelper calendarHelper)
        {
            Dictionary<DateTime, ObservableCollection<EmployeeDTO>> dateEmployeeList = new();
            Collection<DateTime> calendar = calendarHelper.CalendarDate();
            EmployeeDTO employeeDTO;
            Employee employee;
            Day workDay;
            string fullName;
            string shift;

            for (int i = 0; i < employees.Count; i++)
            {
                fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                try
                {
                    employee = await _scheduleService.GetEmployeeSchedule(employees[i].EmployeeId);
                    if (employee.Days.Count > 0)
                    {
                        for (int j = 0; j < employee.Days.Count; j++)
                        {
                            workDay = employee.Days[j];
                            shift = workDay.Shift;
                            employeeDTO = new EmployeeDTO(j, i, fullName, shift);

                            if (dateEmployeeList.ContainsKey(workDay.WorkingDay.Date))
                                dateEmployeeList[workDay.WorkingDay.Date].Add(employeeDTO);
                            else
                                dateEmployeeList.Add(workDay.WorkingDay.Date, new ObservableCollection<EmployeeDTO>() { employeeDTO });
                        }
                    }
                    else
                    {
                        for (int j = 0; j < calendar.Count; j++)
                        {
                            employeeDTO = new EmployeeDTO(j, i, fullName);
                            workDay = new Day()
                            {
                                WorkingDay = calendar[j].Date.Date,
                                IsWorking = false
                            };

                            if (dateEmployeeList.ContainsKey(workDay.WorkingDay.Date.Date))
                                dateEmployeeList[workDay.WorkingDay.Date].Add(employeeDTO);
                            else
                                dateEmployeeList.Add(workDay.WorkingDay.Date.Date, new ObservableCollection<EmployeeDTO>() { employeeDTO });
                        }
                    }
                }
                catch (Exception) { }
            }
            AssignDataToCalendarDates(dateEmployeeList);
        }

        private void AssignDataToCalendarDates(Dictionary<DateTime, ObservableCollection<EmployeeDTO>> dateEmployeeList)
        {
            int id = 0;
            foreach (var key in dateEmployeeList)
            {
                _viewModel.CalendarDates.Add(new CalendarDateDTO()
                {
                    Id = id,
                    Date = key.Key,
                    Employees = key.Value
                });
                id++;
            }
        }
    }
}

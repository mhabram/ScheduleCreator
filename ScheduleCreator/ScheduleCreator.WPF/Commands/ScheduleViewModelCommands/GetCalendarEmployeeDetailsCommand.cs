using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Helpers.Calendar;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands.ScheduleViewModelCommands
{
    class GetCalendarEmployeeDetailsCommand : AsyncCommandBase
    {
        private readonly ScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;
        private readonly IScheduleService _scheduleService;

        public GetCalendarEmployeeDetailsCommand(ScheduleViewModel viewModel,
            IEmployeeService employeeService,
            IScheduleService scheduleService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
            _scheduleService = scheduleService;
        }

        public override async Task ExecuteAsync(object parameter)
        { 
            CalendarHelper calendarHelper = new();
            IList<Employee> employees = new List<Employee>();

            try
            {
                employees = await _scheduleService.GetSchedule();
                LoadDataSchedule(employees, calendarHelper);
            }
            catch(Exception)
            {
                employees = await _employeeService.GetDetails();
                InitialScheduleCreation(employees, calendarHelper);
            }

            //Preferences
            for (int i = 0; i < employees.Count; i++)
            {
                _viewModel.Preferences.Add(employees[i].Preferences);
            }
        }

        private void InitialScheduleCreation(IList<Employee> employees, CalendarHelper calendarHelper)
        {
            ObservableCollection<EmployeeDTO> tempEmployees;
            IList<DateTime> preferenceDays;
            string fullName;
            int freeWorkingDays;


            for (int i = 0; i < employees.Count; i++)
            {
                preferenceDays = new List<DateTime>();
                fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                freeWorkingDays = calendarHelper.WorkingDaysInMonth(employees[i].Preferences.FreeWorkingDays);
                
                for (int j = 0; j < employees[i].Preferences.PreferenceDays.Count; j++)
                {
                    preferenceDays.Add(employees[i].Preferences.PreferenceDays[j].FreeDayChosen);
                }

                _viewModel.Employees.Add(new EmployeeViewDTO
                {
                    FullName = fullName,
                    WorkingDays = freeWorkingDays,
                    PreferenceDays = preferenceDays
                });
            }

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

        private void LoadDataSchedule(IList<Employee> employees, CalendarHelper calendarHelper)
        {
            Dictionary<DateTime, ObservableCollection<EmployeeDTO>> dateEmployeeList = new();
            IList<DateTime> preferenceDays;
            Employee employee;
            Day workDay;
            bool day, swing, night;
            int freeWorkingDays;
            string fullName;
            string shift;

            for (int i = 0; i < employees.Count; i++)
            {
                preferenceDays = new List<DateTime>();
                fullName = String.Concat(employees[i].Name, " ", employees[i].LastName);
                freeWorkingDays = calendarHelper.WorkingDaysInMonth(employees[i].Preferences.FreeWorkingDays);

                for (int j = 0; j < employees[i].Days.Count; j++)
                {
                    if (employees[i].Days[j].IsWorking == true)
                        freeWorkingDays--;
                }

                for (int j = 0; j < employees[i].Preferences.PreferenceDays.Count; j++)
                {
                    preferenceDays.Add(employees[i].Preferences.PreferenceDays[j].FreeDayChosen);
                }

                _viewModel.Employees.Add(new EmployeeViewDTO
                {
                    FullName = fullName,
                    WorkingDays = freeWorkingDays,
                    PreferenceDays = preferenceDays
                });
            }

            for (int i = 0; i < employees.Count; i++)
            {
                employee = employees[i];
                fullName = String.Concat(employee.Name, " ", employee.LastName);

                for (int j = 0; j < employee.Days.Count; j++)
                {
                    day = false;
                    swing = false;
                    night = false;
                    shift = "";

                    workDay = employee.Days[j];

                    if (workDay.Shift == "Day")
                    {
                        day = true;
                        shift = workDay.Shift;
                    }

                    if (workDay.Shift == "Swing")
                    {
                        swing = true;
                        shift = workDay.Shift;
                    }

                    if (workDay.Shift == "Night")
                    {
                        night = true;
                        shift = workDay.Shift;
                    }


                    if (dateEmployeeList.ContainsKey(workDay.WorkingDay.Date))
                    {
                        dateEmployeeList[workDay.WorkingDay.Date].Add(
                            new EmployeeDTO()
                            {
                                CalendarDateDTOId = j,
                                EmployeeId = i,
                                FullName = fullName,
                                Shift = shift,
                                Day = day,
                                Swing = swing,
                                Night = night
                            });

                    }
                    else
                    {
                        dateEmployeeList.Add(workDay.WorkingDay.Date,
                            new ObservableCollection<EmployeeDTO>()
                            {
                                new EmployeeDTO()
                                {
                                    CalendarDateDTOId = j,
                                    EmployeeId = i,
                                    FullName = fullName,
                                    Shift = shift,
                                    Day = day,
                                    Swing = swing,
                                    Night = night
                                }
                            });
                    }
                }
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

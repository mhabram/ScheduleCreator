using GalaSoft.MvvmLight.Command;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.GenerateToExcel;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(IEmployeeService employeeService, IScheduleService scheduleService, IPreferenceService preferenceService)
        {
            GetCalendarEmployeeDetailsCommand = new GetCalendarEmployeeDetailsCommand(this, employeeService, scheduleService, preferenceService);
            GenerateCSVFileCommand = new GenerateCSVFileCommand(this, scheduleService);
            CalendarUpdateCommand = new CalendarUpdateCommand(this, scheduleService);
            CalendarUpdateDayShiftCommand = new RelayCommand<EmployeeDTO>(UpdateDayShift);
            CalendarUpdateSwingShiftCommand = new RelayCommand<EmployeeDTO>(UpdateSwingShift);
            CalendarUpdateNightShiftCommand = new RelayCommand<EmployeeDTO>(UpdateNightShift);
        }

        private ObservableCollection<CalendarDateDTO> _calendarDates;
        public ObservableCollection<CalendarDateDTO> CalendarDates
        {
            get { return _calendarDates ?? (_calendarDates = new ObservableCollection<CalendarDateDTO>()); }
            set
            {
                SetProperty(ref _calendarDates, value);
                OnPropertyChanged(nameof(CalendarDates));
            }
        }

        private ObservableCollection<EmployeeViewDTO> _employees;
        public ObservableCollection<EmployeeViewDTO> Employees
        {
            get { return _employees ?? (_employees = new ObservableCollection<EmployeeViewDTO>()); }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        private List<Preferences> _preferences;
        public List<Preferences> Preferences
        {
            get { return _preferences ?? (_preferences = new List<Preferences>()); }
            set
            {
                _preferences = value;
            }
        }

        private void UpdateDayShift(EmployeeDTO employeeDTO)
        {
            // need to add exception while employee is working 5 days in a row.
            string shift = "Day";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if (((employeeDTO.Date.DayOfWeek.ToString().ToLower() == "saturday") || (employeeDTO.Date.DayOfWeek.ToString().ToLower() == "sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }
                    
                        if ((employeeDTO.Date.DayOfWeek.ToString().ToLower() != "saturday") && (employeeDTO.Date.DayOfWeek.ToString().ToLower() != "sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }
                    }
                }
            }

            if ((employeeDTO.Shift == shift) && !employeeDTO.IsWorking)
                employeeDTO.Shift = "";

            if (employeeDTO.Shift != shift)
                employeeDTO.IsWorking = false;

            //CollectionViewSource.GetDefaultView(Employees).Refresh();
            //CollectionViewSource.GetDefaultView(CalendarDates).Refresh(); // one of this can be deleted. to be checked one more time.
        }

        private void UpdateSwingShift(EmployeeDTO employeeDTO)
        {
            string shift = "Swing";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if (((employeeDTO.Date.DayOfWeek.ToString().ToLower() == "saturday") || (employeeDTO.Date.DayOfWeek.ToString().ToLower() == "sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if ((employeeDTO.Date.DayOfWeek.ToString().ToLower() != "saturday") && (employeeDTO.Date.DayOfWeek.ToString().ToLower() != "sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }
                    }
                }
            }

            if ((employeeDTO.Shift == shift) && !employeeDTO.IsWorking)
                employeeDTO.Shift = "";

            if (employeeDTO.Shift != shift)
                employeeDTO.IsWorking = false;

            //CollectionViewSource.GetDefaultView(Employees).Refresh();
            //CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        }

        private void UpdateNightShift(EmployeeDTO employeeDTO)
        {
            string shift = "Night";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if (((employeeDTO.Date.DayOfWeek.ToString().ToLower() == "saturday") || (employeeDTO.Date.DayOfWeek.ToString().ToLower() == "sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if ((employeeDTO.Date.DayOfWeek.ToString().ToLower() != "saturday") && (employeeDTO.Date.DayOfWeek.ToString().ToLower() != "sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }
                    }
                }
            }

            if ((employeeDTO.Shift == shift) && !employeeDTO.IsWorking)
                employeeDTO.Shift = "";

            if (employeeDTO.Shift != shift)
                employeeDTO.IsWorking = false;

            //CollectionViewSource.GetDefaultView(Employees).Refresh();
            //CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        }

        private bool IsPreferenceDay(EmployeeDTO employeeDTO)
        {
            string lastName = employeeDTO.FullName.Split()[1];
            Preferences preferences = _preferences.Where(e => e.Employee.LastName == lastName).FirstOrDefault();
            return preferences.Dates.Any(d => d.FreeDayChosen == employeeDTO.Date.Date);
        }

        private int GetWorkingDays(EmployeeDTO employeeDTO)
        {
            return _employees.Where(e => e.FullName == employeeDTO.FullName).Select(w => w.WorkingDays).FirstOrDefault();
        }

        private int CountEmployeesInWeekend(Collection<EmployeeDTO> employees, string shift)
        {
            int numberOfEmployeesWorkingOnShift = 0;

            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].Shift == shift)
                {
                    numberOfEmployeesWorkingOnShift++;
                    break;
                }
            }

            return numberOfEmployeesWorkingOnShift;
        }

        private void UpdateViewEmployee(EmployeeDTO employeeDTO)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if ((employeeDTO.FullName == _employees[i].FullName) && (employeeDTO.IsWorking))
                {
                    _employees[i].WorkingDays--;
                    break;
                }

                if ((employeeDTO.FullName == _employees[i].FullName) && (!employeeDTO.IsWorking))
                {
                    _employees[i].WorkingDays++;
                    break;
                }
            }
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
        public ICommand GenerateCSVFileCommand { get; set; }
        public ICommand CalendarUpdateCommand { get; set; }
        public ICommand CalendarUpdateDayShiftCommand { get; private set; }
        public ICommand CalendarUpdateSwingShiftCommand { get; private set; }
        public ICommand CalendarUpdateNightShiftCommand { get; private set; }
    }
}

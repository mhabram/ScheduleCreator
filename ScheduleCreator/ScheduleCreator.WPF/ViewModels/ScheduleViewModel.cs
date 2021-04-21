using GalaSoft.MvvmLight.Command;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(IEmployeeService employeeService,
            IScheduleService scheduleService)
        {
            GetCalendarEmployeeDetailsCommand = new GetCalendarEmployeeDetailsCommand(this, employeeService, scheduleService);
            GenerateExcelFileCommand = new GenerateExcelFileCommand(scheduleService);
            CalendarUpdateCommand = new CalendarUpdateCommand(this, scheduleService);
            CalendarUpdateDayShiftCommand = new RelayCommand<EmployeeDTO>(UpdateDayShift);
            CalendarUpdateSwingShiftCommand = new RelayCommand<EmployeeDTO>(UpdateSwingShift);
            CalendarUpdateNightShiftCommand = new RelayCommand<EmployeeDTO>(UpdateNightShift);
        }

        private ObservableCollection<CalendarDateDTO> _calendarDates;
        public ObservableCollection<CalendarDateDTO> CalendarDates
        {
            get { return _calendarDates ??= new ObservableCollection<CalendarDateDTO>(); }
            set
            {
                _calendarDates = value;
                OnPropertyChanged(nameof(CalendarDates));
            }
        }

        private ObservableCollection<EmployeeViewDTO> _employees;
        public ObservableCollection<EmployeeViewDTO> Employees
        {
            get { return _employees ??= new ObservableCollection<EmployeeViewDTO>(); }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private List<Preferences> _preferences;
        public List<Preferences> Preferences
        {
            get { return _preferences ??= new List<Preferences>(); }
            set
            {
                _preferences = value;
            }
        }

        private void UpdateDayShift(EmployeeDTO employeeDTO)
        {
            // need to add exception while employee is working 5 days in a row.
            // need to add checking the next day if user add employee to next day and then back to previous.
            string shift = "Day";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);
            EmployeeDTO employeePreviousDay = new();

            if (employeeDTO.CalendarDateDTOId > 0)
                employeePreviousDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId - 1).Employees.Where(e => e.FullName == employeeDTO.FullName).FirstOrDefault();

            if (workingDays > 0)
            {
                if (employeeDTO.Swing) // Swing shift is selected for employee
                {
                    employeeDTO.Day = false;
                    employeeDTO.Night = false;
                }

                if (employeeDTO.Night) // Night shift is selected for emploee
                {
                    employeeDTO.Day = false;
                    employeeDTO.Swing = false;
                }

                if (employeePreviousDay.Shift == "Night") // No break between shifts
                    employeeDTO.Day = false;

                if (employeePreviousDay.Shift == "Swing") // Only 8 hours break between shifts
                    employeeDTO.Day = false;


                if (employeeDTO.Day)
                {

                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                            employeeDTO.Day = false;
                    }
                    else
                        employeeDTO.Day = false;
                }
            }
            else
                employeeDTO.Day = false;

            if ((employeeDTO.Shift == shift) && !employeeDTO.Day)
                employeeDTO.Shift = "";
        }

        private void UpdateSwingShift(EmployeeDTO employeeDTO)
        {
            string shift = "Swing";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);
            EmployeeDTO employeePreviousDay = new();

            if (employeeDTO.CalendarDateDTOId > 0)
                employeePreviousDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId - 1).Employees.Where(e => e.FullName == employeeDTO.FullName).FirstOrDefault();

            if (workingDays > 0)
            {
                if (employeeDTO.Day) // Day shift is selected
                {
                    employeeDTO.Swing = false;
                    employeeDTO.Night = false;
                }

                if (employeeDTO.Night) // Night shift is selected
                {
                    employeeDTO.Swing = false;
                    employeeDTO.Day = false;
                }

                if (employeePreviousDay.Shift == "Night") // Only 8 hours break between shifts
                    employeeDTO.Swing = false;

                if (employeeDTO.Swing)
                {
                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                            employeeDTO.Swing = false;
                    }
                    else
                        employeeDTO.Swing = false;
                }
            }
            else
                employeeDTO.Swing = false;
            
            if ((employeeDTO.Shift == shift) && !employeeDTO.Swing)
                employeeDTO.Shift = "";
        }

        private void UpdateNightShift(EmployeeDTO employeeDTO)
        {
            string shift = "Night";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.Day) // Day shift is seleced
                {
                    employeeDTO.Night = false;
                    employeeDTO.Swing = false;
                }

                if (employeeDTO.Swing) // Swing shift is seleced
                {
                    employeeDTO.Night = false;
                    employeeDTO.Day = false;
                }

                if (employeeDTO.Night)
                {
                    if (!IsPreferenceDay(employeeDTO))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            UpdateViewEmployee(employeeDTO);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                            employeeDTO.Night = false;
                    }
                    else
                        employeeDTO.Night = false;
                }
                if ((employeeDTO.Shift == shift) && !employeeDTO.Night)
                    employeeDTO.Shift = "";
            }
            else
                employeeDTO.Night = false;

        }

        private bool IsPreferenceDay(EmployeeDTO employeeDTO)
        {
            string lastName = employeeDTO.FullName.Split()[1];
            Preferences preferences = _preferences.Where(e => e.Employee.LastName == lastName).FirstOrDefault();
            return preferences.PreferenceDays.Any(d => d.FreeDayChosen.Day == _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId).Date.Day);
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
            bool isWorking = false;

            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                isWorking = true;

            for (int i = 0; i < _employees.Count; i++)
            {
                if ((employeeDTO.FullName == _employees[i].FullName) && isWorking)
                {
                    _employees[i].WorkingDays--;
                    break;
                }

                if ((employeeDTO.FullName == _employees[i].FullName) && isWorking)
                {
                    _employees[i].WorkingDays++;
                    break;
                }
            }
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
        public ICommand GenerateExcelFileCommand { get; set; }
        public ICommand CalendarUpdateCommand { get; set; }
        public ICommand CalendarUpdateDayShiftCommand { get; private set; }
        public ICommand CalendarUpdateSwingShiftCommand { get; private set; }
        public ICommand CalendarUpdateNightShiftCommand { get; private set; }
    }
}

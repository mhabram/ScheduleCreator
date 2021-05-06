using GalaSoft.MvvmLight.Command;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands.ScheduleViewModelCommands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

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
            string shift = "Day";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeePreviousDay = new();
            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Day = WorkDaysInRow(employeeDTO);

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

                    if (!employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                        {
                            MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek.ToString()}'s {shift}.");
                            employeeDTO.Day = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"This day is the employee's preference day. ({calendarDateDTO.Date.Day}/{calendarDateDTO.Date.Month}/{calendarDateDTO.Date.Year})");
                        employeeDTO.Shift = shift;
                        employeeDTO.UpdateEmployeeView(_employees); // need to fix this (it is assigning employee to the weekend day while there is already someone working)
                    }
                    //employeeDTO.Day = false;
                }
            }
            else
                employeeDTO.Day = false;

            if ((employeeDTO.Shift == shift) && !employeeDTO.Day)
            {
                employeeDTO.Shift = "";
                employeeDTO.UpdateEmployeeView(_employees);
            }
        }

        private void UpdateSwingShift(EmployeeDTO employeeDTO)
        {
            string shift = "Swing";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeePreviousDay = new();
            EmployeeDTO employeeNextDay = new();
            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Swing = WorkDaysInRow(employeeDTO);

            employeeNextDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId + 1)
                .Employees.Where(e => e.FullName == employeeDTO.FullName)
                .FirstOrDefault();
            if (employeeDTO.CalendarDateDTOId > 0)
                employeePreviousDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId - 1)
                    .Employees.Where(e => e.FullName == employeeDTO.FullName)
                    .FirstOrDefault();

            if (workingDays > 0)
            {
                if (employeeNextDay.Shift == "Day") // Only 8 hours break between shifts
                    employeeDTO.Swing = false;

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
                    if (!employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                        {
                            MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek.ToString()}'s {shift}.");
                            employeeDTO.Swing = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"This day is the employee's preference day. ({calendarDateDTO.Date.Day}/{calendarDateDTO.Date.Month}/{calendarDateDTO.Date.Year})");
                        employeeDTO.Shift = shift;
                    }
                    //employeeDTO.Swing = false;
                }
            }
            else
                employeeDTO.Swing = false;
            
            if ((employeeDTO.Shift == shift) && !employeeDTO.Swing)
            {
                employeeDTO.Shift = "";
                employeeDTO.UpdateEmployeeView(_employees);
            }
        }

        private void UpdateNightShift(EmployeeDTO employeeDTO)
        {
            string shift = "Night";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeeNextDay = new();
            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Night = WorkDaysInRow(employeeDTO);

            employeeNextDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId + 1)
                .Employees.Where(e => e.FullName == employeeDTO.FullName)
                .FirstOrDefault();

            if (workingDays > 0)
            {
                if (employeeNextDay.Shift == "Day") // No break between shifts
                    employeeDTO.Night = false;
                if (employeeNextDay.Shift == "Swing") // Only 8 hours break between shifts
                    employeeDTO.Night = false;

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
                    if (!employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                    {
                        if ((calendarDateDTO.Date.DayOfWeek.ToString() != "Saturday") && (calendarDateDTO.Date.DayOfWeek.ToString() != "Sunday"))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift < 1))
                        {
                            employeeDTO.Shift = shift;
                            employeeDTO.UpdateEmployeeView(_employees);
                        }

                        if (((calendarDateDTO.Date.DayOfWeek.ToString() == "Saturday") || (calendarDateDTO.Date.DayOfWeek.ToString() == "Sunday")) &&
                            (numnberOfEmployeesWorkingOnShift >= 1))
                        {
                            MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek.ToString()}'s {shift}.");
                            employeeDTO.Night = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"This day is the employee's preference day. ({calendarDateDTO.Date.Day}/{calendarDateDTO.Date.Month}/{calendarDateDTO.Date.Year})");
                        employeeDTO.Shift = shift;
                    }
                }
            }
            else
                employeeDTO.Night = false;

            if ((employeeDTO.Shift == shift) && !employeeDTO.Night)
            {
                employeeDTO.Shift = "";
                employeeDTO.UpdateEmployeeView(_employees);
            }
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

        private bool WorkDaysInRow(EmployeeDTO employeeDTO)
        {
            bool workDaysInRow = true;
            EmployeeDTO employeeRow;
            int daysInRowPrev = 0;
            int daysInRowNext = 0;
            bool prev = true;
            bool next = true;

            for (int i = 1; i <= 5; i++)
            {
                if ((employeeDTO.CalendarDateDTOId - i) >= 0)
                {
                    if (prev)
                    {
                        employeeRow = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId - i)
                            .Employees.FirstOrDefault(e => e.FullName == employeeDTO.FullName);
                        if (employeeRow.Day || employeeRow.Swing || employeeRow.Night)
                            daysInRowPrev++;
                        else
                        {
                            daysInRowPrev = 0;
                            prev = false;
                        }
                    }
                }

                if ((employeeDTO.CalendarDateDTOId + i) <= _calendarDates.Last().Id)
                {
                    if (next)
                    {
                        employeeRow = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId + i)
                            .Employees.FirstOrDefault(e => e.FullName == employeeDTO.FullName);
                        if (employeeRow.Day || employeeRow.Swing || employeeRow.Night)
                            daysInRowNext++;
                        else
                        {
                            daysInRowNext = 0;
                            next = false;
                        }
                    }
                }

                if ((daysInRowPrev + daysInRowNext) >= 5)
                {
                    workDaysInRow = false;
                    break;
                }
            }
            return workDaysInRow;
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
        public ICommand GenerateExcelFileCommand { get; set; }
        public ICommand CalendarUpdateCommand { get; set; }
        public ICommand CalendarUpdateDayShiftCommand { get; private set; }
        public ICommand CalendarUpdateSwingShiftCommand { get; private set; }
        public ICommand CalendarUpdateNightShiftCommand { get; private set; }
    }
}

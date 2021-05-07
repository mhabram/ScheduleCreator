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
            int numberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeePreviousDay = new();

            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Day = WorkDaysInRow(employeeDTO);

            if (workingDays == 0)
                employeeDTO.Day = false;

            employeeDTO.CorrectShift();

            if (employeeDTO.CalendarDateDTOId > 0)
            {
                employeePreviousDay = _calendarDates
                    .ElementAt(employeeDTO.CalendarDateDTOId - 1)
                    .Employees
                    .Where(e => e.FullName == employeeDTO.FullName)
                    .FirstOrDefault();
                    
                switch (employeePreviousDay.Shift)
                {
                    case "Night":
                        employeeDTO.Day = false;
                        break;
                    case "Swing":
                        employeeDTO.Day = false;
                        break;
                    default:
                        break;
                }
            }

            if (employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                MessageBox.Show($"This day is the employee's preference day.");

            if (calendarDateDTO.IsWeekend() && numberOfEmployeesWorkingOnShift >= 1 && employeeDTO.Day)
            {
                MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek}'s {shift}.");
                employeeDTO.Day = false;
            }

            employeeDTO.UpdateEmployee(shift, employeeDTO.Day);
            employeeDTO.UpdateEmployeeView(_employees);
        }

        private void UpdateSwingShift(EmployeeDTO employeeDTO)
        {
            string shift = "Swing";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeeNextDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId + 1).Employees.Where(e => e.FullName == employeeDTO.FullName).FirstOrDefault();
            EmployeeDTO employeePreviousDay = new();
            
            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Swing = WorkDaysInRow(employeeDTO);

            if (workingDays == 0)
                employeeDTO.Swing = false;

            employeeDTO.CorrectShift();
                
            if (employeeDTO.CalendarDateDTOId > 0)
            {
                employeePreviousDay = _calendarDates
                    .ElementAt(employeeDTO.CalendarDateDTOId - 1)
                    .Employees
                    .Where(e => e.FullName == employeeDTO.FullName)
                    .FirstOrDefault();
                
                if (employeePreviousDay.Shift == "Night")
                    employeeDTO.Swing = false;
            }

            if (employeeNextDay.Shift == "Day")
                employeeDTO.Swing = false;

            if (employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                MessageBox.Show($"This day is the employee's preference day.");

            if (calendarDateDTO.IsWeekend() && numberOfEmployeesWorkingOnShift >= 1 && employeeDTO.Swing)
            {
                MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek}'s {shift}.");
                employeeDTO.Swing = false;
            }

            employeeDTO.UpdateEmployee(shift, employeeDTO.Swing);
            employeeDTO.UpdateEmployeeView(_employees);
        }

        private void UpdateNightShift(EmployeeDTO employeeDTO)
        {
            string shift = "Night";
            CalendarDateDTO calendarDateDTO = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId);
            int numberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(calendarDateDTO.Employees, shift);
            int workingDays = employeeDTO.GetWorkingDays(_employees);
            EmployeeDTO employeeNextDay = _calendarDates.ElementAt(employeeDTO.CalendarDateDTOId + 1).Employees.Where(e => e.FullName == employeeDTO.FullName).FirstOrDefault();

            if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                employeeDTO.Night = WorkDaysInRow(employeeDTO);

            if (workingDays == 0)
                employeeDTO.Night = false;

            employeeDTO.CorrectShift();

            switch (employeeNextDay.Shift)
            {
                case "Day":
                    employeeDTO.Night = false;
                    break;
                case "Swing":
                    employeeDTO.Night = false;
                    break;
                default:
                    break;
            }

            if (employeeDTO.IsPreferenceDay(_calendarDates, _preferences))
                MessageBox.Show($"This day is the employee's preference day.");

            if (calendarDateDTO.IsWeekend() && numberOfEmployeesWorkingOnShift >= 1 && employeeDTO.Night)
            {
                MessageBox.Show($"There is already employee working on {calendarDateDTO.Date.DayOfWeek}'s {shift}.");
                employeeDTO.Night = false;
            }

            employeeDTO.UpdateEmployee(shift, employeeDTO.Night);
            employeeDTO.UpdateEmployeeView(_employees);
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

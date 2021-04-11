using GalaSoft.MvvmLight.Command;
using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.GenerateToExcel;
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
        public ScheduleViewModel(IEmployeeService employeeService)
        {
            GetCalendarEmployeeDetailsCommand = new GetCalendarEmployeeDetailsCommand(this, employeeService);
            GenerateCSVFileCommand = new GenerateCSVFileCommand(this);
            CalendarUpdateCommand = new CalendarUpdateCommand(this);
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
                OnPropertyChanged("CalendarDates");
            }
        }

        private ObservableCollection<EmployeeViewDTO> _employees;
        public ObservableCollection<EmployeeViewDTO> Employees
        {
            get { return _employees ?? (_employees = new ObservableCollection<EmployeeViewDTO>()); }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        private void UpdateDayShift(EmployeeDTO employeeDTO)
        {
            List<DateTime> preferenceDates = GetPreferenceDay(employeeDTO); // need to add exception while employee is working 5 days in a row.
            string shift = "Day";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);
            
            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if ((employeeDTO.Date.Day != preferenceDates[0].Day) && (employeeDTO.Date.Day != preferenceDates[1].Day) && (employeeDTO.Date.Day != preferenceDates[2].Day))
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

            CollectionViewSource.GetDefaultView(Employees).Refresh();
            CollectionViewSource.GetDefaultView(CalendarDates).Refresh(); // one of this can be deleted. to be checked one more time.
        }

        private void UpdateSwingShift(EmployeeDTO employeeDTO)
        {
            List<DateTime> preferenceDates = GetPreferenceDay(employeeDTO);
            string shift = "Swing";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if ((employeeDTO.Date.Day != preferenceDates[0].Day) && (employeeDTO.Date.Day != preferenceDates[1].Day) && (employeeDTO.Date.Day != preferenceDates[2].Day))
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

            CollectionViewSource.GetDefaultView(Employees).Refresh();
            CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        }

        private void UpdateNightShift(EmployeeDTO employeeDTO)
        {
            List<DateTime> preferenceDates = GetPreferenceDay(employeeDTO);
            string shift = "Night";
            int index = employeeDTO.Date.Day - 1;
            int numnberOfEmployeesWorkingOnShift = CountEmployeesInWeekend(_calendarDates.ElementAt(index).Employees, shift);
            int workingDays = GetWorkingDays(employeeDTO);

            if (workingDays > 0)
            {
                if (employeeDTO.IsWorking)
                {
                    if ((employeeDTO.Date.Day != preferenceDates[0].Day) && (employeeDTO.Date.Day != preferenceDates[1].Day) && (employeeDTO.Date.Day != preferenceDates[2].Day))
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

            CollectionViewSource.GetDefaultView(Employees).Refresh();
            CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        }

        private int GetWorkingDays(EmployeeDTO employeeDTO)
        {
            foreach (EmployeeViewDTO employeeView in _employees)
            {
                if (employeeDTO.FullName == employeeView.FullName)
                    return employeeView.WorkingDays;
            }
            return 0;
        }

        private int CountEmployeesInWeekend(ICollection<EmployeeDTO> employees, string shift)
        {
            int numberOfEmployeesWorkingOnShift = 0;
            
            foreach (EmployeeDTO e in employees)
            {
                if (e.Shift == shift)
                    numberOfEmployeesWorkingOnShift++;
            }
            return numberOfEmployeesWorkingOnShift;
        }

        private void UpdateViewEmployee(EmployeeDTO employeeDTO)
        {
            foreach (EmployeeViewDTO employeeView in _employees)
            {
                if ((employeeDTO.FullName == employeeView.FullName) && (employeeDTO.IsWorking))
                    employeeView.WorkingDays--;

                if ((employeeDTO.FullName == employeeView.FullName) && (!employeeDTO.IsWorking))
                    employeeView.WorkingDays++;
            }
        }

        private List<DateTime> GetPreferenceDay(EmployeeDTO employeeDTO)
        {
            List<DateTime> preferenceDays = new ();

            foreach (EmployeeDTO e in _calendarDates.ElementAt(1).Employees)
            {
                if (e.FullName == employeeDTO.FullName)
                {
                    foreach (DateTime d in e.PreferenceDays)
                    {
                        preferenceDays.Add(d.Date);
                    }
                    break;
                }
            }

            return preferenceDays;
        }

        private void UpdateData(EmployeeDTO employeeDTO, string shift)
        {
            foreach (CalendarDateDTO c in _calendarDates)
            {
                if (employeeDTO.Date.Day == c.Date.Day)
                {
                    foreach (EmployeeDTO e in c.Employees)
                    {
                        if (employeeDTO.FullName == e.FullName)
                        {
                            e.IsWorking = employeeDTO.IsWorking;

                            if (e.IsWorking)
                                e.Shift = shift;
                            else
                                e.Shift = "";

                            break;
                        }
                    }
                }
            }

            foreach (EmployeeViewDTO employeeView in _employees)
            {
                if ((employeeDTO.FullName == employeeView.FullName) && (employeeDTO.IsWorking))
                    employeeView.WorkingDays--;

                if ((employeeDTO.FullName == employeeView.FullName) && (!employeeDTO.IsWorking))
                    employeeView.WorkingDays++;
            }

            CollectionViewSource.GetDefaultView(Employees).Refresh();
            CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        }
        private bool IsEmployeeWorking(EmployeeDTO newEmployeeDTO, string shift)
        {
            bool isEmployeeWorking = false;

            foreach (CalendarDateDTO c in _calendarDates)
            {
                if (newEmployeeDTO.Date.Day == c.Date.Day)
                {
                    foreach (EmployeeDTO e in c.Employees)
                    {
                        if (newEmployeeDTO.Shift == shift)
                        {
                            isEmployeeWorking = true;
                            break;
                        }
                    }
                }
            }
            return isEmployeeWorking;
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
        public ICommand GenerateCSVFileCommand { get; set; }
        public ICommand CalendarUpdateCommand { get; set; }
        public ICommand CalendarUpdateDayShiftCommand { get; private set; }
        public ICommand CalendarUpdateSwingShiftCommand { get; private set; }
        public ICommand CalendarUpdateNightShiftCommand { get; private set; }



        //private EmployeeDTO _selectedEmployeeDayOne;
        //public EmployeeDTO SelectedEmployeeDayOne
        //{
        //    get { return _selectedEmployeeDayOne; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeDayOne != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeDayOne, value);
        //                SetProperty(ref _selectedEmployeeDayOne, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        //OnPropertyChanged(nameof(SelectedEmployeeDayOne));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private EmployeeDTO _selectedEmployeeDayTwo;
        //public EmployeeDTO SelectedEmployeeDayTwo
        //{
        //    get { return _selectedEmployeeDayTwo; }
        //    set
        //    {
        //        //SetProperty(ref _selectedEmployeeDayTwo, value);
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeDayTwo != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeDayTwo, value);
        //                SetProperty(ref _selectedEmployeeDayTwo, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        OnPropertyChanged(nameof(SelectedEmployeeDayTwo));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private EmployeeDTO _selectedEmployeeDayThree;
        //public EmployeeDTO SelectedEmployeeDayThree
        //{
        //    get { return _selectedEmployeeDayThree; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeDayThree != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeDayThree, value);
        //                SetProperty(ref _selectedEmployeeDayThree, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        OnPropertyChanged(nameof(SelectedEmployeeDayThree));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private EmployeeDTO _selectedEmployeeSwingOne;
        //public EmployeeDTO SelectedEmployeeSwingOne
        //{
        //    get { return _selectedEmployeeSwingOne; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeSwingOne != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeSwingOne, value);
        //                SetProperty(ref _selectedEmployeeSwingOne, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        OnPropertyChanged(nameof(SelectedEmployeeSwingOne));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private EmployeeDTO _selectedEmployeeSwingTwo;
        //public EmployeeDTO SelectedEmployeeSwingTwo
        //{
        //    get { return _selectedEmployeeSwingTwo; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeSwingTwo != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeSwingTwo, value);
        //                SetProperty(ref _selectedEmployeeSwingTwo, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        OnPropertyChanged(nameof(SelectedEmployeeSwingTwo));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private EmployeeDTO _selectedEmployeeNight;
        //public EmployeeDTO SelectedEmployeeNight
        //{
        //    get { return _selectedEmployeeNight; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            if (IsEmployeeWorking(value))
        //                MessageBox.Show($"Employee {value.FullName} is already working on that day. Please select someone else.");
        //            else
        //            {
        //                if (_selectedEmployeeNight != null)
        //                    ChangeSelectedEmployee(_selectedEmployeeNight, value);
        //                SetProperty(ref _selectedEmployeeNight, value);
        //                UpdateCalendarDates(value);
        //            }
        //        }

        //        OnPropertyChanged(nameof(SelectedEmployeeNight));
        //        CollectionViewSource.GetDefaultView(Employees).Refresh();
        //        CollectionViewSource.GetDefaultView(CalendarDates).Refresh();
        //    }
        //}

        //private void UpdateCalendarDates(EmployeeDTO employeeDTO)
        //{
        //    foreach (CalendarDateDTO c in _calendarDates)
        //    {
        //        if (employeeDTO.Date.Day == c.Date.Day)
        //        {
        //            foreach (EmployeeDTO e in c.Employees)
        //            {
        //                if (employeeDTO.FullName == e.FullName)
        //                {
        //                    e.IsWorking = true;
        //                    e.WorkingDays++;

        //                    foreach (EmployeeViewDTO employeeView in _employees)
        //                    {
        //                        if (employeeView.FullName == e.FullName)
        //                            employeeView.WorkingDays--;
        //                    }

        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    OnPropertyChanged(nameof(Employees));
        //}

        //private void ChangeSelectedEmployee(EmployeeDTO employeeDTO, EmployeeDTO newEmloyeeDTO)
        //{
        //    if ((employeeDTO.Date.Day == newEmloyeeDTO.Date.Day) && (employeeDTO.FullName != newEmloyeeDTO.FullName))
        //    {
        //        foreach (CalendarDateDTO c in _calendarDates)
        //        {
        //            if (employeeDTO.Date.Day == c.Date.Day)
        //            {
        //                foreach (EmployeeDTO e in c.Employees)
        //                {
        //                    if (employeeDTO.FullName == e.FullName)
        //                    {
        //                        e.IsWorking = false;
        //                        e.WorkingDays--;

        //                        foreach (EmployeeViewDTO employeeView in _employees)
        //                        {
        //                            if (employeeView.FullName == e.FullName)
        //                                employeeView.WorkingDays++;
        //                        }

        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    OnPropertyChanged(nameof(Employees));
        //}

    }
}

using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(IEmployeeService employeeService)
        {
            GetCalendarEmployeeDetailsCommand = new GetCalendarEmployeeDetailsCommand(this, employeeService);
            CalendarUpdateCommand = new CalendarUpdateCommand(this);
        }

        private ObservableCollection<CalendarDateDTO> _calendarDates;
        public ObservableCollection<CalendarDateDTO> CalendarDates // By adding isSelected property should resolve this issue and BindingList<>()
        {
            get
            {
                return _calendarDates ?? (_calendarDates = new ObservableCollection<CalendarDateDTO>());
            }
            set
            {
                _calendarDates = value;
                OnPropertyChanged(nameof(CalendarDates));
            }
        }

        private ObservableCollection<EmployeeDTO> _employees;
        public ObservableCollection<EmployeeDTO> Employees// By adding isSelected property should resolve this issue.
        {
            get
            {
                return _employees ?? (_employees = new ObservableCollection<EmployeeDTO>());
            }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private ObservableCollection<CalendarDateDTO> _selectedCalendarDate;
        public ObservableCollection<CalendarDateDTO> SelectedCalendarDate
        {
            get
            {
                return _selectedCalendarDate ?? (_selectedCalendarDate = new ObservableCollection<CalendarDateDTO>());
            }
            set
            {
                _selectedCalendarDate = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private EmployeeDTO _selectedEmployee;
        public EmployeeDTO SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
                //return _selectedEmployee ?? (_selectedEmployee = new EmployeeDTO());
            }
            set
            {
                _selectedEmployee = value;
                if (_selectedEmployee != null)
                {
                    UpdateCalendarDates();
                }
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                System.Windows.MessageBox.Show(SelectedDate.ToString());

            }
        }

        public void UpdateCalendarDates()
        {
            foreach (CalendarDateDTO c in _calendarDates)
            {
                if (_selectedEmployee.Date.Day == c.Date.Day)
                {
                    foreach (EmployeeDTO e in c.Employees)
                    {
                        if (_selectedEmployee.FullName == e.FullName)
                        {
                            e.IsWorking = true;
                            e.WorkingDays++;
                        }             
                    }
                }
            }
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
        public ICommand CalendarUpdateCommand { get; set; }
    }
}

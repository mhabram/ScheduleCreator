using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class ScheduleViewModel : ViewModelBase
    {
        public ScheduleViewModel(IEmployeeService employeeService)
        {
            GetCalendarEmployeeDetailsCommand = new GetCalendarEmployeeDetailsCommand(this, employeeService);
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
                OnPropertyChanged(nameof(SelectedCalendarDate));
                System.Windows.MessageBox.Show(SelectedCalendarDate.ToString());
            }
        }

        private EmployeeDTO _selectedEmployee;
        public EmployeeDTO SelectedEmployee
        {
            get
            {
                return _selectedEmployee ?? (_selectedEmployee = new EmployeeDTO());
            }
            set
            {
                _selectedEmployee = value;
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
            }
        }

        public void UpdateSelectedCalendarDate(CalendarDateDTO SelectedCalendarDate)
        {
            System.Windows.MessageBox.Show(SelectedCalendarDate.ToString());
        }

        public ICommand GetCalendarEmployeeDetailsCommand { get; set; }
    }
}

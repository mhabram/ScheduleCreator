using GalaSoft.MvvmLight.Command;
using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands.PreferenceViewModelCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class PreferenceViewModel : ViewModelBase
    {
        public PreferenceViewModel(IPreferenceService preferenceService,
            IEmployeeService employeeService)
        {
            AddPreferenceCommand = new AddPreferenceCommand(this, preferenceService);
            GetEmployeesCommand = new GetEmployeesCommand(this, employeeService);
            GetEmployeePreferencesCommand = new GetEmployeePreferencesCommand(this, preferenceService);
            ClearCommand = new RelayCommand(Clear);
            NewPreferenceFieldCommand = new RelayCommand(NewPreferenceField);
            SuccessMessageViewModel = new MessageViewModel();
            ErrorMessageViewModel = new MessageViewModel();
        }

        private ObservableCollection<EmployeeDTO> _employees;
        public ObservableCollection<EmployeeDTO> Employees
        {
            get { return _employees ??= new ObservableCollection<EmployeeDTO>(); }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private EmployeeDTO _employee;
        public EmployeeDTO Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                GetEmployeePreferencesCommand.Execute(value);
            }
        }

        private ObservableCollection<DateTimeWrapper> _preferenceDays;
        public ObservableCollection<DateTimeWrapper> PreferenceDays
        {
            get
            {
                return _preferenceDays;
            }
            set
            {
                _preferenceDays = value;
                OnPropertyChanged(nameof(PreferenceDays));
            }
        }

        public sbyte _holidays;
        public sbyte Holidays
        {
            get { return _holidays; }
            set
            {
                _holidays = value;
                OnPropertyChanged(nameof(Holidays));
            }
        }

        private DateTime? _from;
        public DateTime? From
        {
            get { return _from; }
            set
            {
                if (CheckFrom(value))
                    _from = value;

                if (_to != null)
                    OnLeave = CountLeave();
                OnPropertyChanged(nameof(From));
            }
        }

        private DateTime? _to;
        public DateTime? To
        {
            get { return _to; }
            set
            {
                if (CheckTo(value))
                    _to = value;

                if (_from != null)
                    OnLeave = CountLeave();
                OnPropertyChanged(nameof(To));
            }
        }

        private int _onLeave;
        public int OnLeave
        {
            get { return _onLeave; }
            set
            {
                _onLeave = value;
                OnPropertyChanged(nameof(OnLeave));
            }
        }

        private void NewPreferenceField()
        {
            PreferenceDays.Add(new DateTimeWrapper());
        }

        private void Clear()
        {
            PreferenceDays = new ObservableCollection<DateTimeWrapper>() { new(), new() };
            From = null;
            To = null;
            Holidays = 0;
        }

        private int CountLeave()
        {
            int leave = 1;

            if ((_from != null) && (_to != null))
            {
                DateTime fromDate = (DateTime)_from;
                DateTime toDate = (DateTime)_to;
                
                if (fromDate.Month < toDate.Month)
                    leave = toDate.Day;
                else if (toDate >= fromDate)
                    leave = leave + toDate.Day - fromDate.Day;

                for (int i = 0; i < leave; i++)
                {
                    if ((fromDate.AddDays(i).DayOfWeek.ToString() == "Sunday") ||
                        (fromDate.AddDays(i).DayOfWeek.ToString() == "Saturday"))
                        leave--;
                }
            }
            else
                leave = 0;

            return leave;
        }

        private bool CheckFrom(DateTime? fromDateValue)
        {
            bool isOrder = true;
            DateTime from = new();
            DateTime to = new();
            
            if (fromDateValue != null)
                from = (DateTime) fromDateValue;
            if (_to != null)
                to = (DateTime) _to;
            else
            {
                ErrorMessage = null;
                return isOrder;
            }

            if (from > to)
            {
                ErrorMessage = "Field From cannot be higher than field To.";
                isOrder = false;
            }
            else
                ErrorMessage = null;

            return isOrder;
        }

        private bool CheckTo(DateTime? toDateValue)
        {
            bool isOrder = true;
            DateTime from = new();
            DateTime to = new();

            if (toDateValue != null)
                to = (DateTime) toDateValue;
            if (_from != null)
                from = (DateTime) _from;
            else
            {
                ErrorMessage = null;
                return isOrder;
            }

            if (from > to)
            {
                ErrorMessage = "Field To cannot be lower than field From.";
                isOrder = false;
            }
            else
                ErrorMessage = null;

            return isOrder;
        }

        public MessageViewModel SuccessMessageViewModel { get; }
        public string SuccessMessage
        {
            set => SuccessMessageViewModel.Message = value;
        }

        public MessageViewModel ErrorMessageViewModel { get; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand AddPreferenceCommand { get; set; }
        public ICommand GetEmployeesCommand { get; set; }
        public ICommand GetEmployeePreferencesCommand { get; set; }

        public ICommand ClearCommand { get; private set; }
        public ICommand NewPreferenceFieldCommand { get; private set; }
    }
}

using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands.PreferenceViewModelCommands;
using System;
using System.Collections.ObjectModel;
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

        private DateTime _dayOffOne = DateTime.Now.AddMonths(1);
        public DateTime DayOffOne
        {
            get { return _dayOffOne; }
            set
            {
                _dayOffOne = value;
                OnPropertyChanged(nameof(DayOffOne));
            }
        }

        private DateTime _dayOffTwo = DateTime.Now.AddMonths(1);
        public DateTime DayOffTwo
        {
            get { return _dayOffTwo; }
            set
            {
                _dayOffTwo = value;
                OnPropertyChanged(nameof(DayOffTwo));
            }
        }

        private DateTime _dayOffThree = DateTime.Now.AddMonths(1);
        public DateTime DayOffThree
        {
            get { return _dayOffThree; }
            set
            {
                _dayOffThree = value;
                OnPropertyChanged(nameof(DayOffThree));
            }
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
    }
}

using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands.EmployeeViewModelCommands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class EmployeeViewModel : ViewModelBase
    {
        public EmployeeViewModel(IEmployeeService employeeService)
        {
            AddEmployeeCommand = new AddEmployeeCommand(this, employeeService);
            EmployeeListCommand = new EmployeeListCommand(this, employeeService);
            RemoveEmployeeCommand = new RemoveEmployeeCommand(this, employeeService);

            SuccessMessageViewModel = new MessageViewModel();
            ErrorMessageViewModel = new MessageViewModel();
            SuccessDeletedEmployeeMessageViewModel = new MessageViewModel();
            ErrorDeletedEmployeeMessageViewModel = new MessageViewModel();
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees ??= new ObservableCollection<Employee>(); }
            set
            {
                _employees = value;
                //OnPropertyChanged(nameof(Employees));
            }
        }

        private Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
            }
        }

        public string _name;
        public string Name
        {
            get
            { return _name; }
            set
            {
                _name = value;
            }
        }

        public string _lastName;
        public string LastName
        {
            get
            { return _lastName; }
            set
            {
                _lastName = value;
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

        public MessageViewModel SuccessDeletedEmployeeMessageViewModel { get; }
        public string SuccessDeletedEmployee
        {
            set => SuccessDeletedEmployeeMessageViewModel.Message = value;
        }

        public MessageViewModel ErrorDeletedEmployeeMessageViewModel { get; }
        public string ErrorDeletedEmployee
        {
            set => ErrorDeletedEmployeeMessageViewModel.Message = value;
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EmployeeListCommand { get; set; }
        public ICommand RemoveEmployeeCommand { get; set; }
    }
}

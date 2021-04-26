using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class CreateScheduleViewModel : ViewModelBase
    {
        public CreateScheduleViewModel(IEmployeeService employeeService)
        {
            GetEmployeeDetailsCommand = new GetEmployeeDetailsCommand(this, employeeService);
            //RemoveEmployeesCommand = new RemoveEmployeesCommand(this, employeeService);
            CreateScheduleCommand = new CreateScheduleCommand(this, employeeService);
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ICommand GetEmployeeDetailsCommand { get; set; }
        public ICommand RemoveEmployeesCommand { get; set; }
        public ICommand CreateScheduleCommand { get; set; }

    }
}

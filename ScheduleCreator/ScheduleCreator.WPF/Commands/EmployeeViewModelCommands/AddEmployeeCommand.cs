using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace ScheduleCreator.WPF.Commands.EmployeeViewModelCommands
{
    class AddEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeeViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public AddEmployeeCommand(EmployeeViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Employee employee = new();

            _viewModel.SuccessMessage = null;
            _viewModel.ErrorMessage = null;
            _viewModel.SuccessDeletedEmployee = null;
            _viewModel.ErrorDeletedEmployee = null;

            if ((_viewModel.Name != null) || (_viewModel.LastName != null)) // has to be changed later probably min string size to 2 letters fix this.
            {
                employee = await _employeeService.AddEmployee(_viewModel.Name, _viewModel.LastName);
                _viewModel.Employees.Add(employee);
                _viewModel.SuccessMessage = $"{_viewModel.Name} has been added."; // Singleton Sean #21 from ~13:30 catching specific error messages. tbd in future.
            }
            else
                _viewModel.ErrorMessage = "Can not add employee without name and lastname.";
        }
    }
}

using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands.EmployeeViewModelCommands
{
    class RemoveEmployeeCommand : AsyncCommandBase
    {
        private readonly EmployeeViewModel _viewModel;
        private readonly IEmployeeService _empoyeeService;

        public RemoveEmployeeCommand(EmployeeViewModel viewModel, IEmployeeService empoyeeService)
        {
            _viewModel = viewModel;
            _empoyeeService = empoyeeService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.SuccessMessage = null;
            _viewModel.ErrorMessage = null;
            _viewModel.SuccessDeletedEmployee = null;
            _viewModel.ErrorDeletedEmployee = null;

            try
            {
                await _empoyeeService.RemoveEmployee(_viewModel.Employee);
                _viewModel.Employees.Remove(_viewModel.Employee);
                _viewModel.SuccessDeletedEmployee = "Employee has been deleted.";
            }
            catch (Exception)
            {
                _viewModel.ErrorDeletedEmployee = "Employee couldn't be deleted. Please try again.";
            }
        }
    }
}

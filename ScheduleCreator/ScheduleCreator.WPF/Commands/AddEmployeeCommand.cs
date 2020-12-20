using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class AddEmployeeCommand : ICommand
    {
        private readonly EmployeeViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public AddEmployeeCommand(EmployeeViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            //TBC
            Object employee = await _employeeService.AddEmployee(_viewModel.Name, _viewModel.LastName);
        }
    }
}

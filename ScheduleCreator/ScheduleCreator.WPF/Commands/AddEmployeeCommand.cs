using ScheduleCreator.Domain.Models;
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
            if ((_viewModel.Name != null) || (_viewModel.LastName != null)) // has to be changed later probably min string size to 2 letters fix this.
            {
                await _employeeService.AddEmployee(_viewModel.Name, _viewModel.LastName);
                System.Windows.MessageBox.Show($"{_viewModel.Name} has been added to database.");
            }
            else
                System.Windows.MessageBox.Show("Can not add employee without name and lastname.");
            
        }
    }
}

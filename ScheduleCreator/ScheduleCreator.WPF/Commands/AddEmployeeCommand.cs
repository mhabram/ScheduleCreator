using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
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
            if ((_viewModel.Name != null) || (_viewModel.LastName != null)) // has to be changed later probably min string size to 2 letters fix this.
            {
                await _employeeService.AddEmployee(_viewModel.Name, _viewModel.LastName);
                _viewModel.SuccessMessage = $"{_viewModel.Name} has been added."; // Singleton Sean #21 from ~13:30 catching specific error messages. tbd in future.
                //MessageBox.Show($"{_viewModel.Name} has been added to database.");
            }
            else
                MessageBox.Show("Can not add employee without name and lastname.");
        }
    }
}

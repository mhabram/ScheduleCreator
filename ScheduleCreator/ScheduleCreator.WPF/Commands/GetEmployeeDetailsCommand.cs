using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class GetEmployeeDetailsCommand : ICommand
    {
        private readonly CreateScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public GetEmployeeDetailsCommand(CreateScheduleViewModel viewModel, IEmployeeService employeeService)
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
            //_viewModel.Employees = await _employeeService.GetDetails();
        }
    }
}
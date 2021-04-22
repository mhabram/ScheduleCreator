using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class GetEmployeeDetailsCommand : AsyncCommandBase
    {
        private readonly CreateScheduleViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public GetEmployeeDetailsCommand(CreateScheduleViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //_viewModel.Employees = await _employeeService.GetDetails();
        }
    }
}
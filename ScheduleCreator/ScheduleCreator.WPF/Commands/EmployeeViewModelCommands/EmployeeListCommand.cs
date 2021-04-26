using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.Commands.EmployeeViewModelCommands
{
    class EmployeeListCommand : AsyncCommandBase
    {
        private readonly EmployeeViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public EmployeeListCommand(EmployeeViewModel viewModel,
            IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public override async Task ExecuteAsync(object parametr)
        {
            try
            {
                IList<Employee> employees = await _employeeService.GetEmployees();

                for (int i = 0; i < employees.Count; i++)
                {
                    _viewModel.Employees.Add(employees[i]);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.Commands.PreferenceViewModelCommands
{
    class GetEmployeesCommand : AsyncCommandBase
    {
        private readonly PreferenceViewModel _viewModel;
        private readonly IEmployeeService _employeeService;

        public GetEmployeesCommand(PreferenceViewModel viewModel, IEmployeeService employeeService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
        }

        public override async Task ExecuteAsync(object parametr)
        {
            ObservableCollection<EmployeeDTO> employeesDTO = new();

            try
            {
                IList<Employee> employees = await _employeeService.GetEmployees();

                for (int i = 0; i < employees.Count; i++)
                {
                    employeesDTO.Add(new EmployeeDTO
                    {
                        Id = employees[i].EmployeeId,
                        FullName = string.Concat(employees[i].Name, " ", employees[i].LastName)
                    });
                }

                _viewModel.Employees = employeesDTO;
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "There are no employees in the database.";
            }
        }
    }
}

using ScheduleCreator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class EmployeeViewModelFactory : IScheduleCreatorViewModelFactory<EmployeeViewModel>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeViewModelFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public EmployeeViewModel CreateViewModel()
        {
            return new EmployeeViewModel(_employeeService);
        }
    }
}

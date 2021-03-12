using ScheduleCreator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class CreateScheduleViewModelFactory : IScheduleCreatorViewModelFactory<CreateScheduleViewModel>
    {
        private readonly IEmployeeService _employeeService;

        public CreateScheduleViewModelFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public CreateScheduleViewModel CreateViewModel()
        {
            return new CreateScheduleViewModel(_employeeService);
        }
    }
}

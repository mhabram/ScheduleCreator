using ScheduleCreator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class ScheduleViewModelFactory : IScheduleCreatorViewModelFactory<ScheduleViewModel>
    {
        private readonly IEmployeeService _employeeService;

        public ScheduleViewModelFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ScheduleViewModel CreateViewModel()
        {
            return new ScheduleViewModel(_employeeService);
        }
    }
}

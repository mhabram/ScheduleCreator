using ScheduleCreator.Domain.GenerateToExcel;
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
        private readonly IScheduleService _scheduleService;

        public ScheduleViewModelFactory(IEmployeeService employeeService,
            IScheduleService scheduleService)
        {
            _employeeService = employeeService;
            _scheduleService = scheduleService;
        }

        public ScheduleViewModel CreateViewModel()
        {
            return new ScheduleViewModel(_employeeService, _scheduleService);
        }
    }
}

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
        private readonly IPreferenceService _preferenceService;

        public ScheduleViewModelFactory(IEmployeeService employeeService, IScheduleService scheduleService, IPreferenceService preferenceService)
        {
            _employeeService = employeeService;
            _scheduleService = scheduleService;
            _preferenceService = preferenceService;
        }

        public ScheduleViewModel CreateViewModel()
        {
            return new ScheduleViewModel(_employeeService, _scheduleService, _preferenceService);
        }
    }
}

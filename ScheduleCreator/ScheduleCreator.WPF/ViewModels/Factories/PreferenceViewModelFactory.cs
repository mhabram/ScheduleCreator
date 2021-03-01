using ScheduleCreator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class PreferenceViewModelFactory : IScheduleCreatorViewModelFactory<PreferenceViewModel>
    {
        private readonly IPreferenceService _preferenceService;
        private readonly IEmployeeService _employeeService;
        private readonly IDateService _dateService;

        public PreferenceViewModelFactory(IPreferenceService preferenceService, IDateService dateService, IEmployeeService employeeService)
        {
            _preferenceService = preferenceService;
            _dateService = dateService;
            _employeeService = employeeService;
        }

        public PreferenceViewModel CreateViewModel()
        {
            return new PreferenceViewModel(_preferenceService, _employeeService, _dateService);
        }
    }
}

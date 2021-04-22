using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class AddPreferenceCommand : AsyncCommandBase
    {
        private readonly PreferenceViewModel _viewModel;
        private readonly IPreferenceService _preferenceService;
        private readonly IEmployeeService _employeeService;
        private readonly IPreferenceDayService _preferenceDayService;

        public AddPreferenceCommand(PreferenceViewModel viewModel,
            IPreferenceService preferenceService,
            IEmployeeService employeeService,
            IPreferenceDayService preferenceDayService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
            _preferenceService = preferenceService;
            _preferenceDayService = preferenceDayService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.LastName != null)
            {
                int employeId = await _employeeService.GetEmployee(_viewModel.LastName);
                
                if (employeId != -1)
                {
                    Preferences prefernces = await _preferenceService.AddPreference(employeId, _viewModel.Holidays);
                    await _preferenceDayService.AddPreferenceDay(_viewModel.DayOff1, prefernces.PreferencesId);
                    await _preferenceDayService.AddPreferenceDay(_viewModel.DayOff2, prefernces.PreferencesId);
                    await _preferenceDayService.AddPreferenceDay(_viewModel.DayOff3, prefernces.PreferencesId);

                    System.Windows.MessageBox.Show("Preferences has been applied to the employee.");
                }
                else
                    System.Windows.MessageBox.Show($"Employee {_viewModel.LastName} doesn't exists");
            }
            else
                System.Windows.MessageBox.Show("Provide last name of the employee");
        }
    }
}

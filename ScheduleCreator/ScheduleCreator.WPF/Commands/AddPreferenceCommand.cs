using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    class AddPreferenceCommand : ICommand
    {
        private readonly PreferenceViewModel _viewModel;
        private readonly IPreferenceService _preferenceService;
        private readonly IEmployeeService _employeeService;
        private readonly IDateService _dateService;

        public AddPreferenceCommand(PreferenceViewModel viewModel, IPreferenceService preferenceService, IEmployeeService employeeService, IDateService dateService)
        {
            _viewModel = viewModel;
            _employeeService = employeeService;
            _preferenceService = preferenceService;
            _dateService = dateService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        
        public async void Execute(object parameter)
        {
            if (_viewModel.LastName != null)
            {
                int employeId = await _employeeService.GetEmployee(_viewModel.LastName);
                
                if (employeId != -1)
                {
                    Preferences prefernces = await _preferenceService.AddPreference(employeId, _viewModel.Holidays);
                    await _dateService.AddDate(_viewModel.DayOff1, prefernces.PreferencesId);
                    await _dateService.AddDate(_viewModel.DayOff2, prefernces.PreferencesId);
                    await _dateService.AddDate(_viewModel.DayOff3, prefernces.PreferencesId);

                    System.Windows.MessageBox.Show("Preferences has been applied to the employee.");
                }

                System.Windows.MessageBox.Show($"Employee {_viewModel.LastName} doesn't exists");
            }
            else
                System.Windows.MessageBox.Show("Provide last name of the employee");
        }
    }
}

using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands.PreferenceViewModelCommands
{
    class AddPreferenceCommand : AsyncCommandBase
    {
        private readonly PreferenceViewModel _viewModel;
        private readonly IPreferenceService _preferenceService;

        public AddPreferenceCommand(PreferenceViewModel viewModel,
            IPreferenceService preferenceService)
        {
            _viewModel = viewModel;
            _preferenceService = preferenceService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = null;
            _viewModel.SuccessMessage = null;
            // to be fixed whole saving/updating preferences.
            try
            {
                //need to add exception if employee is null 
                if (_viewModel.Employee.Preferences != null)
                {
                    await _preferenceService.UpdatePreferences(
                        _viewModel.Employee.Id,
                        _viewModel.PreferenceDays,
                        _viewModel.From,
                        _viewModel.To,
                        _viewModel.OnLeave,
                        _viewModel.Holidays);
                    _viewModel.SuccessMessage = "Preferences has been updated.";

                }
                else
                {
                    await _preferenceService.AddPreferences(
                        _viewModel.Employee.Id,
                        _viewModel.PreferenceDays,
                        _viewModel.From,
                        _viewModel.To,
                        _viewModel.OnLeave,
                        _viewModel.Holidays);
                    _viewModel.SuccessMessage = "Preferences has been added.";
                }
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Please check if everything is correct. Couldn't add or update preferences.";
            }
        }
    }
}

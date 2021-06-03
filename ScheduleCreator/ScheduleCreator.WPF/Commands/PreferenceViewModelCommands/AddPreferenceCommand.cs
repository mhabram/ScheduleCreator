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
            IList<PreferenceDay> preferenceDays = new List<PreferenceDay>();
            PreferenceDay preferenceDay;

            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffOne };
            preferenceDays.Add(preferenceDay);
            
            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffTwo };
            preferenceDays.Add(preferenceDay);
            
            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffThree };
            preferenceDays.Add(preferenceDay);

            _viewModel.ErrorMessage = null;
            _viewModel.SuccessMessage = null;

            try
            {
                //need to add exception if employee is null 
                if (_viewModel.Employee.Preferences != null)
                {
                    await _preferenceService.UpdatePreferences(
                        _viewModel.Employee.Id,
                        preferenceDays,
                        _viewModel.From,
                        _viewModel.To,
                        _viewModel.Holidays);
                    _viewModel.SuccessMessage = "Preferences has been updated.";

                }
                else
                {
                    await _preferenceService.AddPreferences(
                        _viewModel.Employee.Id,
                        preferenceDays,
                        _viewModel.From,
                        _viewModel.To,
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

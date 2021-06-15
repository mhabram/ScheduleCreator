using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.Commands.PreferenceViewModelCommands
{
    class GetEmployeePreferencesCommand : AsyncCommandBase
    {
        private readonly PreferenceViewModel _viewModel;
        private readonly IPreferenceService _preferenceService;

        public GetEmployeePreferencesCommand(PreferenceViewModel viewModel,
            IPreferenceService preferenceService)
        {
            _viewModel = viewModel;
            _preferenceService = preferenceService;
        }

        public override async Task ExecuteAsync(object parametr)
        {
            //Making status clear.
            _viewModel.ErrorMessage = null;
            _viewModel.SuccessMessage = null;
            _viewModel.From = null;
            _viewModel.To = null;
            _viewModel.PreferenceDays = new() { new(), new()};

            try
            {
                EmployeeDTO employeeDTO = (EmployeeDTO)parametr;
                ObservableCollection<DateTimeWrapper> preferenceDays = new();
                Preferences preferences = await _preferenceService.GetPreferences(employeeDTO.Id);
                CultureInfo culture = new CultureInfo("en-NZ");
                int leaveDays = 0;

                if (preferences != null)
                {
                    for (int i = 0; i < preferences.PreferenceDays.Count; i++)
                    {
                        preferenceDays.Add(new() { Value = preferences.PreferenceDays[i].FreeDayChosen });
                    }

                    if ((preferences.From != "") && (preferences.To != ""))
                    {
                        _viewModel.From = Convert.ToDateTime(preferences.From, culture);
                        _viewModel.To = Convert.ToDateTime(preferences.To, culture);
                        leaveDays = preferences.LeaveDays;
                    }
                    else
                    {
                        _viewModel.From = null;
                        _viewModel.To = null;
                    }

                    _viewModel.OnLeave = leaveDays;
                    _viewModel.PreferenceDays = preferenceDays;
                    _viewModel.Holidays = preferences.FreeWorkingDays;
                    _viewModel.Employee.Preferences = new Preferences() { PreferencesId = preferences.PreferencesId };
                }
                else
                {
                    _viewModel.Holidays = 0;
                    _viewModel.ErrorMessage = "This user do not have choosen preferences yet.";
                }

            }
            catch (Exception e)
            {
                _viewModel.ErrorMessage = e.ToString();
            }
        }
    }
}

using ScheduleCreator.Domain.DTO.PreferenceView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
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
            try
            {
                EmployeeDTO employeeDTO = (EmployeeDTO)parametr;
                List<PreferenceDay> preferenceDay = new();
                Preferences preferences = await _preferenceService.GetPreferences(employeeDTO.Id);

                if (preferences != null)
                {
                    for (int i = 0; i < preferences.PreferenceDays.Count; i++)
                    {
                        preferenceDay.Add(preferences.PreferenceDays[i]);
                    }

                    _viewModel.DayOffOne = preferenceDay[0].FreeDayChosen;
                    _viewModel.DayOffTwo = preferenceDay[1].FreeDayChosen;
                    _viewModel.DayOffThree = preferenceDay[2].FreeDayChosen;
                    _viewModel.Holidays = preferences.FreeWorkingDays;
                    _viewModel.Employee.Preferences = new Preferences() { PreferencesId = preferences.PreferencesId };
                }
                else
                {
                    _viewModel.DayOffOne = DateTime.Now.AddMonths(1);
                    _viewModel.DayOffTwo = DateTime.Now.AddMonths(1);
                    _viewModel.DayOffThree = DateTime.Now.AddMonths(1);
                    _viewModel.Holidays = 0;
                }

            }
            catch (Exception)
            {

            }
        }
    }
}

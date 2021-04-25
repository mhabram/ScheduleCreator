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
            IList<PreferenceDay> preferenceDays = new List<PreferenceDay>();
            PreferenceDay preferenceDay;

            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffOne };
            preferenceDays.Add(preferenceDay);
            
            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffTwo };
            preferenceDays.Add(preferenceDay);
            
            preferenceDay = new PreferenceDay() { FreeDayChosen = _viewModel.DayOffThree };
            preferenceDays.Add(preferenceDay);
            
            try
            {
                if (_viewModel.Employee.Preferences != null)
                    await _preferenceService.UpdatePreferences(_viewModel.Employee.Id, preferenceDays, _viewModel.Holidays);
                else
                {
                    await _preferenceService.AddPreferences(_viewModel.Employee.Id, preferenceDays, _viewModel.Holidays);
                }
            }
            catch (Exception)
            {

            }

                    //System.Windows.MessageBox.Show("Preferences has been applied to the employee.");
            //if (_viewModel.LastName != null)
            //{
            //    int employeId = await _employeeService.GetEmployee(_viewModel.LastName);
                
                //if (employeId != -1)
                //{
                    
                //}
            //    else
            //        System.Windows.MessageBox.Show($"Employee {_viewModel.LastName} doesn't exists");
            //}
            //else
            //    System.Windows.MessageBox.Show("Provide last name of the employee");
        }
    }
}

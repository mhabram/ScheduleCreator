using ScheduleCreator.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class ScheduleCreatorViewModelFactory : IScheduleCreatorViewModelFactory
    {
        private readonly CreateViewModel<HelpViewModel> _createHelpViewModelFactory;
        private readonly CreateViewModel<CreateScheduleViewModel> _createCreateScheduleViewModel; // to be deleted probably
        private readonly CreateViewModel<PreferenceViewModel> _createPreferenceViewModel;
        private readonly CreateViewModel<EmployeeViewModel> _createEmployeeViewModel;
        private readonly CreateViewModel<ScheduleViewModel> _createScheduleViewModel;

        public ScheduleCreatorViewModelFactory(CreateViewModel<HelpViewModel> createHelpViewModelFactory,
            CreateViewModel<CreateScheduleViewModel> createCreateScheduleViewModel,
            CreateViewModel<PreferenceViewModel> createPreferenceViewModel,
            CreateViewModel<EmployeeViewModel> createEmployeeViewModel,
            CreateViewModel<ScheduleViewModel> createScheduleViewModel)
        {
            _createHelpViewModelFactory = createHelpViewModelFactory;
            _createCreateScheduleViewModel = createCreateScheduleViewModel;
            _createPreferenceViewModel = createPreferenceViewModel;
            _createEmployeeViewModel = createEmployeeViewModel;
            _createScheduleViewModel = createScheduleViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Preference:
                    return _createPreferenceViewModel();
                case ViewType.CreateSchedule:
                    return _createCreateScheduleViewModel();
                case ViewType.Employee:
                    return _createEmployeeViewModel();
                case ViewType.Help:
                    return _createHelpViewModelFactory();
                case ViewType.Schedule:
                    return _createScheduleViewModel();
                default:
                    throw new ArgumentException("The ViewType doesn't have a ViewModel.", "viewType");
            }
        }
    }
}

using ScheduleCreator.WPF.State.Navigators;
using System;
namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class ScheduleCreatorViewModelFactory : IScheduleCreatorViewModelFactory
    {
        private readonly CreateViewModel<HelpViewModel> _createHelpViewModelFactory;
        private readonly CreateViewModel<PreferenceViewModel> _createPreferenceViewModel;
        private readonly CreateViewModel<EmployeeViewModel> _createEmployeeViewModel;
        private readonly CreateViewModel<ScheduleViewModel> _createScheduleViewModel;

        public ScheduleCreatorViewModelFactory(CreateViewModel<HelpViewModel> createHelpViewModelFactory,
            CreateViewModel<PreferenceViewModel> createPreferenceViewModel,
            CreateViewModel<EmployeeViewModel> createEmployeeViewModel,
            CreateViewModel<ScheduleViewModel> createScheduleViewModel)
        {
            _createHelpViewModelFactory = createHelpViewModelFactory;
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
                case ViewType.Employee:
                    return _createEmployeeViewModel();
                case ViewType.Help:
                    return _createHelpViewModelFactory();
                case ViewType.Schedule:
                    return _createScheduleViewModel();
                default:
                    throw new ArgumentException("The ViewType doesn't have a ViewModel.", nameof(viewType));
            }
        }
    }
}

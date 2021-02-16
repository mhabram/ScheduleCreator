using ScheduleCreator.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class RootScheduleCreatorViewModelFactory : IRootScheduleCreatorViewModelFactory
    {
        private readonly IScheduleCreatorViewModelFactory<HelpViewModel> _helpViewModelFactory;
        private readonly IScheduleCreatorViewModelFactory<CreateScheduleViewModel> _createScheduleViewModel;
        private readonly IScheduleCreatorViewModelFactory<PreferenceViewModel> _preferenceViewModel;
        private readonly IScheduleCreatorViewModelFactory<EmployeeViewModel> _employeeViewModel;

        public RootScheduleCreatorViewModelFactory(IScheduleCreatorViewModelFactory<HelpViewModel> helpViewModelFactory,
            IScheduleCreatorViewModelFactory<CreateScheduleViewModel> createScheduleViewModel,
            IScheduleCreatorViewModelFactory<PreferenceViewModel> preferenceViewModel,
            IScheduleCreatorViewModelFactory<EmployeeViewModel> employeeViewModel)
        {
            _helpViewModelFactory = helpViewModelFactory;
            _employeeViewModel = employeeViewModel;
            _createScheduleViewModel = createScheduleViewModel;
            _preferenceViewModel = preferenceViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Preference:
                    return _preferenceViewModel.CreateViewModel();
                case ViewType.CreateSchedule:
                    return _createScheduleViewModel.CreateViewModel();
                case ViewType.Employee:
                    return _employeeViewModel.CreateViewModel();
                case ViewType.Help:
                    return _helpViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType doesn't have a ViewModel.", "viewType");
            }
        }
    }
}

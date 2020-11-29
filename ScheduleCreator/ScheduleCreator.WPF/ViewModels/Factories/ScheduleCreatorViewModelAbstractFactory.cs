using ScheduleCreator.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class ScheduleCreatorViewModelAbstractFactory : IScheduleCreatorViewModelAbstractFactory
    {
        private readonly IScheduleCreatorViewModelFactory<HelpViewModel> _helpViewModelFactory;
        private readonly IScheduleCreatorViewModelFactory<EmployeeViewModel> _employeeViewModel;
        private readonly IScheduleCreatorViewModelFactory<CreateScheduleViewModel> _createScheduleViewModel;
        private readonly IScheduleCreatorViewModelFactory<ConditionsViewModel> _conditionsViewModel;

        public ScheduleCreatorViewModelAbstractFactory(IScheduleCreatorViewModelFactory<HelpViewModel> helpViewModelFactory,
            IScheduleCreatorViewModelFactory<EmployeeViewModel> employeeViewModel,
            IScheduleCreatorViewModelFactory<CreateScheduleViewModel> createScheduleViewModel,
            IScheduleCreatorViewModelFactory<ConditionsViewModel> conditionsViewModel)
        {
            _helpViewModelFactory = helpViewModelFactory;
            _employeeViewModel = employeeViewModel;
            _createScheduleViewModel = createScheduleViewModel;
            _conditionsViewModel = conditionsViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Conditions:
                    return _conditionsViewModel.CreateViewModel();
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

using ScheduleCreator.Domain.Services;
using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static ScheduleCreator.WPF.State.Navigators.INavigator;

namespace ScheduleCreator.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IScheduleCreatorViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator,
            IScheduleCreatorViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}

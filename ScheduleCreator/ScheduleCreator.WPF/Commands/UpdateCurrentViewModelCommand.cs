using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static ScheduleCreator.WPF.State.Navigators.INavigator;

namespace ScheduleCreator.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigator _navigator;
        private readonly IRootScheduleCreatorViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator,
            IRootScheduleCreatorViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}

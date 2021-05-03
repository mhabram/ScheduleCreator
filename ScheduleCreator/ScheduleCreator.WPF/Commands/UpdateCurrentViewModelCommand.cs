using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCreator.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IScheduleCreatorViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator,
            IScheduleCreatorViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                if (_navigator.CurrentViewModel == _viewModelFactory.CreateViewModel(viewType))
                    return;
                else
                    _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}

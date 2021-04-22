using ScheduleCreator.WPF.Commands;
using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels.Factories;
using System;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IScheduleCreatorViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator,
            IScheduleCreatorViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += NavigatorStateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
        }

        private void NavigatorStateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

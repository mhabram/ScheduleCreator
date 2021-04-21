using ScheduleCreator.WPF.Commands;
using ScheduleCreator.WPF.State.Navigators;
using ScheduleCreator.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace ScheduleCreator.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRootScheduleCreatorViewModelFactory _viewModelFactory;

        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator,
            IRootScheduleCreatorViewModelFactory viewModelFactory)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
        }
    }
}

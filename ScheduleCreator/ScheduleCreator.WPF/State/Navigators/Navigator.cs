using ScheduleCreator.Domain.DTO.Observable;
using ScheduleCreator.WPF.Commands;
using ScheduleCreator.WPF.ViewModels;
using ScheduleCreator.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace ScheduleCreator.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        public Navigator(IRootScheduleCreatorViewModelFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory);
        }

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }
    }
}

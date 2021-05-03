using ScheduleCreator.WPF.ViewModels;
using System;

namespace ScheduleCreator.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
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
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}

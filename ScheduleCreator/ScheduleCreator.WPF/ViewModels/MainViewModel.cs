using ScheduleCreator.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        //TODO #6
        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }
    }
}

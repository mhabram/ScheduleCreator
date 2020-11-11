using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.State.Navigators
{
    public enum ViewType
    {
        Conditions,
        CreateSchedule,
        Employee,
        Help
    }

    public interface INavigator
    {
        

        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}

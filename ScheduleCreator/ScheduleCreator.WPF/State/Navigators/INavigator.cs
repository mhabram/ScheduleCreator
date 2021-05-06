using ScheduleCreator.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ScheduleCreator.WPF.State.Navigators
{
    public enum ViewType
    {
        Preference,
        Schedule,
        Employee,
        Help
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}

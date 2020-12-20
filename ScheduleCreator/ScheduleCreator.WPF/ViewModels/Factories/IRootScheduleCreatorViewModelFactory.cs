using ScheduleCreator.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    public interface IRootScheduleCreatorViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}

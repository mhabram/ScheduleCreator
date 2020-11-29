using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    public interface IScheduleCreatorViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class PreferenceViewModelFactory : IScheduleCreatorViewModelFactory<PreferenceViewModel>
    {
        public PreferenceViewModel CreateViewModel()
        {
            return new PreferenceViewModel();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class HelpViewModelFactory : IScheduleCreatorViewModelFactory<HelpViewModel>
    {
        public HelpViewModel CreateViewModel()
        {
            return new HelpViewModel();
        }
    }
}

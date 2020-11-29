using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class ConditionsViewModelFacoty : IScheduleCreatorViewModelFactory<ConditionsViewModel>
    {
        public ConditionsViewModel CreateViewModel()
        {
            return new ConditionsViewModel();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class CreateScheduleViewModelFactory : IScheduleCreatorViewModelFactory<CreateScheduleViewModel>
    {
        public CreateScheduleViewModel CreateViewModel()
        {
            return new CreateScheduleViewModel();
        }
    }
}

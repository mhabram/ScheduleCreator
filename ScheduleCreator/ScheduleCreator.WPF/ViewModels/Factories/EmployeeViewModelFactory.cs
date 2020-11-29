using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    class EmployeeViewModelFactory : IScheduleCreatorViewModelFactory<EmployeeViewModel>
    {
        public EmployeeViewModel CreateViewModel()
        {
            return new EmployeeViewModel();
        }
    }
}

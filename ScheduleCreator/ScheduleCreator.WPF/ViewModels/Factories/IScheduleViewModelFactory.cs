using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.WPF.ViewModels.Factories
{
    public interface IScheduleViewModelFactory<T> where T : ViewModelBase
    {
        T SchedulreViewModel();
    }
}

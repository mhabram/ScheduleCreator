using ScheduleCreator.Domain.DTO.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.DTO.PreferenceView
{
    public class DateTimeWrapper : ObservableObject
    {
        private DateTime? _value;
        public DateTime? Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}

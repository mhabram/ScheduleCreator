using ScheduleCreator.Domain.DTO.Observable;
using System;
using System.Collections.Generic;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeViewDTO : ObservableObject
    {
        public string FullName { get; set; }
        public IList<DateTime> PreferenceDays { get; set; }
        private int _workingDays;
        public int WorkingDays
        {
            get { return _workingDays; }
            set
            {
                _workingDays = value;
                OnPropertyChanged(nameof(WorkingDays));
            }
        }
    }
}

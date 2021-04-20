using ScheduleCreator.Domain.DTO.Observable;
using System;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeDTO : ObservableObject
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        private bool _day;
        public bool Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged(nameof(Day));
            }
        }
        private bool _swing;
        public bool Swing
        {
            get { return _swing; }
            set
            {
                _swing = value;
                OnPropertyChanged(nameof(Swing));
            }
        }
        private bool _night;
        public bool Night
        {
            get { return _night; }
            set
            {
                _night = value;
                OnPropertyChanged(nameof(Night));
            }
        }
        private string _shift;
        public string Shift
        {
            get { return _shift; }
            set
            {
                _shift = value;
                OnPropertyChanged(nameof(Shift));
            }
        }
        public int CalendarDateDTOId{ get; set; }
    }
}

using ScheduleCreator.Domain.DTO.Observable;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class EmployeeViewDTO : ObservableObject
    {
        public string FullName { get; set; }
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

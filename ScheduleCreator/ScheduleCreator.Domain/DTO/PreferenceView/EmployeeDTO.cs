using ScheduleCreator.Domain.DTO.Observable;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.DTO.PreferenceView
{
    public class EmployeeDTO : ObservableObject
    {
        public int Id { get; set; }
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public Preferences Preferences { get; set; }
    }
}

using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class PreferencesDTO
    {
        public string LastName{ get; set; }
        public sbyte FreeWorkingDays { get; set; }
        public virtual IList<PreferenceDay> PreferenceDays { get; set; }

        public PreferencesDTO(Preferences preferences)
        {
            LastName = preferences.Employee.LastName;
            FreeWorkingDays = preferences.FreeWorkingDays;
            PreferenceDays = preferences.PreferenceDays;
        }
    }
}

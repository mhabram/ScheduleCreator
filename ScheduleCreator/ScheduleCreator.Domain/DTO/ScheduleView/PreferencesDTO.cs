using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.DTO.ScheduleView
{
    public class PreferencesDTO
    {
        public string LastName{ get; set; }
        public int FreeWorkingDays { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public virtual IList<PreferenceDay> PreferenceDays { get; set; }

        public PreferencesDTO(Preferences preferences)
        {
            CultureInfo culture = new CultureInfo("en-NZ");

            LastName = preferences.Employee.LastName;
            FreeWorkingDays = preferences.FreeWorkingDays + preferences.LeaveDays;
            From = Convert.ToDateTime(preferences.From, culture);
            To = Convert.ToDateTime(preferences.To, culture);
            PreferenceDays = preferences.PreferenceDays;
        }
    }
}

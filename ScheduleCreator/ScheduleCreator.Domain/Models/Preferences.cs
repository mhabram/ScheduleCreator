using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Preferences
    {
        [Key]
        public int PreferencesId { get; set; }
        [Required]
        public sbyte FreeWorkingDays { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int LeaveDays { get; set; }
        [Required]
        public string InternalPreferenceId { get; set; }

        public Employee Employee { get; set; }
        public virtual IList<PreferenceDay> PreferenceDays { get; set; }
    }
}

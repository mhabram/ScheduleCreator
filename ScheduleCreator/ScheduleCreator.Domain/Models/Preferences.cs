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
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [Required]
        public string InternalPreferenceId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual IList<PreferenceDay> PreferenceDays { get; set; }
    }
}

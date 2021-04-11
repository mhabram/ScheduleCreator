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
        [Required]
        public string InternalPreferenceId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<Date> Dates { get; set; } // change it to virtual later.
    }
}

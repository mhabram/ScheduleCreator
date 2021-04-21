using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class PreferenceDay
    {
        [Key]
        public int PreferenceDayId { get; set; }
        public DateTime FreeDayChosen { get; set; }

        public virtual Preferences Preferences { get; set; }
    }
}

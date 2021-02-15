using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    [Table("Date")]
    public class DateModel : GenericModel
    {
        [Required]
        public DateModel FreeDayChosen { get; set; }
        [Required]
        public char Shift { get; set; }

        public PreferencesModel Preferences { get; set; }
    }
}

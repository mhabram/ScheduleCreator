using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        [Required]
        public string Shift { get; set; }
        public bool IsWorking { get; set; }
        public DateTime WorkingDay { get; set; }

        [ForeignKey("WeekId")]
        public virtual Week Week { get; set; }
    }
}

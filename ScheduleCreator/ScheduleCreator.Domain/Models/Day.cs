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
        public string MonthId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public Day() { }
        public Day(string shift, DateTime workingDay, string monthId)
        {
            Shift = shift;
            IsWorking = false;
            if (shift != "Free")
                IsWorking = true;
            WorkingDay = workingDay;
            MonthId = monthId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    [Table("Schedule")]
    public class ScheduleModel : GenericModel
    {
        public ScheduleModel()
        {
            Employees = new HashSet<EmployeeModel>();
        }
        [Required]
        public byte Year { get; set; } // record as yyyy
        [Required]
        public sbyte Month { get; set; } // record as 01-12
        [Required]
        public sbyte WorkingDays { get; set; } //numbers of a working day
        [Required]
        public char Shift { get; set; } //Depends on the char. D = Day shift, S = Swing shift and N stands for Night shift

        [ForeignKey("Id")]
        public virtual ICollection<EmployeeModel> Employees { get; set; }
    }
}

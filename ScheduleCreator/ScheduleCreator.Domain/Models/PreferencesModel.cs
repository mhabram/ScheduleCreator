using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    [Table("Preferences")]
    public class PreferencesModel : GenericModel
    {
        [Required]
        public sbyte FreeWorkingDays { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public ICollection<DateModel> Dates { get; set; }
    }
}

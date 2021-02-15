using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScheduleCreator.Domain.Models
{
    public class GenericModel
    {
        [Key]
        public int Id { get; set; }
    }
}

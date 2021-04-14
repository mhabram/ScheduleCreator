using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Helpers.Calendar
{
    public class ScheduleData
    {
        private int DaysInMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month);

        public ScheduleData()
        {

        }

        public string[,] LoadSchedule(ICollection<Employee> employees)
        {
            // string[+1,+2] there are +1 because we need header, and + 2 because of Date cell and Day Of Week
            string[,] schedule = new string[DaysInMonth + 1, employees.Count + 2];

            foreach (Employee employee in employees)
            {
                //employee
            }

            return schedule;
        }
    }
}

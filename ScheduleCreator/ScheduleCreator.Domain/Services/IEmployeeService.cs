using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(string name, string lastName);
    }
}

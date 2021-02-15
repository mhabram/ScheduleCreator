using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeModel> AddEmployee(EmployeeModel employee);
    }
}

using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public EmployeeRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();

                return employee;
            }
        }

        public async Task<IEnumerable<Employee>> GetDetails()
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Employee> employees = await context.Employees.ToListAsync();
                if (employees == null)
                    return null;

                return employees;
            }
        }

        public async Task<int> GetEmployee(string lastName)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                Employee employee = await context.Employees.FirstAsync(e => e.LastName == lastName);
                if (employee == null)
                    return -1;

                return employee.EmployeeId;

            }
        }
    }
}

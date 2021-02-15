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

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employee)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();

                return employee;
            }
        }
    }
}

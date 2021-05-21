using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            return employee;
        }

        public async Task<IList<Employee>> GetDetails(string internalPreferenceId)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            IList<Employee> employees = new List<Employee>();

            employees = await context.Employees
                .Include(p => p.Preferences)
                .ThenInclude(d => d.PreferenceDays)
                .Where(p => p.Preferences.InternalPreferenceId == internalPreferenceId)
                .ToListAsync();

            return employees;
        }

        public async Task<int> GetEmployee(string lastName)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            Employee employee = await context.Employees.SingleOrDefaultAsync(e => e.LastName == lastName);

            if (employee == null)
                return -1;
            else
                return employee.EmployeeId;
        }

        public async Task<IList<Employee>> GetEmployees()
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            return await context.Employees.ToListAsync();
        }

        public async Task RemoveEmployee(Employee employee)
        {
            using ScheduleCreatorDbContext context = _contextFactory.CreateDbContext();
            employee = await context.Employees
                .Include(p => p.Preferences)
                .ThenInclude(pd => pd.PreferenceDays)
                .Include(d => d.Days)
                .SingleOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            for (int i = 0; i < employee.Preferences.PreferenceDays.Count; i++)
            {
                context.PreferenceDays.Remove(employee.Preferences.PreferenceDays[i]);
            }
            context.Preferences.Remove(employee.Preferences);
            for (int i = 0; i < employee.Days.Count; i++)
            {
                context.Days.Remove(employee.Days[i]);
            }
            context.Remove(employee);
            await context.SaveChangesAsync();
        }
    }
}

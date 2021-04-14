using Microsoft.EntityFrameworkCore;
using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.WeekRepository
{
    public class WeekRepository : IWeekRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public WeekRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> SaveWeeks(ICollection<Week> weeks, string lastName)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                Employee employee = await context.Employees.SingleOrDefaultAsync(e => e.LastName == lastName);

                if (employee == null)
                    return false;

                foreach (Week week in weeks)
                {
                    week.Employee = employee;
                    await context.Weeks.AddAsync(week);
                }

                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}

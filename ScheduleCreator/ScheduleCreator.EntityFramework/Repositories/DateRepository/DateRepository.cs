using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.DateRepository
{
    public class DateRepository : IDateRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public DateRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Date> AddDate(Date date, int preferencesId)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                Preferences preferences = await context.Preferences.FindAsync(preferencesId);
                date.Preferences = preferences;

                await context.Dates.AddAsync(date);
                await context.SaveChangesAsync();
                return date;
            }
        }
    }
}

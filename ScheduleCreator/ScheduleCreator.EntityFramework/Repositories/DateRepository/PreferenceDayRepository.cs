using ScheduleCreator.Domain.Models;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.DateRepository
{
    public class PreferenceDayRepository : IPreferenceDayRepository
    {
        private readonly ScheduleCreatorDbContextFactory _contextFactory;

        public PreferenceDayRepository(ScheduleCreatorDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<PreferenceDay> AddPreferenceDay(PreferenceDay preferenceDay, int preferencesId)
        {
            using (ScheduleCreatorDbContext context = _contextFactory.CreateDbContext())
            {
                Preferences preferences = await context.Preferences.FindAsync(preferencesId);
                preferenceDay.Preferences = preferences;

                await context.AddAsync(preferenceDay);
                await context.SaveChangesAsync();
                return preferenceDay;
            }
        }
    }
}

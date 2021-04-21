using ScheduleCreator.Domain.Models;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.DateRepository
{
    public interface IPreferenceDayRepository
    {
        Task<PreferenceDay> AddPreferenceDay(PreferenceDay preferenceDay, int preferencesId);
    }
}

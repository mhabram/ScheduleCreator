using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IPreferenceService
    {
        Task<Preferences> AddPreference(int employeId, sbyte holidays);
        Task<ICollection<Preferences>> GetPreferences();
    }
}

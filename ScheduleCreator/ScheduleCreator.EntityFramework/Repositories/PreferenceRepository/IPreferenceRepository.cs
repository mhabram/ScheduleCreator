using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.PreferenceRepository
{
    public interface IPreferenceRepository
    {
        Task<Preferences> AddPreference(Preferences preferences, int employeId);
    }
}

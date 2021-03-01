using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Repositories.DateRepository
{
    public interface IDateRepository
    {
        Task<Date> AddDate(Date date, int preferencesId);
    }
}

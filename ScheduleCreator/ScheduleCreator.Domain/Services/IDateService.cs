using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IDateService
    {
        Task<Date> AddDate(DateTime date, int preferencesId);
    }
}

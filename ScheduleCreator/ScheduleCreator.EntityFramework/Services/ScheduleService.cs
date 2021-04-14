using ScheduleCreator.Domain.DTO.ScheduleView;
using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.ScheduleRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IWeekService _weekService;
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IWeekService weekService, IScheduleRepository scheduleRepository)
        {
            _weekService = weekService;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ICollection<Employee>> GetSchedule()
        {
            ICollection<Employee> employees = new Collection<Employee>();
            string internalWeekId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(), DateTime.Now.AddMonths(1).Month.ToString());
            
            employees =  await _scheduleRepository.GetSchedule(internalWeekId);

            return employees;
        }

        public async Task<bool> CreateSchedule(ObservableCollection<CalendarDateDTO> calendarDateDTO)
        {
            Dictionary<string, ICollection<Day>> employeeFullNameWorkingDays = new Dictionary<string, ICollection<Day>>();
            bool isSaved = false;
            string shift = "";

            foreach (CalendarDateDTO calendarDTO in calendarDateDTO)
            {
                foreach (EmployeeDTO employeeDTO in calendarDTO.Employees)
                {
                    if ((employeeDTO.Shift == null) || (employeeDTO.Shift == ""))
                        shift = "Free";
                    else
                        shift = employeeDTO.Shift;

                    if (employeeFullNameWorkingDays.ContainsKey(employeeDTO.FullName))
                    {

                        employeeFullNameWorkingDays[employeeDTO.FullName].Add(
                            new Day()
                            {
                                Shift = shift,
                                IsWorking = employeeDTO.IsWorking,
                                WorkingDay = employeeDTO.Date
                            });
                    }
                    else
                    {
                        employeeFullNameWorkingDays.Add(employeeDTO.FullName,
                            new List<Day>() {
                                new Day()
                                {
                                    Shift = shift,
                                    IsWorking = employeeDTO.IsWorking,
                                    WorkingDay = employeeDTO.Date
                                }
                            });
                    }
                }
            }

            foreach (var key in employeeFullNameWorkingDays)
            {
                isSaved = await _weekService.AddWeeks(key.Key, key.Value);
            }

            return isSaved;
        }
    }
}

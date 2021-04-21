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
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IList<Employee>> GetSchedule()
        {
            IList<Employee> employees = new List<Employee>();
            IList<Day> test = new List<Day>();
            string internalId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(), DateTime.Now.AddMonths(1).Month.ToString());
            
            employees =  await _scheduleRepository.GetSchedule(internalId);

            for (int i = 0; i < employees.Count; i++)
            {
                employees[i].Days = employees[i].Days.OrderBy(o => o.WorkingDay).ToList();
            }

            return employees;
        }

        public async Task<bool> CreateSchedule(ObservableCollection<CalendarDateDTO> calendarDateDTO)
        {
            Dictionary<string, List<Day>> employeeFullNameWorkingDays = new();
            EmployeeDTO employeeDTO;
            bool isSaved = false;
            bool isWorking = false;
            string shift;
            string monthId = String.Concat(DateTime.Now.AddMonths(1).Year.ToString(), DateTime.Now.AddMonths(1).Month.ToString());

            for (int i = 0; i < calendarDateDTO.Count; i++)
            {
                for (int j = 0; j < calendarDateDTO[i].Employees.Count; j++)
                {
                    shift = "Free";
                    employeeDTO = calendarDateDTO[i].Employees[j];

                    //if ((employeeDTO.Shift == null) || (employeeDTO.Shift == ""))
                    //    shift = "Free";
                    //else
                    if ((employeeDTO.Shift != null) && (employeeDTO.Shift != ""))
                        shift = employeeDTO.Shift;

                    //if (employeeDTO.Day || employeeDTO.Swing || employeeDTO.Night)
                    if (shift != "Free")
                        isWorking = true;

                    if (employeeFullNameWorkingDays.ContainsKey(employeeDTO.FullName))
                    {

                        employeeFullNameWorkingDays[employeeDTO.FullName].Add(
                            new Day()
                            {
                                Shift = shift,
                                IsWorking = isWorking,
                                WorkingDay = calendarDateDTO[i].Date,
                                MonthId = monthId
                            });
                    }
                    else
                    {
                        employeeFullNameWorkingDays.Add(employeeDTO.FullName,
                            new List<Day>() {
                                new Day()
                                {
                                    Shift = shift,
                                    IsWorking = isWorking,
                                    WorkingDay = calendarDateDTO[i].Date,
                                    MonthId = monthId
                                }
                            });
                    }
                }
            }

            foreach (var key in employeeFullNameWorkingDays)
            {
                isSaved = await _scheduleRepository.AddEmployeeScheduleDays(key.Key.Split()[1].ToLower(), key.Value);
            }

            return isSaved;
        }
    }
}

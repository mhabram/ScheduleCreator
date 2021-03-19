using ScheduleCreator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.Domain.Services
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(string name, string lastName);
        Task<int> GetEmployee(string lastName);
        Task<ObservableCollection<Employee>> GetDetails();
        Task<Employee> SetWeek(Employee employee, Week week, byte d);
    }
}

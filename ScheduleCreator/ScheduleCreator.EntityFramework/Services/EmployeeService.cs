using ScheduleCreator.Domain.Models;
using ScheduleCreator.Domain.Services;
using ScheduleCreator.EntityFramework.Repositories.EmployeeRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCreator.EntityFramework.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployee(string name, string lastName)
        {
            Employee employee = new Employee
            {
                Name = name.ToLower(),
                LastName = lastName.ToLower()
            };

            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<ObservableCollection<Employee>> GetDetails()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            DateTime StartMonth = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day + 1);
            string internalPreferenceId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString());
            string internalWeekId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString(), 5);

            // to fix issue with the 28 days of February need to create another getDetails repository and add to if that will be checking if month has 4 or 5 weeks.
            IEnumerable<Employee>? employeeDetails = await _employeeRepository.GetDetails(internalPreferenceId, internalWeekId);

            if (employeeDetails != null)
            {
                foreach (Employee employee in employeeDetails)
                {
                    employee.Name = String.Concat(employee.Name.First().ToString().ToUpper(), employee.Name.Substring(1).ToLower());
                    employee.LastName = String.Concat(employee.LastName.First().ToString().ToUpper(), employee.LastName.Substring(1).ToLower());
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public async Task<int> GetEmployee(string lastName)
        {
            int employeId = await _employeeRepository.GetEmployee(lastName.ToLower());

            return employeId;
        }

        public async Task<Employee> SetWeek(Employee employee, Week week, byte d)
        {
            ICollection<Day> day = new Collection<Day>();
            DateTime StartMonth = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day + 1);
            string internalWeekId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString(), d);

            week.InternalWeekId = internalWeekId;
            day = week.Days;


            employee = await _employeeRepository.SetWeek(employee, week, day);

            return employee;
        }
    }
}

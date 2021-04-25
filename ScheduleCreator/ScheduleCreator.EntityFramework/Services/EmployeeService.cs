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

        public async Task<IList<Employee>> GetDetails()
        {
            IList<Employee> employees = new List<Employee>();
            DateTime StartMonth = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day + 1);
            string internalPreferenceId = String.Concat(StartMonth.Year.ToString(), StartMonth.Month.ToString());
            IList<Employee> employeeDetails = await _employeeRepository.GetDetails(internalPreferenceId);

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

        public async Task<IList<Employee>> GetEmployees()
        {
            IList<Employee> employeesRepository = await _employeeRepository.GetEmployees();
            List<Employee> employees = new();

            if (employeesRepository != null)
            {
                foreach (Employee employee in employeesRepository)
                {
                    employee.Name = String.Concat(employee.Name.First().ToString().ToUpper(), employee.Name.Substring(1).ToLower());
                    employee.LastName = String.Concat(employee.LastName.First().ToString().ToUpper(), employee.LastName.Substring(1).ToLower());
                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}

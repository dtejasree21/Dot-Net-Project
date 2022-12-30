using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary
{
    class EmployeeRepoAsync : IEmployeeRepoAsync
    {
        EmployeeDBEntities ent = new EmployeeDBEntities();
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await ent.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string empId)
        {
            try
            {
                Employee employee = await (from e in ent.Employees where e.EmpId == empId select e).FirstAsync();
                return employee;
            }
            catch (Exception)
            {
                throw new Exception("No such Employee Id");
            }
        }
    }
}

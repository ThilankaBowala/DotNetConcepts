using EmployeesManagement.Context;
using EmployeesManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.Services.Employee
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Employee>> GetEmployeesAsync() { 
            return await _context.Employees.ToListAsync();
        }

        public async Task<Models.Employee> GetById(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(Models.Employee employee) {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmployee(int id, Models.Employee employee)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
            
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

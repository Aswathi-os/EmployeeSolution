using Data.Models;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{


    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context)
        {
            _context = context;

        }



        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        //public async Task<Employee> UpdateAsync(Employee employee)
        //{
        //    _context.Entry(employee).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return employee;
        //}

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.Id);
            if (existing == null)
                throw new KeyNotFoundException("Employee not found");

            existing.Name = employee.Name;
            existing.Department = employee.Department;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkDeleteAsync(List<int> ids)
        {
            var employees = await _context.Employees
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();

            if (!employees.Any())
                return false;

            _context.Employees.RemoveRange(employees);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee?> ToggleStatusAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;

            employee.IsActive = !employee.IsActive;
            await _context.SaveChangesAsync();

            return employee;
        }
    }

}

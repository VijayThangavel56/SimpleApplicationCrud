using Microsoft.EntityFrameworkCore;
using SampleApplication.Server.Data;
using SampleApplication.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApplication.Server.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;
        public EmployeeRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id) => await _db.Employees.AnyAsync(e => e.Id == id);

        public async Task<List<Employee>> GetAllAsync() => await _db.Employees.ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) => await _db.Employees.FindAsync(id);

        public async Task UpdateAsync(Employee employee)
        {
            _db.Entry(employee).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}

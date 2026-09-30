using SampleApplication.Server.Models;
using SampleApplication.Server.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApplication.Server.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) => _repo = repo;

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _repo.AddAsync(employee);
            return employee;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Employee not found");
            await _repo.DeleteAsync(existing);
        }

        public async Task<List<Employee>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Employee?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task UpdateAsync(int id, Employee employee)
        {
            if (id != employee.Id) throw new System.ArgumentException("Id mismatch");
            if (!await _repo.ExistsAsync(id)) throw new KeyNotFoundException("Employee not found");
            await _repo.UpdateAsync(employee);
        }
    }
}

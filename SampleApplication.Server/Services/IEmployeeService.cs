using SampleApplication.Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApplication.Server.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(int id, Employee employee);
        Task DeleteAsync(int id);
    }
}

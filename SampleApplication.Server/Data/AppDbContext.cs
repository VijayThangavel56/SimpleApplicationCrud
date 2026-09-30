using Microsoft.EntityFrameworkCore;
using SampleApplication.Server.Models;

namespace SampleApplication.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;
    }
}

using GemBox.Models;
using Microsoft.EntityFrameworkCore;

namespace GemBox.Repository
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<EmployeeProject>? EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(k=>k.ID);
            modelBuilder.Entity<EmployeeProject>().HasKey(k=>k.ID);
        }

    }

}
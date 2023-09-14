using ASPCORE_CRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;
using ASPCORE_CRUD.Models;

namespace ASPCORE_CRUD.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }

        public DbSet<ASPCORE_CRUD.Models.UpdateEmployeeViewModel>? UpdateEmployeeViewModel { get; set; }
    }
}

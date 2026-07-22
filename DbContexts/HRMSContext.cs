using HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DbContexts
{
    public class HRMSContext : DbContext
    {
        public HRMSContext(DbContextOptions<HRMSContext> options) : base(options)
        {
            //options
            //1) which database to connect to(sql,mysql,postgresql....)
            //2) connection string
        }

        //table == DBset
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lookup> Lookups { get; set; }

    }
}

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Employee>()
            // .HasOne(e => e.Department)
            //.WithMany(d => d.Employees)
            // .HasForeignKey(e => e.DepartmentId)
            //.OnDelete(DeleteBehavior.Restrict);



            //seeding data for lookup table

            //Employee Positions{major code=0}
            modelBuilder.Entity<Lookup>().HasData(
                new Lookup { Id = 1, MajorCode = 0, MinorCode = 0, Name="Employee Positions" },
                new Lookup { Id = 2, MajorCode = 0, MinorCode = 1, Name="HR" },
                new Lookup { Id = 3, MajorCode = 0, MinorCode = 2, Name= "Manager" },
                new Lookup { Id = 4, MajorCode = 0, MinorCode = 3, Name= "Developer" },

                //Department Types{major code=1}
                new Lookup { Id = 5, MajorCode = 1, MinorCode = 0, Name = "Department Types" },
                new Lookup { Id = 6, MajorCode = 1, MinorCode = 1, Name = "Finance" },
                new Lookup { Id = 7, MajorCode = 1, MinorCode = 2, Name = "Administrative" },
                new Lookup { Id = 8, MajorCode = 1, MinorCode = 3, Name = "Technical" }
            );


            //seeding admin user
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Admin", HashedPassword = "$2a$11$GVebhrpLBqBxbpDOLE7bMOl4j2jJE5m8Txhtv16ITk0gBDC.X5xF2", IsAdmin = true }
            );





            modelBuilder.Entity<Employee>().HasIndex(e => e.UserId).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();

        }




       





        //table == DBset
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

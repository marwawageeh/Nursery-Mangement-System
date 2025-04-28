using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mvc_project.Models
{
	public class MWContext:IdentityDbContext<ApplicationUser>
	{
		public DbSet<Department> department { get; set; }
		public DbSet<Course> course { get; set; }
		public DbSet<Instructor> instructor { get; set; }
		public DbSet<Trainee> trainee { get; set; }
		public DbSet<CrsResult> crsResult { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public MWContext() : base()
        {
        }

        public MWContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			optionsBuilder.UseSqlServer("Data Source=MARWA\\SQLEXPRESS;Initial Catalog=MVC_Project;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
			base.OnConfiguring(optionsBuilder);
		}


	}
}

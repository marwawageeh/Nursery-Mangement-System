using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_project.Models
{
	public class Course
	{
		public int ID { get; set; }
		[Required]
		public string? Name { get; set; }
		public int Degree { get; set; }
		public int Houres { get; set; }
		public int Min_Degree { get; set; }

        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 


        public virtual ICollection<Attendance> Attendances { get; set; }

        public List<Instructor>? instructors { get; set; }
		public List<CrsResult>? crsResults { get; set; }



		[ForeignKey("department")]
		public int dept_id { get; set; }
		public Department department { get; set; }
	}
}

using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_project.Models
{
	public class CrsResult
	{
		public int ID { get; set; }
		public int Degree { get; set; }



		[ForeignKey("trainee")]
		public int trainee_id { get; set; }
		public Trainee trainee { get; set; }



		[ForeignKey("course")]
		public int crs_id { get; set; }
		public Course course { get; set; }
	}
}

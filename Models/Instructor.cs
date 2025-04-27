using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mvc_project.Models
{
	public class Instructor
	{
        public int ID { get; set; }
		public string? Name { get; set; }
		public string? ImageURL { get; set; }
		public int Salary { get; set; }
		public string? Address { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // لا يُحفظ في قاعدة البيانات


        [ForeignKey("course")]
		public int crs_id { get; set; }
		public Course course { get; set; }



		[ForeignKey("department")]
		public int dept_id { get; set; }
		public Department department { get; set; }

	}
}

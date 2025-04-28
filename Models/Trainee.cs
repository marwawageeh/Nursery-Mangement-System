using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace mvc_project.Models
{
	public class Trainee
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? ImageURL { get; set; }
		public int Grade { get; set; }
		public string? Address { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // لا يُحفظ في قاعدة البيانات


        public List<CrsResult> crsResults { get; set; }


		[ForeignKey("department")]
		public int dept_id { get; set; }
		public Department? department { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

    }
}

using mvc_project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_project.ModelView
{
    public class Inst_With_Cource_MV
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }
        public int crs_id { get; set; }
		public int dept_id { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; } // لا يُحفظ في قاعدة البيانات

        public List<Course>? CoursesList { get; set; }
        public List<Department >? Departmentlist { get; set; }
    }
}

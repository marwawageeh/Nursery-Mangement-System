namespace mvc_project.Models
{
	public class Department
	{
        public int ID{ get; set; }
		public string? Name { get; set; }
		public string? Manger { get; set; }

		public List<Instructor>? instructors { get; set; }
		public List<Trainee>? trainees { get; set; }
		public List<Course>? courses { get; set; }

	}
}

using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_project.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; } 
        public DateTime Date { get; set; } 
        public string Status { get; set; } 
        public TimeSpan Time { get; set; }


        [ForeignKey(" Trainee")]
        public int TraineeID { get; set; }
        public Trainee? Trainee { get; set; }


        [ForeignKey(" Course")]
        public int CourseId { get; set; }
        public  Course Course { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvc_project.Models
{
    public class Payment
    {

        public int Id { get; set; }

        public int TraineeId { get; set; }  // الطالب

        [ForeignKey("TraineeId")]
        public Trainee Trainee { get; set; }


        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }


        public string Month { get; set; }  // مثال: January, February...

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }


        public string? PaymentMethod { get; set; } // Visa, Cash, etc.

        public bool IsPaid { get; set; }  // true = دفع، false = لسه ما دفعش
    }
}

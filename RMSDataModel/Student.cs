using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class Student
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 7)]
        public string Session { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Roll Number must be a positive value")]
        [Display(Name = "Class Roll")]

        public int ClassRoll { get; set; }

        [Required]
        [Display(Name = "Exam Roll")]
        [Range(1, int.MaxValue, ErrorMessage = "Roll Number must be a positive value")]
        public int ExamRoll { get; set; }

        [Display(Name = "Semester/Year")]
        public Semester Semester { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        //Navigational Properties
        public virtual ICollection<Enrollment> Enrollments { get; set; }


    }
}

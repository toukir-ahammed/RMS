using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Least Overall CGPA for Promotion")]
        [Range(0.00,4.00)]
        public double LeastCGPAToPass { get; set; }

        [Required]
        [Display(Name = "Least Grade Point in each Subject for Promotion")]
        [Range(0.00, 4.00)]
        public double LeastGPToPass { get; set; }


        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}

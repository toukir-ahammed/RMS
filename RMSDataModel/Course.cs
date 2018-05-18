using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course Code")]
        public int CourseID { get; set; }

        [Required]
        [Display(Name = "Course Title")]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [Range(0, 3)]
        public int Credits { get; set; }

        
        public int? DepartmentID { get; set; }

        public virtual Department Department { get; set; }


        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors{ get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class Assignment
    {
        public int AssignmentID { get; set; }
        
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public int? DepartmentID { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        [Display(Name = "Calender Year")]
        [Range(1900,2100)]
        public int CalenderYear { get; set; }

        
        [Display(Name = "Marksheet file name")]
        public string MarksheetFileName { get; set; }

        [Required]
        [Display(Name = "Continuous Evalution Total Mark")]
        [Range(0,100)]
        public double CETotal { get; set; }

        [Required]
        [Display(Name = "Final Examination Total Mark")]
        [Range(0,100)]
        public double FinalExamTotal { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="CE Mark Submission Deadline")]
        [Column(TypeName = "datetime2")]
        public DateTime CEDeadline { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Final Mark Submission Deadline")]
        
        public DateTime FinalDeadLine { get; set; }

        [Display(Name = "CE Mark Submitted")]
        public bool CESubmitted { get; set; }

        [Display(Name = "Final Examination Mark Submitted")]
        public bool FinalSubmitted { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Department Department { get; set; }
    }
}

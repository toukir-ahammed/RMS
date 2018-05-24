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

        public string MarksheetFileName { get; set; }

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

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}

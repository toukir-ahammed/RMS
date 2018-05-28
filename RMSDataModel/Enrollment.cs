using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseID { get; set; }
        public int StudentId { get; set; }
        //public int? DepartmentID { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        [Display(Name = "Calender Year")]
        [Range(1900, 2100)]
        public int CalenderYear { get; set; }

        
        public double CEMark { get; set; }
        public double FinalMark { get; set; }
        public double TotalMark
        {
            get
            {
                return Math.Round(CEMark+ FinalMark);
            }
            
        }

        public double GradePoint
        {
            get
            {
                if (TotalMark >= 80.00)
                {
                    return 4.00;
                }
                else if (TotalMark >= 75.00 && TotalMark < 80.00)
                {
                    return 3.75;
                }
                else if (TotalMark >= 70.00 && TotalMark < 75.00)
                {
                    return 3.50;
                }
                else if (TotalMark >= 65.00 && TotalMark < 70.00)
                {
                    return 3.25;
                }
                else if (TotalMark >= 60.00 && TotalMark < 65.00)
                {
                    return 3.00;
                }
                else if (TotalMark >= 55.00 && TotalMark < 60.00)
                {
                    return 2.75;
                }
                else if (TotalMark >= 50.00 && TotalMark < 55.00)
                {
                    return 2.50;
                }
                else if (TotalMark >= 45.00 && TotalMark < 50.00)
                {
                    return 2.25;
                }
                else if (TotalMark >= 40.00 && TotalMark < 45.00)
                {
                    return 2.00;
                }
                else return 0.00;
            }
        }

        public string Grade
        {
            get
            {
                if (TotalMark >= 80.00)
                {
                    return "A+";
                }
                else if (TotalMark >= 75.00 && TotalMark < 80.00)
                {
                    return "A";
                }
                else if (TotalMark >= 70.00 && TotalMark < 75.00)
                {
                    return "A-";
                }
                else if (TotalMark >= 65.00 && TotalMark < 70.00)
                {
                    return "B+";
                }
                else if (TotalMark >= 60.00 && TotalMark < 65.00)
                {
                    return "B";
                }
                else if (TotalMark >= 55.00 && TotalMark < 60.00)
                {
                    return "B-";
                }
                else if (TotalMark >= 50.00 && TotalMark < 55.00)
                {
                    return "C+";
                }
                else if (TotalMark >= 45.00 && TotalMark < 50.00)
                {
                    return "C";
                }
                else if (TotalMark >= 40.00 && TotalMark < 45.00)
                {
                    return "D";
                }
                else return "F";
            }
        }

        //[DisplayFormat(NullDisplayText = "No Grade")]
        //public Grade? Grade { get; set; }

        //[Display(Name = "Mark Submitted")]
        //public bool Submitted { get; set; }
        //[Display(Name = "Mark Published")]
        //public bool Published { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Column(TypeName = "datetime2")]
        //[Display(Name = "Result Publication Date")]
        //public DateTime PublicationDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        //public virtual Department Department{ get; set; }

    }
}

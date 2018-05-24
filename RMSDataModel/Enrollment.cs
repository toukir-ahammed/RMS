using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public double CEMark { get; set; }
        public double FinalMark { get; set; }
        public double TotalMark
        {
            get
            {
                return Math.Round(CEMark+ FinalMark);
            }
        }

        //[DisplayFormat(NullDisplayText = "No Grade")]
        //public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}

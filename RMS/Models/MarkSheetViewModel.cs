using RMSDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.Models
{
    public class MarkSheetViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public Semester Semester { get; set; }

        [Display(Name = "Weighted Sum of Grade Points")]
        public double CurrentSemesterTotalCredits { get; set; }
        public double CurrentSemesterObtainedCredits { get; set; }
        [Display(Name = "GPA")]
        public double CurrentSemesterGPA {
            get
            {
                if (CurrentSemesterTotalCredits == 0.0) return 0.0;
                return Math.Round( CurrentSemesterObtainedCredits/CurrentSemesterTotalCredits, 2);
            }
        }

        [Display(Name = "Weighted Sum of Grade Points")]
        public double UptoPreviousSemesterTotalCredits
        { get
            {
                return TotalCredits - CurrentSemesterTotalCredits;
            }
        }
        public double UptoPreviousSemesterObtainedCredits
        {
            get
            {
                return TotalObtainedCredits - CurrentSemesterObtainedCredits;
            }
        }
        [Display(Name = "GPA")]
        public double UptoPreviousSemesterGPA
        {
            get
            {
                if (UptoPreviousSemesterTotalCredits == 0.0) return 0.0;
                return Math.Round(UptoPreviousSemesterObtainedCredits / UptoPreviousSemesterTotalCredits, 2);
            }
        }

        [Display(Name = "Weighted Sum of Grade Points")]
        public double TotalCredits { get; set; }
        public double TotalObtainedCredits { get; set; }
        [Display(Name = "CGPA")]
        public double CGPA
        {
            get
            {
                if (TotalCredits == 0.0) return 0.0;
                return Math.Round(TotalObtainedCredits / TotalCredits, 2);
            }
        }

        public bool Promoted { get; set; }
    }

    public class ResultPublicationViewModel
    {
        [Required]
        [Display(Name = "Calender Year")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Semester/Year")]
        public Semester Semester { get; set; }
    }
}
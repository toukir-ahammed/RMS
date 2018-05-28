using RMSDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.Models
{
    public class ResultViewModel
    {
        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        //[Required]
        //[Display(Name = "Department" )]
        //public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Calender Year")]
        [Range(1900, 2100)]
        public int CalenderYear { get; set; }

        [Required]
        [Display(Name = "Semester/Year")]
        public Semester Semester { get; set; }
    }

}
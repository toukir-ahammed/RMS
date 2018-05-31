using RMSDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.Models
{
    public class TabulationSheetViewModel
    {
        [Required]
        public int DepartmentID { get; set; }
        [Required]
        public Semester Semester { get; set; }

        [Required]
        [Display(Name = "Calender Year")]
        [Range(1900, 2100)]
        public int CalenderYear { get; set; }

        public virtual Department Department { get; set; }
    }
}
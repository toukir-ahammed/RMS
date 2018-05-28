using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class ResultPublication
    {
        public int ID { get; set; }

        [Required]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Semester/Year")]
        public Semester Semester { get; set; }

        [Required]
        [Display(Name = "Calender Year")]
        [Range(1900, 2100)]
        public int CalenderYear { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Result Publication Date")]
        [Column(TypeName = "datetime2")]
        public DateTime PublicationDate { get; set; }



        public virtual Department Department { get; set; }
    }
}

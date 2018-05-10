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
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}

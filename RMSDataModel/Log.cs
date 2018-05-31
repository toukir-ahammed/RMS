using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSDataModel
{
    public class Log
    {
        public int LogID { get; set; }

        
        public int? AssignmentID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        public string LogMessage { get; set; }

        
        public virtual Assignment Assignment { get; set; }
    }
}

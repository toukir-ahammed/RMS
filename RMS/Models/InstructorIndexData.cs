﻿using RMSDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Models
{
    public class InstructorIndexData
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
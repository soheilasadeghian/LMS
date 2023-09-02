using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Areas.admin.Models
{
    public class CourseUserResult
    {
        public Result result { get; set; }
        public CourseUser CourseUser { get; set; }
    }
}
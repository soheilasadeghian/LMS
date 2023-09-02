using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Areas.admin.Models
{
    public class CourseUserListResult
    {
        public int count { get; set; }
        public Result result { get; set; }
        public List<CourseUser> CourseUser { get; set; }
    }
}
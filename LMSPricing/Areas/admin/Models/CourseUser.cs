using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Areas.admin.Models
{
    public class CourseUser
    {
        public long ID { get; set; }
        public long courseID { get; set; }
        public long userID { get; set; }
        public string fullname { get; set; }
        public string regDate { get; set; }
        public string coursename { get; set; }
        public bool isread { get; set; }
        public string mobile { get; set; }
        public string userimage { get; set; }
    }
}
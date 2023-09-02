using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Areas.admin.Models
{
    public class User
    {
        public long ID { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string mobile { get; set; }
        public string fullname { get; set; }
        public string image { get; set; }
        public string regrDate { get; set; }
    }
}
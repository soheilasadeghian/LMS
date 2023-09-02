using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Models
{
    public class AgentViewModel
    {
        public BaseObject<string> fullName { get; set; } = new BaseObject<string>();
        public BaseObject<string> company { get; set; } = new BaseObject<string>();
        public BaseObject<string> email { get; set; } = new BaseObject<string>();
        public BaseObject<string> address { get; set; } = new BaseObject<string>();
        public BaseObject<string> mobile { get; set; } = new BaseObject<string>();
        public BaseObject<string> explain { get; set; } = new BaseObject<string>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Models
{
    public class RegisterOrderViewModel
    {

        public BaseObject<int> packetList { get; set; } = new BaseObject<int>();
        public BaseObject<int> hostList { get; set; } =new BaseObject<int>();
        public BaseObject<string> email { get; set; } = new BaseObject<string>();
        public BaseObject<string> fullName { get; set; } = new BaseObject<string>();
        public BaseObject<string> mobile { get; set; } = new BaseObject<string>();
        public BaseObject<string> explain { get; set; } = new BaseObject<string>();
    }
}
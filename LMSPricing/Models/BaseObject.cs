using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.Models
{
    public class BaseObject<T>
    {
        public bool isError { get; set; }
        public string message { get; set; } = "";
        public T value { get; set; }
    }
}
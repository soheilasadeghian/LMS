using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSPricing.ClassCollection
{
    public class Model
    {
        public class Result
        {
            public int code { set; get; }
            public string message { set; get; }
        }
        public class LongResult
        {
            public Result result { get; set; }
            public long value { get; set; }
        }
        public class UploadRequestResult
        {
            public Result Result { get; set; }
            public string Token { get; set; }
        }
        public class UploadFileResult
        {
            public Result Result { get; set; }
            public string image { get; set; }
        }
    }
}
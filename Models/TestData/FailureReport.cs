using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.TestData
{
    public class FailureReport
    {
        public String Actual { get; set; }
        public String Expected { get; set; }
        public ResultCode ResultCode { get; set; }
        public String ResultComment { get; set; }
        public String VariableName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PainFree.Models.TestInfo;
using PainFree.Models.VM;

namespace PainFree.Models.Failure.VMLevel
{
    public class SystemIsDownFailure : IFailure
    {
        private readonly String Message = "The following tests did not run:";
        private readonly String FileBasePath = @"C:\Automation\Projects\Tellurium\nightly runs\";

        private Boolean _isFailure;

        private String _failureMessage;

        public Boolean IsFailure { get { return _isFailure; } }

        public String FailureMessage { get { return _failureMessage; } }
    }
}
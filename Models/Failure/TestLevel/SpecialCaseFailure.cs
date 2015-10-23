using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PainFree.Models.Failure.TestLevel
{
    public class SpecialCaseFailure : IFailure
    {
        private Boolean _isFailure;
        private String _failureMessage;

        public Boolean IsFailure { get { return _isFailure; } }
        public String FailureMessage { get { return _failureMessage; } }
    }
}
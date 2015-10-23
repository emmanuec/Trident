using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PainFree.Models.VM;

namespace PainFree.Models.Failure.VMLevel
{
    public class WebDriverFailure : IFailure
    {
        private VirtualMachine VirtualMachine;
        private Boolean _isFailure;
        private String _failureMessage;

        public WebDriverFailure(VirtualMachine virtualMachine)
        {
            VirtualMachine = virtualMachine;
        }

        public Boolean IsFailure { get { return _isFailure; } }

        public String FailureMessage { get { return _failureMessage; } }
    }
}
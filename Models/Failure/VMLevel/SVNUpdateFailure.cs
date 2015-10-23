using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PainFree.Models.VM;

namespace PainFree.Models.Failure.VMLevel
{
    public class SVNUpdateFailure : IFailure
    {
        private VirtualMachine VirtualMachine;
        private Boolean _isFailure;
        private String _failureMessage;

        public SVNUpdateFailure(VirtualMachine virtualMachine)
        {
            VirtualMachine = virtualMachine;
            setFailure();
        }

        private void setFailure()
        {
            List<String> machineNames = new List<String>()
            {
                "VMTEST01","VMTEST11"
            };

            var test = machineNames.Contains(VirtualMachine.Name);

            _isFailure = test;
        }

        public Boolean IsFailure { get { return _isFailure; } }

        public String FailureMessage { get { return _failureMessage; } }
    }
}
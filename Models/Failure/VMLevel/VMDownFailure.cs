using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using PainFree.Models.VM;

namespace PainFree.Models.Failure.VMLevel
{
    public class VMDownFailure : IFailure
    {
        private readonly String Message = "Virtual Machine is down";
        private VirtualMachine VirtualMachine;

        private Boolean _isFailure;
        private String _failureMessage;

        public VMDownFailure(VirtualMachine virtualMachine)
        {
            VirtualMachine = virtualMachine;
            SetFailure();
            SetFailureMessage();
        }

        private void SetFailure()
        {
            _isFailure = !IsPingSuccess();
        }

        private Boolean IsPingSuccess()
        {
            var ping = new Ping();
            var reply = ping.Send(VirtualMachine.Name, 60 * 1000);

            if (reply.Status.Equals(IPStatus.Success))
                return true;

            return false;
        }

        private void SetFailureMessage()
        {
            if (IsFailure == false)
            {
                _failureMessage = "No Failure";
                return;
            }
            else
            {
                _failureMessage = Message;
            }

        }

        public Boolean IsFailure { get { return _isFailure; } }

        public String FailureMessage { get { return _failureMessage; } }
    }
}
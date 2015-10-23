using PainFree.Models.Failure.VMLevel;

namespace PainFree.Models.VM
{
    public class VirtualMachineStatusReport
    {
        private VirtualMachine _virtualMachine;
        private VMDownFailure _vmDownFailure;
        private AllTestRanFailure _allTestRunFailure;
        private JavaUpdateFailure _javaUpdateFailure;
        private SVNUpdateFailure _svnUpdateFailure;
        private WebDriverFailure _webDriverFailure;

        public VirtualMachine VirtualMachine { get { return _virtualMachine; } }
        public VMDownFailure VmDownFailure { get { return _vmDownFailure; } }
        public AllTestRanFailure AllTestRunFailure { get { return _allTestRunFailure; } }
        public JavaUpdateFailure JavaUpdateFailure { get { return _javaUpdateFailure; } }
        public SVNUpdateFailure SvnUpdateFailure { get { return _svnUpdateFailure; } }
        public WebDriverFailure WebDriverFailure { get { return _webDriverFailure; } }

        public VirtualMachineStatusReport(VirtualMachine virtualMachine)
        {
            _virtualMachine = virtualMachine;
            _vmDownFailure = new VMDownFailure(virtualMachine);
            _allTestRunFailure = new AllTestRanFailure(virtualMachine);
            _javaUpdateFailure = new JavaUpdateFailure(virtualMachine);
            _svnUpdateFailure = new SVNUpdateFailure(virtualMachine);
            _webDriverFailure = new WebDriverFailure(virtualMachine);
        }
    }
}
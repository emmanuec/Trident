using PainFree.Models.TestInfo;
using System;
using System.Collections.Generic;
using TelluriumCore.Reporting.TestRun;

namespace PainFree.Models.VM
{
    public class VirtualMachine
    {
        private string _name;
        private DateTime _dataBeginTime;
        private DateTime _dataEndTime;
        private List<RunInfo> _testRuns;

        public VirtualMachine(String name, List<ITestReport> testRuns, DateTime dataBeginTime, DateTime dataEndTime)
        {
            _name = name;
            _dataBeginTime = dataBeginTime;
            _dataEndTime = dataEndTime;
            _testRuns = RunInfo.getAllInstance(testRuns);
        }

        public string Name { get { return _name; } }
        public DateTime DataBeginTime { get { return _dataBeginTime; } }
        public DateTime DataEndTime { get { return _dataEndTime; } }
        public List<RunInfo> TestRuns { get { return _testRuns; } }
    }
}
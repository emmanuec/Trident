using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelluriumCore.Reporting.TestRun;

namespace PainFree.Models.TestInfo
{
    public class RunInfo : IEquatable<RunInfo>
    {
        private String _project;
        private String _testMode;
        private String _virtualMachine;
        private DateTime _runsBeginTime;
        private DateTime _runsEndTime;

        public String Project { get { return _project; } }
        public String TestMode { get { return _testMode; } }
        public String VirtualMachine { get { return _virtualMachine; } }
        public DateTime RunsBeginTime { get { return _runsBeginTime; } }
        public DateTime RunsEndTime { get { return _runsEndTime; } }

        private RunInfo(String project, String testMode, String virtualMachine, DateTime runsBeginTime, DateTime runsEndTime)
        {
            _project = project;
            _testMode = testMode;
            _virtualMachine = virtualMachine;
            _runsBeginTime = runsBeginTime;
            _runsEndTime = runsEndTime;
        }

        public static List<RunInfo> getAllInstance(List<ITestReport> testRuns)
        {
            List<RunInfo> runInfos = new List<RunInfo>();

            foreach(var test in testRuns)
            {
                runInfos.Add(new RunInfo(test.ProjectName, test.TestMode.Name, test.MachineName, test.RunBeginTime, test.RunEndTime));
            }

            return runInfos;
        }

        public static RunInfo getInstance(String project, String testMode, String virtualMachine, DateTime runsBeginTime, DateTime runsEndTime)
        {
            return new RunInfo(project, testMode, virtualMachine, runsBeginTime, runsEndTime);
        }


        public bool Equals(RunInfo other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return Project.Equals(other.Project)
                    && TestMode.Equals(other.TestMode)
                    && VirtualMachine.Equals(other.VirtualMachine);
            }
        }
    }
}
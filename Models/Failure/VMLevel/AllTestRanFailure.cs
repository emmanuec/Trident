using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PainFree.Models.TestInfo;
using PainFree.Models.VM;

namespace PainFree.Models.Failure.VMLevel
{
    public class AllTestRanFailure : IFailure
    {
        private readonly String Message = "The following tests did not run:";
        private readonly String FileBasePath = @"C:\Automation\Projects\Tellurium\nightly runs\";

        private Boolean _isFailure;

        private String _failureMessage;

        private VirtualMachine VirtualMachine;

        private List<RunInfo> ActualNoonRuns = new List<RunInfo>();
        private List<RunInfo> ActualNightRuns = new List<RunInfo>();

        private List<RunInfo> ExpectedNoonRuns = new List<RunInfo>();
        private List<RunInfo> ExpectedNightRuns = new List<RunInfo>();

        private List<RunInfo> MissingNoonRuns = new List<RunInfo>();
        private List<RunInfo> MissingNightRuns = new List<RunInfo>();

        public AllTestRanFailure(VirtualMachine virtualMachine)
        {
            VirtualMachine = virtualMachine;
            SetActualNoonRuns();
            SetActualNightRuns();
            SetExpectedNoonRuns();
            SetExpectedNightRuns();
            setMissingNoonRuns();
            setMissingNightRuns();
            setFailure();
            setFailureMessage();
        }

        private void SetActualNoonRuns()
        {
            var noonStartTime = VirtualMachine.DataBeginTime;
            var noonEndTime = VirtualMachine.DataBeginTime.Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm

            SetActualRuns(noonStartTime, noonEndTime, ActualNoonRuns);
        }

        private void SetActualNightRuns()
        {
            var nightStartTime = VirtualMachine.DataEndTime.AddYears(-1).Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm
            var nightEndTime = VirtualMachine.DataEndTime;

            SetActualRuns(nightStartTime, nightEndTime, ActualNoonRuns);
        }

        private void SetActualRuns(DateTime startTime, DateTime endTime, List<RunInfo> actualRuns)
        {
            var testData = from report in VirtualMachine.TestRuns
                           where report.RunsBeginTime >= startTime
                           where report.RunsBeginTime <= endTime
                           where report.VirtualMachine.Contains("VMTEST") ||
                                 report.VirtualMachine.Contains("VMAUTOMATION")
                           select report;

            actualRuns = testData.ToList();
        }

        private void SetExpectedNoonRuns()
        {
            var noonStartTime = VirtualMachine.DataBeginTime;
            var noonEndTime = VirtualMachine.DataBeginTime.Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm

            var fileName = @"NOON*.cmd";
            var noonBatchFiles = GetRunsFromBatchFiles(fileName);
            ExpectedNoonRuns = GetTestThatRan(noonBatchFiles, noonStartTime, noonEndTime);
        }

        private void SetExpectedNightRuns()
        {
            var nightStartTime = VirtualMachine.DataEndTime.AddYears(-1).Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm
            var nightEndTime = VirtualMachine.DataEndTime;

            var fileName = @"NIGHT*.cmd";
            var nightBatchFiles = GetRunsFromBatchFiles(fileName);
            ExpectedNightRuns = GetTestThatRan(nightBatchFiles, nightStartTime, nightEndTime);
        }

        //TODO: file not found exception yo
        private List<String> GetRunsFromBatchFiles(String fileName)
        {
            var directoryPath = FileBasePath + VirtualMachine.Name + "\\";
            List<String> batchFilesToRun = new List<String>();

            if (Directory.Exists(directoryPath))
            {
                var pattern = @"(/wait\s+)(\w+.*?.cmd)";

                var files = Directory.EnumerateFiles(directoryPath, fileName);

                foreach (var file in files)
                {
                    var fileToCheckForRuns = File.ReadAllText(file);

                    foreach (Match match in Regex.Matches(fileToCheckForRuns, pattern))
                    {
                        batchFilesToRun.Add(match.Groups[2].Value);
                    }
                }
            }

            return batchFilesToRun;
        }

        private List<RunInfo> GetTestThatRan(List<String> batchFiles, DateTime startTime, DateTime endTime)
        {
            var fileLocation = FileBasePath + VirtualMachine.Name;
            var pattern = "(Tellurium.*?-project\\s+\\\")(.*?)(\\\"\\s+.*?-DefaultTestMode\\s+)((\\\".*?\\\")|(.*?))(\\s+)";

            var runInfos = new List<RunInfo>();

            foreach (var file in batchFiles)
            {
                var fileToCheck = fileLocation + @"\" + file;
                var fileString = File.ReadAllText(fileToCheck);

                foreach (Match match in Regex.Matches(fileString, pattern))
                {
                    String project = match.Groups[2].Value.Trim('"');
                    String testMode = match.Groups[4].Value.Trim('"');

                    runInfos.Add(RunInfo.getInstance(project, testMode, VirtualMachine.Name, startTime, endTime));
                }
            }
            return runInfos;
        }

        private void setMissingNoonRuns()
        {
            foreach (var run in ExpectedNoonRuns)
            {
                if (!ActualNoonRuns.Contains(run))
                    MissingNoonRuns.Add(run);
            }
        }

        private void setMissingNightRuns()
        {
            foreach (var run in ExpectedNightRuns)
            {
                if (!ActualNightRuns.Contains(run))
                    MissingNightRuns.Add(run);
            }
        }

        private void setFailure()
        {
            _isFailure = (MissingNoonRuns.Count() + MissingNightRuns.Count() > 0);
        }

        private void setFailureMessage()
        {
            if (IsFailure == false)
            {
                _failureMessage = "No Failure";
                return;
            }

            var message = Message;

            if (MissingNoonRuns.Count > 0)
                message = message + GenerateMissingRunMessage("NOON", MissingNoonRuns);

            if (MissingNightRuns.Count > 0)
                message = message + GenerateMissingRunMessage("NIGHT", MissingNightRuns);

            _failureMessage = message; 
        }

        private String GenerateMissingRunMessage(String runType, List<RunInfo> runs)
        {
            var message = "";

            if (MissingNoonRuns.Count > 0)
            {
                message = " " + runType + "- ";
                foreach (var run in runs)
                    message = message + run.Project + ",";

                message.TrimEnd(',');
            }
            return message;
        }

        public Boolean IsFailure { get { return _isFailure; } }

        public String FailureMessage { get { return _failureMessage; } }
    }
}
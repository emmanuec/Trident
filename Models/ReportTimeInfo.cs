using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelluriumCore.Reporting.TestRun;
using Trident.Models.TestData;

namespace Trident.Models
{
    public class ReportTimeInfo
    {
        private static readonly List<ScheduledBatchRunInfo> ScheduledRunInfos =
                                    new List<ScheduledBatchRunInfo>(new[]
                                    {new ScheduledBatchRunInfo(13, 6, "noon"), new ScheduledBatchRunInfo(19, 15, "night")});

        private readonly TestProjectReport _testReport;
        private readonly String _name;

        public ReportTimeInfo(TestProjectReport report)
        {
            _testReport = report;
            _name = SetName();
        }

        private string SetName()
        {
            var reportBeginDate = _testReport.RunBeginTime;
            var reportEndDate = _testReport.RunEndTime;

            var name = "";

            //makes sure scheduled run info list is sorted by hour
            ScheduledRunInfos.Sort((runInfo1, runInfo2) => runInfo1.BeginHour.CompareTo(runInfo2.BeginHour));

            foreach (var runInfo in ScheduledRunInfos)
            {
                var dateBaseLineDate = new DateTime(reportBeginDate.Year, reportBeginDate.Month, reportBeginDate.Day);
                var dateBaseLineStartDate = dateBaseLineDate.AddHours(runInfo.BeginHour);

                if (reportBeginDate >= dateBaseLineStartDate)
                {
                    name = dateBaseLineStartDate.DayOfWeek + " " + runInfo.Name;
                }
            }

            //If report date does not match any from schedule batch, it is because it is the day before and the most latests schedule run info
            if (String.IsNullOrEmpty(name))
            {
                name = reportBeginDate.AddDays(-1).DayOfWeek + " " + ScheduledRunInfos.ElementAt(ScheduledRunInfos.Count - 1).Name;
            }

            return name;
        }

        public TestProjectReport TestReport
        {
            get { return _testReport; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
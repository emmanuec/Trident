using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelluriumCore.Logging;
using TelluriumCore.Reporting.TestRun;
using TelluriumCore.TestCaseExecution;
using TelluriumCore.UINavigation;
using Trident.Models.TestData;

namespace Trident.Models
{
    public class TestProjectReportViewModel
    {
        private readonly String _projectName;
        private readonly String _testMode;
        private readonly BrowserName _browser;
        private readonly String _virtualMachine;
        private List<ReportTimeInfo> _runTimeInfo = new List<ReportTimeInfo>();

        private TestProjectReportViewModel(TestProjectReport report)
        {
            _projectName = report.ProjectName;
            _testMode = report.TestMode;
            _browser = report.Browser;
            _virtualMachine = report.MachineName;
            _runTimeInfo.Add(new ReportTimeInfo(report));
        }

        public void AddToRunTimeInfo(TestProjectReport report)
        {
            _runTimeInfo.Add(new ReportTimeInfo(report));
        }

        public string ProjectName
        {
            get { return _projectName; }
        }

        public String TestMode
        {
            get { return _testMode; }
        }

        public BrowserName Browser
        {
            get { return _browser; }
        }

        public String VirtualMachine
        {
            get { return _virtualMachine; }
        }

        public List<ReportTimeInfo> RunTimeInfo
        {
            get { return _runTimeInfo; }
        }

        public static List<TestProjectReportViewModel> GetInstances(List<TestProjectReport> reports)
        {
            return OrganizeIntoTestRunCollections(reports);
        }

        private static List<TestProjectReportViewModel> OrganizeIntoTestRunCollections(IList<TestProjectReport> reports)
        {
            var testReports = new List<TestProjectReportViewModel>();

            foreach(var report in reports)
            {   
                if (!testReports.Any(test =>
                        test.ProjectName.Equals(report.ProjectName)
                        && test.TestMode.Equals(report.TestMode)
                        && test.Browser.Equals(report.Browser)
                        && test.VirtualMachine.Equals(report.MachineName))) 
                {
                    testReports.Add(new TestProjectReportViewModel(report));
                }
                else
                {
                    testReports.Find(test => test.ProjectName.Equals(report.ProjectName)
                                      && test.TestMode.Equals(report.TestMode)
                                      && test.Browser.Equals(report.Browser)
                                      && test.VirtualMachine.Equals(report.MachineName)).AddToRunTimeInfo(report);
                }
            }

            testReports.Sort((report1, report2) => report1.ProjectName.CompareTo(report2.ProjectName));

            return testReports;
        }
    }
}
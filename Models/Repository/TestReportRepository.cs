using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelluriumCore.Reporting.TestRun;
using TelluriumCore.Reporting.TestCase;
using System.Web;
using Trident.Models;

namespace PainFree.Helpers.TestData
{
    public static class TestDataRepository
    {
        private const string CacheKey = "TestFailures";

        /** Note that ITestReport End Time is invalid **/
        //C:\Users\ecastill\Desktop\test\
        private static readonly string testDataPath = @"S:\IS\QA\Automation\Tellurium Results";
        //private static readonly string testDataPath = HttpServerUtility.MapPath("~") +  @"VMTEST01\Billing\";
        //private static readonly string testDataPath = @"C:\Users\ecastill\Desktop\test\";
        /*TODO:Add validation that date range is valid for a single day model */
        public static List<ITestReport> GetData(DateTime beginTime, DateTime endTime)
        {
            var testReports = TestReport.GetAllInstances(null, testDataPath, beginTime);
            var filteredTestReports = from report in testReports
                                      where report.RunEndTime <= endTime
                                      select report;
            return filteredTestReports.ToList();

            ;
            /*var dataList = new List<ITestReport>();

            var context = HttpContext.Current;

            if(context != null)
            {
                if(context.Cache[CacheKey] == null)
                {
                    var testReports = TestReport.GetAllInstances(null, testDataPath, beginTime);
                    var filteredTestReports = from report in testReports
                                              where report.RunEndTime <= endTime
                                              select report;

                    context.Cache[CacheKey] = filteredTestReports;

                }
            }

            dataList = (List<ITestReport>)context.Cache[CacheKey];

            return dataList;*/
        }

        public static IList<ITestReport> GetWeeklyData(DateTime beginTime, String test)
        {
            //var testReports = TestReport.GetAllInstances(null, testDataPath, beginTime);
            var testReports = TestReport.GetAllInstances(null, test, beginTime);
            //var weeklyTestsReports = TestProjectReportViewModel.GetInstances(testReports);
          /*  var filteredWeeklyTestReports = from report in weeklyTestsReports
                                            where report.VirtualMachine.Contains("VMTEST") ||
                                                 report.VirtualMachine.Contains("VMAUTOMATION")
                                            select report;*/

            return testReports.ToList();
        }

        /*TODO:Add Code to update data*/
        public static IList<ITestReport> getNoonData(DateTime beginTime, List<ITestReport> testData)
        {
            var noonStartTime = beginTime;
            var noonEndTime = beginTime.Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm

            var noonTestData = from report in testData
                               where report.RunBeginTime >= noonStartTime
                               where report.RunBeginTime <= noonEndTime
                               where report.MachineName.Contains("VMTEST") || 
                                     report.MachineName.Contains("VMAUTOMATION")
                               select report;

            return noonTestData.ToList();
        }

        public static IList<ITestReport> getNightData(DateTime endTime, List<ITestReport> testData)
        {
            var nightStartTime = endTime.AddYears(-1).Date.Add(new TimeSpan(19, 30, 0)); // 7:30 pm
            var nightEndTime = endTime;

            var nightTestReports = from report in testData
                                   where report.RunBeginTime >= nightStartTime
                                   where report.RunBeginTime <= endTime
                                   where report.MachineName.Contains("VMTEST") ||
                                         report.MachineName.Contains("VMAUTOMATION")
                                   select report;

            return nightTestReports.ToList();
        }
    }
}

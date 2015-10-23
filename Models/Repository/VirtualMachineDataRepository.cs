using PainFree.Helpers.TestData;
using PainFree.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TelluriumCore.Reporting.TestRun;

namespace PainFree.Models.Repository
{
    public class VirtualMachineDataRepository
    {
        private static readonly DateTime dataBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
        private static readonly DateTime dataEndTime = DateTime.Now.Date.Add(new TimeSpan(13, 30, 0));
        private List<ITestReport> TestReports = TestDataRepository.GetData(dataBeginTime, dataEndTime);

        public List<VirtualMachineStatusReport> GetVmFailureReports()
        {
            var vmList = VirtualMachineList.GetAllVirtualMachines();
            var vmReportList = new List<VirtualMachineStatusReport>();
            
            foreach(var vm in vmList)
            {
                var vmFilterTestReports = GetFilteredTestReports(vm);
                var virtualMachine = new VirtualMachine(vm, vmFilterTestReports, dataBeginTime, dataEndTime);
                vmReportList.Add(new VirtualMachineStatusReport(virtualMachine));
            }

            var failureVMList = from report in vmReportList
                                where report.AllTestRunFailure.IsFailure == true
                                || report.JavaUpdateFailure.IsFailure == true
                                || report.SvnUpdateFailure.IsFailure == true
                                || report.VmDownFailure.IsFailure == true
                                || report.WebDriverFailure.IsFailure == true
                                select report;

            return failureVMList.ToList();
        }

        private List<ITestReport> GetFilteredTestReports(String virtualMachine)
        {
            var filteredReports = from report in TestReports
                                  where report.MachineName.Equals(virtualMachine)
                                  select report;

            return filteredReports.ToList();
        }

    }
}
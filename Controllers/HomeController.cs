using PainFree.Helpers.TestData;
using PainFree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trident.Models.Repository;

namespace PainFree.Controllers
{
    public class HomeController : Controller
    {
        private static int NumberOfDaysToReportOn = 5;
        //public static readonly DateTime dataBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
        private readonly DateTime dataBeginTime = DateTime.Now.Subtract(new TimeSpan(NumberOfDaysToReportOn, 0, 0, 0));
        //public static readonly DateTime dataEndTime = DateTime.Now.Date.Add(new TimeSpan(13, 30, 0));
        private readonly DateTime dataEndTime = DateTime.Now;

        private TestReportDB db = new TestReportDB();

        public ActionResult Index()
        {
            // var testing = HttpContext.Server.MapPath("/AUTOMATION/Tellurium Results/");

            //var testDataItems = TestDataRepository.GetWeeklyData(dataBeginTime, testing);
            //var failedTestDataItems = testDataItems.Where(data => data.NumberOfFailures > 0).ToList();

            var testDataItems = db.GetWeeklyReports();

           if (testDataItems.Count > 0)
            {
                var dataTimeSlots = new List<string>();
                var maxRunTimes = testDataItems.Where(data => data.RunTimeInfo.Count >= NumberOfDaysToReportOn * 2).Select(info => info.RunTimeInfo).First();

                foreach (var timeInfo in maxRunTimes)
                {
                    dataTimeSlots.Add(timeInfo.Name);
                }

                ViewBag.TimeSlots = dataTimeSlots;
            }

            return View(testDataItems);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
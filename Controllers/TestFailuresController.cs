using Newtonsoft.Json;
using PainFree.Helpers.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TelluriumCore.Reporting.TestRun;

namespace PainFree.Controllers
{
    [Route("failurereport")]
   // [EnableCors(origins: "http://localhost:58381/api/failurereport", headers: "*", methods: "*")]
    public class TestFailuresController : ApiController
    {
        public static readonly DateTime dataBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
        public static readonly DateTime dataEndTime = DateTime.Now.Date.Add(new TimeSpan(13, 30, 0));

        public IEnumerable<ITestReport> Get()
        {
            List<ITestReport> testData = TestDataRepository.GetData(dataBeginTime, dataEndTime);
            var failedTestDataItems = testData.Where(data => data.NumberOfFailures > 0).ToList();
            var test = new List<ITestReport>();
            test.Add(failedTestDataItems.ElementAt(0));
            test.Add(failedTestDataItems.ElementAt(1));
            return test;
        }

        private string test()
        {
            string json = JsonConvert.SerializeObject(new
            {
                results = new List<Result>()
                {
                    new Result { id = 1, value = "ABC", info = "ABC" },
                    new Result { id = 2, value = "JKL", info = "JKL" }
                }
            });

            return json;
        }

        class Result
        {
            public int id { get; set; }
            public string value { get; set; }
            public string info { get; set; }
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelluriumCore.Reporting.TestCase;
using TelluriumCore.TestCaseExecution;

namespace Trident.Models.TestData
{
    public class TestProjectReport
    {
        public ObjectId id { get; set; }
        public bool HadFatalError { get; set; }
        public String MachineName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RunBeginTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime RunEndTime { get; set; }
        public Boolean TestRunTimedOut { get; set; }
        public TestEnvironment Environment { get; set; }
        [BsonElementAttribute("TestedBrowser")]
        public BrowserName Browser{ get; set; }
        public String TestMode { get; set; }
        public UInt32 NumberTestCasesLoaded { get; set; }
        public UInt32 NumberOfFailedLoads { get; set; }
        public String ProgramName { get; set; }
        public String ProjectName { get; set; }
        public String Platform { get; set; }
        public UInt32 NumberOfFailures { get; set; }
        public UInt32 NumberOfPasses { get; set; }
        public string[] ErrorMessages { get; set; }
        [BsonElementAttribute("TestCaseRunDetails")]
        public TestCaseReport[] TestCaseRunDetails { get; set; }
    }
}
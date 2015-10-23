using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.TestData
{
    public class TransactionReport
    {
        public ObjectId id { get; set; }
        public Boolean AbnormalExit { get; set; }
        public Boolean Completed { get; set; }
        public String Description { get; set; }
        public String Identifier { get; set; }
        public String Name { get; set; }
        public UInt32 Number { get; set; }
        public UInt32 NumberOfPasses { get; set; }
        public Boolean Passed { get; set; }
        public Boolean Started { get; set; }
        public FailureReport[] FailureDetails;
        public FailureReport[] KnownFailureDetails;
        public FailureReport[] TodoDetails;
    }
}
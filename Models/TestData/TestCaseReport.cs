using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trident.Models.TestData
{
    public class TestCaseReport
    {
        public ObjectId id { get; set; }
        public Boolean AbnormalExit { get; set; }
        public Boolean Completed { get; set; }
        public String Description { get; set; }
        public String FileName { get; set; }
        public String Identifier { get; set; }
        public Boolean LoadSuccessful { get; set; }
        public String Name { get; set; }
        public UInt32 NumberFailedTransactions { get; set; }
        public UInt32 NumberPassedTransactions { get; set; }
        public UInt32 NumberTransactions { get; set; }
        public Boolean Passed { get; set; }
        public Boolean Started { get; set; }
        public Boolean TestingTimedOut { get; set; }
        [BsonElementAttribute("TransactionRunDetailDtos")]
        public TransactionReport[] TransactionReport { get; set; }
    }
}
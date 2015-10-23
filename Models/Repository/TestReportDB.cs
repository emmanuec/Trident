using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Trident.Models.TestData;

namespace Trident.Models.Repository
{
    public class TestReportDB
    {
        private const string Address = @"mongodb://localhost";
        private const string DatabaseName = "Automation";
        private const string CollectionName = "Dev";
        private readonly MongoClient Client;
        private readonly IMongoDatabase Database;
        private readonly IMongoCollection<TestProjectReport> Collection;

        public TestReportDB()
        {
            Client = new MongoClient(Address);
            Database = Client.GetDatabase(DatabaseName);
            Collection = Database.GetCollection<TestProjectReport>(CollectionName);
        }

        public async Task<List<TestProjectReport>> GetReports()
        {
            var data = await Collection.Find(_ => true).ToListAsync();

            return data;
        }

        public List<TestProjectReportViewModel> GetWeeklyReports()
        {
            var NumberOfDaysToReportOn = 5;
            //public static readonly DateTime dataBeginTime = DateTime.Now.AddDays(-1).Date.Add(new TimeSpan(13, 30, 0));
            var dataBeginTime = DateTime.Now.Subtract(new TimeSpan(NumberOfDaysToReportOn, 0, 0, 0));
            var bsonDataBeginTime = BsonDateTime.Create(dataBeginTime);

            var dataFilter = Builders<TestProjectReport>.Filter.Gte("RunBeginTime", bsonDataBeginTime);
            var filteredData = Collection.Find(dataFilter).ToListAsync();
            filteredData.Wait();
            var data = filteredData.Result;

            var weeklyTestsReports = TestProjectReportViewModel.GetInstances(data);
            var filteredWeeklyTestReports = from report in weeklyTestsReports
                                            where report.VirtualMachine.Contains("VMTEST") ||
                                                 report.VirtualMachine.Contains("VMAUTOMATION")
                                            select report;

            return filteredWeeklyTestReports.ToList();
        }

        private async Task<List<TestProjectReport>> GetFilteredData(FilterDefinition<TestProjectReport> filter)
        {
            var data = await Collection.Find(filter).ToListAsync();

            return data;
        }


    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Messages;


namespace Tests
{
    [TestClass]
    public class ReadRepositoryTests
    {
        [TestMethod]
        public void ReadRepositoryTests_AddRead_Nominal()
        {
          /*  var now = DateTime.UtcNow;

            MongoClient client = new MongoClient(); // connect to localhost

            MongoCollection<ReadHasIncomeMessage> dataReads = client.GetServer().GetDatabase("test").GetCollection<ReadHasIncomeMessage>("DataReadsTests");
            ReadHasIncomeMessage read = new ReadHasIncomeMessage
            {
               SerialNumber="sn",
               ReadTimeStamp = now

            };

            ReadHasIncomeMessage oldRead = new ReadHasIncomeMessage
            {
                SerialNumber = "sn",
                ReadTimeStamp = now.AddDays(-5)

            };

            dataReads.Insert(read);
            dataReads.Insert(oldRead);

            DataReadsRepository rep = new DataReadsRepository();

            Assert.IsTrue(rep.GetDataReadsByDates(now).Count == 1);

            dataReads.Drop();*/
        }
    }
}

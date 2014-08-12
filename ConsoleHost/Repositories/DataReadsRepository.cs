using Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;

namespace Repositories
{
    public class DataReadsRepository
    {
        public void AddDataRead(DataRead readToAdd)
        {
            MongoClient client = new MongoClient();

            MongoCollection<DataRead> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataRead>("DataReads");

            dataReads.Insert(readToAdd);
           
        }


        public List<DataRead> GetDataReadsByDates(DateTime fromReadTimeStampInUTC,DateTime? toReadTimeStampInUTC=null)
        { 
            MongoClient client = new MongoClient();

            MongoCollection<DataRead> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataRead>("DataReads");

            var result = dataReads.AsQueryable<DataRead>().Where(r => r.ReadTimeStamp >= fromReadTimeStampInUTC);

            if (toReadTimeStampInUTC.HasValue)
            { 
                result=result.Where(r => r.ReadTimeStamp <= fromReadTimeStampInUTC);
            }

            return result.ToList();


        }

        public void DeleteRead(string id)
        {
            MongoClient client = new MongoClient();

            MongoCollection<DataRead> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataRead>("DataReads");

            var query = Query<DataRead>.EQ(e => e.id, new ObjectId(id));

            dataReads.Remove(query);

        }
    }
}

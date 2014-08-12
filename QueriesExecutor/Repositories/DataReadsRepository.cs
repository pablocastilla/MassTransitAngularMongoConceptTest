
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;
using QueriesUpdater.DTOs;

namespace QueriesUpdater.Repositories
{
    public class DataReadsRepository
    {
        public void AddDataRead(DataReadDto readToAdd)
        {
            MongoClient client = new MongoClient();

            MongoCollection<DataReadDto> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataReadDto>("DataReadsDtos");

            dataReads.Insert(readToAdd);
           
        }


        public List<DataReadDto> GetDataReadsByDates(DateTime fromReadTimeStampInUTC, DateTime? toReadTimeStampInUTC = null)
        { 
            MongoClient client = new MongoClient();

            MongoCollection<DataReadDto> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataReadDto>("DataReadsDtos");

            var result = dataReads.AsQueryable<DataReadDto>().Where(r => r.ReadTimeStamp >= fromReadTimeStampInUTC);

            if (toReadTimeStampInUTC.HasValue)
            { 
                result=result.Where(r => r.ReadTimeStamp <= fromReadTimeStampInUTC);
            }

            return result.ToList();


        }

        public void DeleteRead(string id)
        {
            MongoClient client = new MongoClient();

            MongoCollection<DataReadDto> dataReads = client.GetServer().GetDatabase("test").GetCollection<DataReadDto>("DataReadsDtos");

            var query = Query<DataReadDto>.EQ(e => e.id, new ObjectId(id));

            dataReads.Remove(query);

        }
    }
}

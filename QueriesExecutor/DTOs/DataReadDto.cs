using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace QueriesUpdater.DTOs
{
    public class DataReadDto
    {
        [BsonId]
        public MongoDB.Bson.ObjectId id { get; set; }

        public string SerialNumber { get; set; }

        public DateTime ReadTimeStamp { get; set; }

        public float Value { get; set; }

    }
}
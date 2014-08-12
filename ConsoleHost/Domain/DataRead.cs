using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Read entity.
    /// </summary>
    public class DataRead
    {
        [BsonId]
        public MongoDB.Bson.ObjectId id { get; set; }

        public string SerialNumber { get; set; }

        public DateTime ReadTimeStamp { get; set; }

        public float Value { get; set; }

        public Guid CommandId { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Messages
{
     [Serializable]
    public class InsertReadCommand : CorrelatedBy<Guid>
    {
        public string SerialNumber { get; set; }

        public DateTime ReadTimeStamp { get; set; }

        public float Value { get; set; }

        public Guid CorrelationId
        { get; set; }
    }
}

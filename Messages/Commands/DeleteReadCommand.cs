using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MassTransit;

namespace Messages
{
    /// <summary>
    /// Message that indicates that a read wants to be deleted
    /// </summary>
     [Serializable]
    public class DeleteReadMessage : CorrelatedBy<Guid>
    {
        public string id { get; set; }

        public Guid CorrelationId
        { get; set; }
    }
}

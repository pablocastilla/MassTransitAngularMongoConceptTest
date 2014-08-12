using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Messages
{
     [Serializable]
    public class ReadsCollectionChangedMessage : CorrelatedBy<Guid>
    {
         public Guid CorrelationId
         { get; set; }
    }
}

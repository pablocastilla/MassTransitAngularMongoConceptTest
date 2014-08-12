using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Messages.Engines
{
    /// <summary>
    /// Message for launching an engine, a small process that has to be continuously running.
    /// </summary>
    [Serializable]
    public class LaunchEngineMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId
        { get; set; }
    }
}

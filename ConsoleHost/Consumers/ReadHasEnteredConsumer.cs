using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MassTransit;
using Messages;
using Repositories;

namespace ConsoleHost.Consumers
{
    /// <summary>
    /// Consumes the ReadHasEnteredMessage and stores the read.
    /// </summary>
    public class ReadHasEnteredConsumer : Consumes<InsertReadCommand>.All
    {
        public void Consume(InsertReadCommand msg)
        {
            Console.WriteLine("New read inserted");
            System.Threading.Thread.Sleep(1000);

            DataReadsRepository rep = new DataReadsRepository();

            rep.AddDataRead(new DataRead()
                            {
                                ReadTimeStamp=msg.ReadTimeStamp,
                                SerialNumber=msg.SerialNumber,
                                Value=msg.Value,
                                CommandId=msg.CorrelationId
                            });

            Bus.Instance.Publish(new ReadsCollectionChangedMessage() { CorrelationId = msg.CorrelationId });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using QueriesUpdater.DTOs;
using QueriesUpdater.Repositories;


namespace QueriesUpdater.Consumers
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

            rep.AddDataRead(new DataReadDto()
                            {
                                ReadTimeStamp=msg.ReadTimeStamp,
                                SerialNumber=msg.SerialNumber,
                                Value=msg.Value
                            });

        
        }
    }
}

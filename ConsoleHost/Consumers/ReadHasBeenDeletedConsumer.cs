﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Repositories;

namespace ConsoleHost.Consumers
{
    /// <summary>
    /// Consumes the ReadHasBeenDeletedMessage messages and deletes the read.
    /// </summary>
    public class ReadHasBeenDeletedConsumer : Consumes<DeleteReadMessage>.All
    {
        public void Consume(DeleteReadMessage message)
        {
            Console.WriteLine("Read deleted");

            DataReadsRepository rep = new DataReadsRepository();

            rep.DeleteRead(message.id);

            Bus.Instance.Publish(new ReadsCollectionChangedMessage() { CorrelationId = message.CorrelationId });
        }
    }
}

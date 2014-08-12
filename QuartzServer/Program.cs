using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.QuartzIntegration;
using Messages;
using Quartz;
using Quartz.Impl;
using MassTransit.Scheduling;
using Magnum.Extensions;
using Messages.Engines;

namespace QuartzServer
{
    class Program
    {
        static void Main(string[] args)
        {

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
          
            IServiceBus bus = ServiceBusFactory.New(x =>
            {
                x.UseRabbitMq();
                x.ReceiveFrom("rabbitmq://localhost/quartz");

               
                x.Subscribe(s =>
                {
                   

                    s.Consumer(() => new ScheduleMessageConsumer(scheduler));
                    s.Consumer(() => new CancelScheduledMessageConsumer(scheduler));
                                       
                });
            });             
           
          
            // and start it off
            scheduler.JobFactory = new MassTransitJobFactory(bus);
            scheduler.Start();

                     

            while (true)
            {
               

                Thread.Sleep(10000);
                              
            }

      

           
        }

     
    }
}

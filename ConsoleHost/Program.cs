using Domain;
using MassTransit;
using Messages;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Saga;
using ConsoleHost.Sagas;
using MassTransit.Services.Timeout.Messages;
using MassTransit.Services.Timeout;
using MassTransit.Services.Timeout.Server;
using MassTransit.QuartzIntegration;
using MassTransit.Scheduling;
using StructureMap;
using ConsoleHost.Consumers;
using Messages.Engines;
using Magnum.Extensions;

namespace ConsoleHost
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Reads management service...");


            //Search for every consumer and add to the container
            var container = new Container(cfg =>
            {
                cfg.Scan(x =>
                {
                    x.TheCallingAssembly();                  
                    x.AddAllTypesOf(typeof(IConsumer));                                      
                });            
                            
            });
                               

            Bus.Initialize(sbc =>
            {

                sbc.UseRabbitMq();

                sbc.ReceiveFrom("rabbitmq://localhost/mt_readsmanagementservice_receive_queue");
                

               // sbc.UseControlBus();

                /*sbc.UseMsmq(
                    c =>      
                        {
                           // c.VerifyMsmqConfiguration();
                            //c.UseMulticastSubscriptionClient();
                            c.UseSubscriptionService("msmq://localhost/mt_subscriptions");
                          
                                                  
                        }              
                    );   */
                              

                sbc.Subscribe(subs =>
                {
                    subs.Saga<ReadsCollectorSaga>(new InMemorySagaRepository<ReadsCollectorSaga>()).Permanent();
                                  
                  
                });

                sbc.Subscribe(x => x.LoadFrom(container));
              
            });

            container.Inject(Bus.Instance);

            var guid = Guid.NewGuid();

            //launches a saga in the future. It uses the QuartzServer. 
            Bus.Instance.ScheduleMessage(3.Seconds().FromUtcNow(), new LaunchEngineMessage() { CorrelationId = guid }); 

        

            while (true)
            {
                Thread.Sleep(10000);
                
               
            }
        }

        

    }
}

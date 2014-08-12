using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using StructureMap;

namespace QueriesExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Queries executor event listener....");

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

                sbc.ReceiveFrom("rabbitmq://localhost/mt_queriesexecutoreventlistener_receive_queue");


                // sbc.UseControlBus();

                /*sbc.UseMsmq(
                    c =>      
                        {
                           // c.VerifyMsmqConfiguration();
                            //c.UseMulticastSubscriptionClient();
                            c.UseSubscriptionService("msmq://localhost/mt_subscriptions");
                          
                                                  
                        }              
                    );   */


                sbc.Subscribe(x => x.LoadFrom(container));

            });

            container.Inject(Bus.Instance);

          


            while (true)
            {
                Thread.Sleep(10000);


            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magnum.StateMachine;
using MassTransit;
using MassTransit.Saga;
using MassTransit.Services.Timeout.Messages;
using Messages;
using Messages.Engines;
using MassTransit.Scheduling;
using Magnum.Extensions;
using Domain;

namespace ConsoleHost.Sagas
{
    /// <summary>
    /// This is just an example of a Saga that executes a process and before going to complete state it launches its next logic execution.
    /// </summary>
    public class ReadsCollectorSaga : SagaStateMachine<ReadsCollectorSaga>,ISaga
    {
        public IServiceBus Bus
        { get; set; }

        public Guid CorrelationId
        { get; set; }


        /************ states ***********/
        public static State Initial { get; set; }

        public static State Collecting { get; set; }

        public static State Completed { get; set; }
               

        public static Event<LaunchEngineMessage> InitializeCollector { get; set; }
      

        public ReadsCollectorSaga(Guid correlationId)
        {
            CorrelationId = correlationId;
        }


        static ReadsCollectorSaga()
        {
            Define(() =>
            {
                Initially(
                    When(InitializeCollector)
                        .TransitionTo(Collecting)
                        .Then((saga, message) => 
                        { 
                            //execute collection logic, in this case a fake read is sent to the bus
                            saga.Bus.Publish(new InsertReadCommand 
                                                {                                                    
                                                        ReadTimeStamp=DateTime.UtcNow ,
                                                        SerialNumber = "sn" + DateTime.UtcNow.Second,
                                                        Value = DateTime.UtcNow.Second,
                                                        CorrelationId = Guid.NewGuid() 
                                                });                

                            //send message for next execution
                            saga.Bus.ScheduleMessage(10.Seconds().FromUtcNow(), new LaunchEngineMessage() { CorrelationId = Guid.NewGuid() }); 
                        }).Complete()
                      
                        );

                During(Collecting, When(InitializeCollector).Then((saga, message) => { 
                    //Singleton instance, only one could be executed
                }));
                
            });
        }


    }
}

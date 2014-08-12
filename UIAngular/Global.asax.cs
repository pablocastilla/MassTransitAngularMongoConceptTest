using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using UIAngular.App_Start;
using Messages;
using Microsoft.AspNet.SignalR;
using UIAngular.Hubs;

namespace UIAngular
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            

            Bus.Initialize(sbc =>
            {
                /*  sbc.UseMsmq(
                      c =>
                      {
                          c.VerifyMsmqConfiguration();
                          c.UseMulticastSubscriptionClient();
                          c.UseSubscriptionService("msmq://localhost/mt_subscriptions");


                      }
                      );
                  sbc.SetCreateTransactionalQueues(true);*/

                sbc.ReceiveFrom("rabbitmq://localhost/mt_receive_queue_ui");

                sbc.UseRabbitMq();

                
                //subscribes to this message to communicate the changes to the clients
                sbc.Subscribe(subs =>
                {
                    subs.Handler<ReadsCollectionChangedMessage>((ctx, msg) =>
                    {
                        //calls to readInsertionFinished in every client
                        var hub = GlobalHost.ConnectionManager.GetHubContext<NonPersistentHub>();
                        hub.Clients.All.readInsertionFinished(msg.CorrelationId.ToString());

                    });
                });
            });
        }
    }
}

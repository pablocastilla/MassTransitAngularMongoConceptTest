using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using MassTransit;
using Messages;
using Microsoft.AspNet.SignalR;
using QueriesUpdater.DTOs;
using QueriesUpdater.Repositories;
using UIAngular.Hubs;

namespace UIAngular
{
    public class ReadsController : ApiController
    {
        // GET api/<controller>
        
        /// <summary>
        /// Read are made directly to the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataReadDto> GetAllReads()
        {
            var rep = new DataReadsRepository();

            return rep.GetDataReadsByDates(DateTime.UtcNow.AddDays(-90));
        }


        /// <summary>
        /// Sends an InsertRead command and returns the guid of the command.
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        public CommandRequestInfoDto Post([FromBody] DataReadDto read)
        {
            System.Threading.Thread.Sleep(1000);

            var guid = Guid.NewGuid();
            
            Bus.Instance.Publish(new InsertReadCommand 
                                    { 
                                        SerialNumber=read.SerialNumber,
                                        ReadTimeStamp = read.ReadTimeStamp,
                                        Value=read.Value,
                                        CorrelationId=guid 
                                    });                

            return new CommandRequestInfoDto(){TransactionID=guid.ToString()};
        }
              

        public void Delete(string id)
        {
            Bus.Instance.Publish(new DeleteReadMessage { id = id });
                    
        }
             
    }
}
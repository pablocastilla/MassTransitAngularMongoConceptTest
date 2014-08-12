
using MassTransit;
using Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIAngular.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult CreateRead()
        {
            System.Threading.Thread.Sleep(1000);
            return View();
        }

      
     
        public ActionResult ViewReads()
        {
            System.Threading.Thread.Sleep(1000);
            return View();
        }
    }
}
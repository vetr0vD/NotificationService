using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetStatus()
        {
            WorkNotification data = new Proxies.ServiceProxy().GetWorkStatus(DateTime.Now);
            return PartialView("GetStatus", data);
        }

        [HttpPost]
        public void AddItem(Data.WorkNotification notification)
        {
            (new Proxies.ServiceProxy()).SetWorkStatus(notification);
        }
    }
}
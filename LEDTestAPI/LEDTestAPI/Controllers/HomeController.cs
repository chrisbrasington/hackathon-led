using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEDTestAPI.Controllers
{
   
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewBag.Title = "Home Page";

         return View();
      }

      [HttpPost]
      public bool ShowText(string text)
      {
         if (!string.IsNullOrEmpty(text))
         {
            return true;
         }
         return false;
      }
   }
}

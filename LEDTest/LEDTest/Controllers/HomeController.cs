using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using LEDTest.Models;

namespace LEDTest.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index(ViewModel model)
      {
         if (!string.IsNullOrEmpty(model.Text))
         {

            SentText(model.Text);
         }
         return View(model);
      }

      public ActionResult About()
      {
         ViewBag.Message = "Your application description page.";

         return View();
      }

      public ActionResult Contact()
      {
         ViewBag.Message = "Your contact page.";

         return View();
      }


      private void SentText(string text)
      {
         SerialPort arduino = new SerialPort();
         arduino.BaudRate = 115200;
         arduino.PortName = "COM7";
         arduino.Open();


         if (arduino.IsOpen)
         {
            arduino.Write(text);
         }

         arduino.Close();
      }
   }
}
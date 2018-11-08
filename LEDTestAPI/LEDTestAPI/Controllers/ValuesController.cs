using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEDTestAPI.Controllers
{
   public class ValuesController : ApiController
   {
      // GET api/values
      public IEnumerable<string> Get()
      {
         return new string[] { "value1", "value2" };
      }

      // GET api/values/5
      public string Get(int id)
      {
         return "value";
      }

      // POST api/values
      public void Post([FromUri]string value)
      {
         if (value == null)
         {
            return;
         }

         SerialPort arduino = new SerialPort();
         arduino.BaudRate = 115200;
         arduino.PortName = "COM7";
         arduino.Open();

         try
         {
            if (arduino.IsOpen)
            {
               arduino.Write(value);
            }
         }
         catch
         { }

         arduino.Close();
      }

      // PUT api/values/5
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/values/5
      public void Delete(int id)
      {
      }
   }
}

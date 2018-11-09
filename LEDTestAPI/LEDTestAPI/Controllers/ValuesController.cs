using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDTestAPI.Services;

namespace LEDTestAPI.Controllers
{

   [RoutePrefix("api")]
   public class ValuesController : ApiController
   {
      private readonly SerialService _serialService;

      /// <summary>
      /// Initializes an instance of class <see cref="ValuesController"/>
      /// </summary>
      public ValuesController(SerialService serialService)
      {
         _serialService = serialService;
      }

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
      [HttpPost]
      [Route("message")]
      public void Post([FromUri]string value)
      {
         if (value == null)
         {
            return;
         }

         try
         {
            if (_serialService.IsOpen)
            {
               List<string> words = new List<string>();

               int i = 0;

               foreach (string s in value.Split(' '))
               {
                  if (i % 2 == 0)
                  {
                     words.Add(s);
                  }
                  else
                  {
                     words[words.Count-1] += " " + s;
                  }
                  i++;
               }

               foreach (string s in words)
               {
                  _serialService.SendText("0" + s);
                  System.Threading.Thread.Sleep(2000);
               }

               System.Threading.Thread.Sleep(4000);

               _serialService.SendText("0");
            }
         }
         catch
         { }
      }

      // POST api/values
      [HttpPost]
      [Route("scrolling")]
      public void PostScrolling([FromUri]string value)
      {
         if (value == null)
         {
            return;
         }

         try
         {
            if (_serialService.IsOpen)
            {
               _serialService.SendText("1"+value);
            }
         }
         catch
         { }
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

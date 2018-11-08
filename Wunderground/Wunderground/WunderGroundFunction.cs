using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Wunderground
{
   public static class WunderGroundFunction
   {
      [FunctionName("Function1")]
      public static async void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
      {
         log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
         using (var client = new HttpClient())
         {
            client.BaseAddress = new Uri("http://api.wunderground.com/api/faded1290e956eb4/conditions/q/80111.json");
            var result = await client.GetAsync("");
            string resultContent = await result.Content.ReadAsStringAsync();
            RootObject a = JsonConvert.DeserializeObject<RootObject>(resultContent);
            string text = $"{a.current_observation.temperature_string} {a.current_observation.weather}";
            log.Info(text);
            client.BaseAddress = new Uri("Chris' computer");
            client.PostAsync("Chris' computer", new StringContent(text));
         }

      }
   }
}

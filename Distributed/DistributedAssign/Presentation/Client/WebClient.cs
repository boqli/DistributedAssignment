using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherForecastRESTWebAPI.Client
{
    public class WebClient
    {

        private static WebClient webClientInstance = null;

        private WebClient() { }

        public static WebClient WebClientInstance
        {
            get
            {
                if(webClientInstance == null)
                {
                    webClientInstance = new WebClient();
                }

                return webClientInstance;
            }     
        }

        public async Task<JsonDocument> Request(HttpRequestMessage requestMessage)
        {
            string responseValue;

            var client = new HttpClient();

            using (var response = await client.SendAsync(requestMessage))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                responseValue = body;
            }

            return JsonDocument.Parse(responseValue);
        }
    }
}

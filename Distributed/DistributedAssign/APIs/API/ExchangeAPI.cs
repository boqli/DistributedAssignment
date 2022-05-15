using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using APIs.Client;

namespace APIs.API
{
    public class ExchangeAPI
    {
        private readonly string APIkey = "6TDc3sSJZ3uZMxr48WSRm2OvgaQfmziG";
        private readonly string hostEndPoint = "https://api.apilayer.com/exchangerates_data";
        private readonly string convertEndPoint = "convert";

        //Web Client
        private readonly WebClient webClient = WebClient.WebClientInstance;


        private static ExchangeAPI exchangeInstance = null;

        private ExchangeAPI() { }

        public static ExchangeAPI exchangeAPIInstance
        {
            get
            {
                if (exchangeInstance == null)
                {
                    exchangeInstance = new ExchangeAPI();
                }

                return exchangeInstance;
            }
        }

        private HttpRequestMessage GetRequestMessage(string endPoint, string queryString)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(hostEndPoint + "/" + endPoint + queryString),
                Headers =
                {
                    { "apikey", APIkey },
                },
            };
            return request;

        }

        public double convert(string queryString)
        {
            double money = 0;
            double final = 0;
            var request = GetRequestMessage(convertEndPoint, queryString);
            JsonDocument jsonDocument = webClient.Request(request).Result;
            JsonElement root = jsonDocument.RootElement;
            if (root.TryGetProperty("result", out JsonElement tempCELement))
            {
                money = tempCELement.GetDouble();
            }
            final = money;
            return final;
        }


    }
}

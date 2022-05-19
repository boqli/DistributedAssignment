using System;
using System.Net.Http;

namespace Presentation.Helper
{
    public class MicroServices
    {
        public HttpClient TransferMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44306/");
            return Client;
        }

        public HttpClient RegisterMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44342/");
            return Client;
        }

        public HttpClient FundMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44335/");
            return Client;
        }

        public HttpClient Audit()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44374/");
            return Client;
        }

        public HttpClient APIs()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44309/");
            return Client;
        }


    }
}
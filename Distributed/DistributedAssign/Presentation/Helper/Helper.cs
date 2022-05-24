using System;
using System.Net.Http;

namespace Presentation.Helper
{
    public class MicroServices
    {
        public HttpClient TransferMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://trans-ws7floi5xa-uc.a.run.app");
            return Client;
        }

        public HttpClient RegisterMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://reg-ws7floi5xa-uc.a.run.app");
            return Client;
        }

        public HttpClient FundMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://funds-ws7floi5xa-uc.a.run.app");
            return Client;
        }

        public HttpClient AuditMicroService()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://audit-ws7floi5xa-uc.a.run.app");
            return Client;
        }

        public HttpClient APIs()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api-ws7floi5xa-uc.a.run.app");
            return Client;
        }

    }


}

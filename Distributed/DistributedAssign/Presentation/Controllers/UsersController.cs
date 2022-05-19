using Common;
using DataAccess.Interfaces;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Presentation.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Presentation.Controllers
{
    public class UsersController : Controller
    {

        MicroServices help = new MicroServices();

        public async Task<ActionResult> Index()
        {
            User user = new User();

            HttpClient client = help.RegisterMicroService();
            HttpResponseMessage res = await client.GetAsync("PUTMEthodhere?email=" + User.Claims.ElementAt(4).Value);

            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(senten);
            }
            return View(user);
        }

        public async Task<IActionResult> Register(IFormCollection fr)
        {
            User user = new User();
            HttpClient client = help.RegisterMicroService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("Register/Register?email=" + User.Claims.ElementAt(4).Value + "&name=" +fr["FirstName"] + "&surname=" + fr["LastName"] ,stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(senten);
            }
            return RedirectToAction("Index");
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Helper;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class APIController : Controller
    {
        MicroServices help = new MicroServices();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Curr> curr = new List<Curr>();
            HttpClient client = help.APIs();
            HttpResponseMessage res = await client.GetAsync("api/API/getsymbols");
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                curr = JsonConvert.DeserializeObject<List<Curr>>(senten);
            }
            return View(curr);
        }

        [HttpGet]
        public async Task<IActionResult> Calc()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calc(IFormCollection form)
        {
            string value = "";
            HttpClient client = help.APIs();
            HttpResponseMessage res = await client.GetAsync("api/API/currecncyconvert?to=" + form["to"]+ "&from="+form["from"] + "&amount="+form["amount"]);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                value = JsonConvert.DeserializeObject<string>(senten);
            }
            ViewBag.newCurr = value;
            return View();
        }


    }
}

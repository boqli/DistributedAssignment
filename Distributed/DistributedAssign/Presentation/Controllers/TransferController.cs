using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class TransferController : Controller
    {
        MicroServices help = new MicroServices();

        public async Task<ActionResult> Index()
        {
            Fund user = new Fund();

            HttpClient client = help.TransferMicroService();
            HttpResponseMessage res = await client.GetAsync("GetUserInformation?email=" + User.Claims.ElementAt(4).Value);

            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return View(user);
        }
        //https://localhost:44306/deposit?email=kyleisstar%40gmail.com&fundAcc=1&amount=100
        

        [HttpGet]
        public async Task<IActionResult> deposit()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> deposit(IFormCollection form)
        {
            Fund fund = new Fund();
            HttpClient client = help.TransferMicroService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("deposit?email=" + User.Claims.ElementAt(4).Value+ "&fundAcc="+ form["fundAcc"]+ "&amount="+form["amount"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                fund = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return RedirectToAction("getFunds", "Fund");
        }


        [HttpGet]
        public async Task<IActionResult> transferToOwnAccount()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> transferToOwnAccount(IFormCollection form)
        {
            Fund fund = new Fund();
            HttpClient client = help.TransferMicroService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("transferToOwnAccount?email=" + User.Claims.ElementAt(4).Value + "&fundAccSender=" + form["fundAcc"] + "&fundAccReciever="+form["reciever"] + "&money=" + form["amount"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                fund = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return RedirectToAction("getFunds", "Fund");
        }

        [HttpGet]
        public async Task<IActionResult> transferToOtherAccount()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> transferToOtherAccount(IFormCollection form)
        {
            Fund fund = new Fund();
            HttpClient client = help.TransferMicroService();
            var stringContent = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("transferToOtherAccount?email=" + User.Claims.ElementAt(4).Value + "&fundAccSender=" + form["fundAcc"] + "&fundAccReciever=" + form["reciever"] + "&money=" + form["amount"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                fund = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return RedirectToAction("getFunds", "Fund");
        }

    }
}

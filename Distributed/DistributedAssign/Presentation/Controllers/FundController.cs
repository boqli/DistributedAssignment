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
    public class FundController : Controller
    {
        MicroServices help = new MicroServices();

        public async Task<ActionResult> Index()
        {
            Fund user = new Fund();

            HttpClient client = help.FundMicroService();
            HttpResponseMessage res = await client.GetAsync("GetUserInformation?email=" + User.Claims.ElementAt(4).Value);

            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> createFund()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> createFund(IFormCollection form)
        {
            Fund fund = new Fund();
            HttpClient client = help.FundMicroService();//createFund?bankCode=BOV&BankName=BankOfValletta&Country=Malta&CountryCode=MT&currencyCode=EUR&openingBal=1000&payee=kyleisstar%40gmail.com
            var stringContent = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("createFund?bankCode=" + form["BankCode"] + "&BankName=" + form["BankName"] + "&Country=" + form["Country"] + "&CountryCode=" +form["CountryCode"] + "&currencyCode="+form["currencyCode"] + "&openingBal="+form["OpeningBalance"] + "&payee="+form["payee"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                fund = JsonConvert.DeserializeObject<Fund>(senten);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> getFunds()
        {
            List<Fund> fund = new List<Fund>();
            HttpClient client = help.FundMicroService();//https://localhost:44335/getFunds?email=kyleisstar%40gmail.com
            HttpResponseMessage res = await client.GetAsync("getFunds?email=" + User.Claims.ElementAt(4).Value);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                fund = JsonConvert.DeserializeObject<List<Fund>>(senten);
                
            }
            return View(fund);
        }

        [HttpGet]
        public async Task<ActionResult> disableFund()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> disableFund(IFormCollection form)
        {
            List<Fund> fund = new List<Fund>();
            HttpClient client = help.FundMicroService();//https://localhost:44335/deactivate?email=kyleisstar%40gmail.com&fundAcc=1
            var stringContent = new StringContent(JsonConvert.SerializeObject(fund), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("deactivate?email=" + User.Claims.ElementAt(4).Value+ "&fundAcc="+ form["fundAcc"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("getFunds");
        }

        [HttpGet]
        public async Task<ActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Search(IFormCollection form)
        {

            List<Fund> acc = new List<Fund>();
            HttpClient client = help.FundMicroService();
            HttpResponseMessage res = await client.GetAsync("Search?email=" + User.Claims.ElementAt(4).Value + "&accNo=" + form["Search"]);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                acc = JsonConvert.DeserializeObject<List<Fund>>(senten);
            }
            return View(acc);
        }



    }
}

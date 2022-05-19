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
    public class AuditController : Controller
    {
        MicroServices help = new MicroServices();

        public async Task<ActionResult> Index()
        {
            /*
            List<Audits> audit = new List<Audits>();

            HttpClient client = help.AuditMicroService();
            HttpResponseMessage res = await client.GetAsync("GetUserInformation?email=" + User.Claims.ElementAt(4).Value);

            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                audit = JsonConvert.DeserializeObject<List<Audits>>(senten);
            }
            return View(audit);

            */

            List<Audits> audit = new List<Audits>();
            HttpClient client = help.AuditMicroService();//
            HttpResponseMessage res = await client.GetAsync("getAudits?email=" + User.Claims.ElementAt(4).Value);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                audit = JsonConvert.DeserializeObject<List<Audits>>(senten);

            }
            return View(audit);
        }

        [HttpGet]
        public async Task<IActionResult> getAudits()
        {
            List<Audits> audit = new List<Audits>();
            HttpClient client = help.AuditMicroService();//
            HttpResponseMessage res = await client.GetAsync("getAudits?email=" + User.Claims.ElementAt(4).Value);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                audit = JsonConvert.DeserializeObject<List<Audits>>(senten);

            }
            return View(audit);
        }

        [HttpGet]
        public async Task<IActionResult> getByDateAudits()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> getByDateAudits(IFormCollection form)
        {
            List<Audits> audit = new List<Audits>();
            HttpClient client = help.AuditMicroService();

            var stringContent = new StringContent(JsonConvert.SerializeObject(audit), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("getByDateAudits?email=" + User.Claims.ElementAt(4).Value+"&dateFrom="+form["dateFrom"]+ "&dateTo=" + form["dateTo"], stringContent);
            if (res.IsSuccessStatusCode)
            {
                var senten = res.Content.ReadAsStringAsync().Result;
                audit = JsonConvert.DeserializeObject<List<Audits>>(senten);

            }
            return View("Index",audit);
        }




        /*
        [HttpGet("getByDateAudits")]
        public async Task<IActionResult> getByDateAudits(string email, string dateFrom, string dateTo)
        {
            List<Audits> audits = await fireStore.getByDateAudits(email, dateFrom, dateTo);
            return Ok(audits);
        }
        */


    }
}

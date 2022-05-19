using Common;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.Controllers
{
    public class AuditController : Controller
    {

        private IFireStoreDataAccess fireStore;

        public AuditController(IFireStoreDataAccess _fireStore)
        {
            fireStore = _fireStore;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("getAudits")]
        public async Task<IActionResult> getAudits(string email)
        {
            List<Audits> audits = await fireStore.getAudits(email);
            return Ok(audits);
        }


        [HttpGet("getByDateAudits")]
        public async Task<IActionResult> getByDateAudits(string email, string dateFrom, string dateTo)
        {
            List<Audits> audits = await fireStore.getByDateAudits(email, dateFrom,dateTo);
            return Ok(audits);
        }



    }
}

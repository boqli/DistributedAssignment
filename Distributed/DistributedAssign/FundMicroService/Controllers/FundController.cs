using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundMicroService.Controllers
{
    public class FundController : Controller
    {

        private IFireStoreDataAccess fireStore;

        public FundController(IFireStoreDataAccess _fireStore)
        {
            fireStore = _fireStore;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> deactivate(string email, int fundAcc)
        {
            fireStore.deactivate(email, fundAcc);
            return Ok();
        }
    }
}

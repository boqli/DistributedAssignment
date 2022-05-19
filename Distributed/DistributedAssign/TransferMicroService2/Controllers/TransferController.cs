using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferMicroService.Controllers
{
    public class TransferController : Controller
    {
        private IFireStoreDataAccess fireStore;

        public TransferController(IFireStoreDataAccess _fireStore)
        {
            fireStore = _fireStore;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("deposit")]
        public async Task<IActionResult> deposit(string email, int fundAcc, int amount)
        {
            fireStore.Deposit(email, fundAcc, amount);
            return Ok();
        }

        [HttpPost("transferToOwnAccount")]
        public async Task<IActionResult> transferToOwnAccount(string email, int fundAccSender, int fundAccReciever, double money)
        {
            fireStore.transferToOwnAccount(email, fundAccSender, fundAccReciever, money);
            return Ok();
        }

        [HttpPost("transferToOtherAccount")]
        public async Task<IActionResult> transferToOtherAccount(string email, int fundAccSender, int fundAccReciever, double money)
        {
            fireStore.transferToOtherAccount(email, fundAccSender, fundAccReciever, money);
            return Ok();
        }



    }
}

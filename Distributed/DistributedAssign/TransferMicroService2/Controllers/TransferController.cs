using APIs.API;
using Common;
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
        private readonly ExchangeAPI exchangeAPI = ExchangeAPI.exchangeAPIInstance;

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
            List<Fund> fundFrom = await fireStore.getSpecificFund(fundAccSender);
            List<Fund> fundTo = await fireStore.getSpecificFund(fundAccReciever);

            if(fundTo[0].currencyCode != fundFrom[0].currencyCode)
            {
                string queryString = "?to=" + fundTo[0].currencyCode + "&from=" + fundFrom[0].currencyCode + "&amount=" + money;
                double newMoney = exchangeAPI.convert(queryString);


                fireStore.transferToOwnAccount(email, fundAccSender, fundAccReciever, money, newMoney);
                return Ok();
            }
            else
            {
                fireStore.transferToOwnAccount(email, fundAccSender, fundAccReciever, money, money);
                return Ok();
            }
            
        }

        [HttpPost("transferToOtherAccount")]
        public async Task<IActionResult> transferToOtherAccount(string email, int fundAccSender, int fundAccReciever, double money)
        {
            List<Fund> fundFrom = await fireStore.getSpecificFund(fundAccSender);
            List<Fund> fundTo = await fireStore.getSpecificFund(fundAccReciever);
            if (fundTo[0].currencyCode != fundFrom[0].currencyCode)
            {
                string queryString = "?to=" + fundTo[0].currencyCode + "&from=" + fundFrom[0].currencyCode + "&amount=" + money;
                double newMoney = exchangeAPI.convert(queryString);
                fireStore.transferToOtherAccount(email, fundAccSender, fundAccReciever, money, newMoney);
                return Ok();
            }
            else
            {
                fireStore.transferToOtherAccount(email, fundAccSender, fundAccReciever, money, money);
                return Ok();
            }
        }



    }
}

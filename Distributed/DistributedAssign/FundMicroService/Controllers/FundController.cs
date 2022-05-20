﻿using Common;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        

        [HttpPost("createFund")]
        public async Task<IActionResult> createFund(string bankCode, string BankName, string Country, string CountryCode,string currencyCode, int openingBal, string payee)
        {
            Fund fund =  await fireStore.createFund( bankCode,  BankName,  Country,  CountryCode,currencyCode,  openingBal,  payee);
            return Ok(fund);
        }


        [HttpGet("getFunds")]
        public async Task<IActionResult> getFunds(string email)
        {
            List<Fund> funds = await fireStore.getAllFunds(email);
            return Ok(funds);

        }

        [HttpGet("getSpecificFund")]
        public async Task<IActionResult> getSpecificFund( int bankAccNo)
        {
            List<Fund> fund = await fireStore.getSpecificFund(bankAccNo);
            return Ok(fund);

        }

        [HttpPost("deactivate")]
        public async Task<IActionResult> deactivate(string email, int fundAcc)
        {
            fireStore.deactivate(email, fundAcc);
            return Ok();
        }

        [HttpGet("Search")]
        public async Task<ActionResult> Search(string email, string accNo)
        {
            List<Fund> account = await fireStore.Search(accNo);
            string json = JsonConvert.SerializeObject(accNo);
            return Ok(account);
        }


    }
}

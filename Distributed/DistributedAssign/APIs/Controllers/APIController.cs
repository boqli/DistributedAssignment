using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIs.Controllers;
using APIs.API;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ExchangeAPI exchange = ExchangeAPI.exchangeAPIInstance;
        [HttpGet("currecncyconvert")]
        public IActionResult convert(string to, string from, string amount)
        {
            string queryString = "?to=" + to +"&from="+from+"&amount="+amount;
            return Ok(exchange.convert(queryString));
        }

        [HttpGet("getsymbols")]
        public IActionResult convsymbolsert()
        {
            return Ok(exchange.getSymbols());
        }




    }
}

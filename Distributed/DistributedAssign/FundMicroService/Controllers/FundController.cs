using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundMicroService.Controllers
{
    public class FundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

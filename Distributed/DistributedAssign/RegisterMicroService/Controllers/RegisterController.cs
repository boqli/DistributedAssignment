using Common;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController: ControllerBase
    {
        private IFireStoreDataAccess fireStore;

        public RegisterController(IFireStoreDataAccess _fireStore)
        {
            fireStore = _fireStore;
        }

        /*
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var myUser = await fireStore.GetUser(User.Claims.ElementAt(4).Value);
            if (myUser == null)
            {
                myUser = new Common.User();
                myUser.Email = User.Claims.ElementAt(4).Value;
            }
            return View(myUser);
        }
        */

        public IActionResult Register(User user)
        {

            user.Email = User.Claims.ElementAt(4).Value;
            user.FirstName = User.Claims.ElementAt(2).Value;
            user.LastName = User.Claims.ElementAt(3).Value;

            fireStore.AddUser(user);
            return RedirectToAction("Index");
        }
    }
}

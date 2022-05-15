using Common;
using DataAccess.Interfaces;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class UsersController : Controller
    {
        private IFireStoreDataAccess fireStore;

        public UsersController(IFireStoreDataAccess _fireStore)
        {
            fireStore = _fireStore;
        }

        [Authorize]
        public async Task< IActionResult> Index()
        {
            var myUser = await fireStore.GetUser(User.Claims.ElementAt(4).Value);
            if(myUser == null)
            {
                myUser = new Common.User();
                myUser.Email = User.Claims.ElementAt(4).Value;
            }
            return View(myUser);
        }

        public IActionResult Register(User user)
        {
            
            user.Email = User.Claims.ElementAt(4).Value;
            user.FirstName = User.Claims.ElementAt(2).Value;
            user.LastName = User.Claims.ElementAt(3).Value;
            
            fireStore.AddUser(user);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
            List<User> users = await fireStore.GetAllUsers();
            return View(users);
        }





    }
}

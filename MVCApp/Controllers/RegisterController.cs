using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /User/

        private static Users _users = new Users();
        public ActionResult Index()
        {
            return View(_users._userList);
        }
        public ActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserAdd(UserModels userModel)
        {
            _users.CreateUser(userModel);
            return View();
        }
       
        
    }
}

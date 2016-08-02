using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
using System.Security.Cryptography;


namespace TrainMeNowMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            using(var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var user = new User();
                user.Id = 1;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                user.RoleId = 3;

                ctx.Users.Add(user);
                ctx.SaveChanges();
            }

            return RedirectToAction("Display");



            return View();
        }
        public ActionResult Login(UserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;
            List<User> listaUseri = UsersDAL.getUsers();

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
using System.Security.Cryptography;
using System.Diagnostics;

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
                try
                {

                    Random rnd = new Random();
                    var user = new User();
                    user.Id = rnd.Next(100000); ;
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Password = model.Password;
                    user.RoleId = 3;

                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.StackTrace);

                }
                
            }

            return RedirectToAction("Index","Home");
            
        }
        public ActionResult Login(UserViewModel model)
        {
            var username = model.Username;
            var password = model.Password;
            List<User> listaUseri = UsersDAL.getUsers();
            if (username != null)
            { 
                foreach (User u in listaUseri)
                {
                    if (u.Username.Equals(username) && u.Password.Equals(password))
                    {
                        Session["User"] = u;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Register", "Account");
                    }
                }
            }
           
            return View();
        }
        [HttpGet]
        public ActionResult EditAccount()
        {
            if (Session["User"] != null) {
                var model = new UserViewModel();
                var user = new User();
                user = Session["User"] as User;
                model.Password = user.Password;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                return View(model);
                    }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult EditAccount (UserViewModel model)
        {
            var user = Session["User"] as User;
            user.Email = model.Email;
            user.Password = model.Password;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            Session["User"] = user;
            return RedirectToAction("EditAccount");
        }
    }
}
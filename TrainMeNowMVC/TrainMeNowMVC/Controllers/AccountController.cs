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
            foreach(User u in listaUseri)
            {
                if(u.Username.Equals(username) && u.Password.Equals(password))
                {
                    Session["User"] = u;
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    RedirectToAction("Login");
                }
            }
           
            return View();
        }
    }
}
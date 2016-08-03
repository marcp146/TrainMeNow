using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;

namespace TrainMeNowMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
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

                    //using (MD5 md5 = MD5.Create())
                    //{
                    //    user.Password = md5.ComputeHash(Encoding.UTF8.GetBytes(model.Password)).ToString();
                    //}

                    
                    user.Password = model.Password;
                    user.RoleId = 3;

                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);

                }

            }

            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                        Session["User"] = u.Id;
                        Session["RoleId"] = u.RoleId;
                       

                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction("Register", "Account");
            }

            return View();
        }
        [HttpGet]
        public ActionResult EditAccount()
        {
            if (Session["User"] != null)
            {
                var model = new UserViewModel();
                var dal = new UsersDAL();
                var user = new User();
                user = dal.getUser((int)Session["User"]);
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
        public ActionResult EditAccount(UserViewModel model)
        {
            
            var userinfo = new User();
            var userdal = new UsersDAL();
            userinfo = userdal.getUser((int)Session["User"]);
            userinfo.Email = model.Email;
            userinfo.Password = model.Password;
            userinfo.FirstName = model.FirstName;
            userinfo.LastName = model.LastName;
            using (var dal = new Internship2016NetTrainMeNowEntities())
            {
                dal.Entry(userinfo).State = System.Data.Entity.EntityState.Modified;
                dal.SaveChanges();
            }
            return RedirectToAction("EditAccount");
        }
        public ActionResult LogOff(UserViewModel model)
        {
            Session["User"] = null;
            Session["RoleId"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult MyTrainings()
        {
            if (Session["User"] != null)
            {
                List<UserTrainingsViewModel> orderslist = new List<UserTrainingsViewModel>();
                using (var ctx = new Internship2016NetTrainMeNowEntities())
                {
                    var user = ctx.Users.Find((int)Session["User"]);
                    var myTrainingList = user.Orders;
                    foreach (Order ord in myTrainingList)
                    {
                        orderslist.Add(new UserTrainingsViewModel { Id = ord.ID, Name = ord.Training.Name, Trainer = ord.Training.User.FirstName + " " + ord.Training.User.LastName });
                    }
                }
                return View(orderslist);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
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
                    var user = new User();
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Password = CalculateMD5Hash(model.Password);
                    user.RoleId = 3;
                    
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
            Login(model);
            return RedirectToAction("Index", "Home");
        }


        private string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

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
            var password = CalculateMD5Hash(model.Password);

            List<User> listaUseri = UsersDAL.GetUsers();
            if (username != null)
            {
                // Raul: Am modificat dupa cum zicea in task-ul de code review (sa foloseasca linq expression)
                List<User> loggedUser = listaUseri.Where(u => u.Username == username && u.Password == password).ToList();
                if (loggedUser.Count() > 0)
                {
                    Session["User"] = loggedUser[0].Id;
                    Session["RoleId"] = loggedUser[0].RoleId;

                    return RedirectToAction("Index", "Home");
                }
                // Pana vede Dani modificarea las codul lui comentat
                //foreach (User u in listaUseri)
                //{
                //    if (u.Username.Equals(username) && u.Password.Equals(password))
                //    {
                //        Session["User"] = u.Id;
                //        Session["RoleId"] = u.RoleId;


                //        return RedirectToAction("Index", "Home");
                //    }
                //}
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
                user = dal.GetUser((int)Session["User"]);
                model.Password = user.Password;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.Phone = user.Phone;
                model.Adress = user.Address;
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
            userinfo = userdal.GetUser((int)Session["User"]);
            userinfo.Email = model.Email;
            userinfo.FirstName = model.FirstName;
            userinfo.LastName = model.LastName;
            userinfo.Address = model.Adress;
            userinfo.Phone = model.Phone;
            using (var dal = new Internship2016NetTrainMeNowEntities())
            {
                dal.Entry(userinfo).State = System.Data.Entity.EntityState.Modified;
                dal.SaveChanges();
            }
            return RedirectToAction("EditAccount");
        }
        
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(string oldPass,string newPass, string verifyPass)
        {
            User user;
            using(var ctx = new Internship2016NetTrainMeNowEntities())
            {
                user = ctx.Users.Find((int)Session["User"]);
            }
            if(user.Password == CalculateMD5Hash(oldPass) && newPass == verifyPass)
            {
                user.Password = CalculateMD5Hash(newPass);
                using (var ctx = new Internship2016NetTrainMeNowEntities())
                {
                    ctx.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }

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

        public string GenerateHash(string pass)
        {

            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pass), 0, Encoding.UTF8.GetByteCount(pass));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
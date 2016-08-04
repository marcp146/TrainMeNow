using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
using TrainMeNowMVC.CustomAuthorize;
using System;
using System.Web;
using System.Web.Security;

namespace TrainMeNowMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrainerList()
        {
            if (Session["RoleId"] != null && (int)Session["RoleId"] == 1)
            {
                var trainers = new List<UserViewModel>();
                foreach (var user in UsersDAL.getUsersByRole(2))
                {
                    trainers.Add(new UserViewModel
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username
                    });
                }
                return View(trainers);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }
        
        [CustomAuthorize.CustomAuthorize(1)]
        public ActionResult TrainingList()
        {
            var trainingList = new TrainingsDal().getAllTrainings().Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
            return View(trainingList);
        }
    }
}
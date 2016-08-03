using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
using TrainMeNowMVC.CustomAuthorize;
using System;

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

        public ActionResult ManageTrainers()
        {
            if (Session["User"] != null && (int)Session["RoleId"] == 1)
            {
                var users = new List<User>();
                var dal = new UsersDAL();
                var model = new List<UserViewModel>();

                var trainerId = 2;
                var userId = 3;

                users = UsersDAL.getUsersByRole(trainerId);
                users.AddRange(UsersDAL.getUsersByRole(userId));

                foreach (var u in users)
                {
                    model.Add(new UserViewModel
                    {
                        Username = u.Username,
                        Id = u.RoleId,
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult ManageTrainers(List<UserViewModel> model)
        {



            return View();
        }

        public ActionResult TrainingList()
        {
            var trainingList = new TrainingsDal().getAllTrainings().Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
            return View(trainingList);
        }
    }
}
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
        [CustomAuthorize.CustomAuthorize(1, 2, 3)]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize.CustomAuthorize(1)]
        public ActionResult TrainerList()
        {
            if (Session["RoleId"] != null && (int)Session["RoleId"] == 1)
            {
                var trainers = new List<UserViewModel>();
                foreach (var user in UsersDAL.GetUsersByRole(2))
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
        public ActionResult ManageTrainers()
        {
            if (Session["User"] != null && (int)Session["RoleId"] == 1)
            {
                var users = new List<User>();
                var dal = new UsersDAL();
                var model = new List<UserViewModel>();

                var trainerId = 2;
                var userId = 3;

                users = UsersDAL.GetUsersByRole(trainerId);
                users.AddRange(UsersDAL.GetUsersByRole(userId));

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
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [CustomAuthorize.CustomAuthorize(1)]
        public ActionResult ManageTrainers(List<UserViewModel> model)
        {

            return View(model);
        }

        [CustomAuthorize.CustomAuthorize(1)]
        public ActionResult TrainingList()
        {
            var trainingList = new TrainingsDal().GetAllTrainings().Select(x => new TrainingViewModel
            {
                Id = x.Id,
                Name = x.Name,
                TrainerId = x.TrainerId,
                Price = x.Price,
                MaxUsers = x.MaxUsers,
                TrainerName = (new UsersDAL().GetUser((int)x.TrainerId).FirstName + " " + new UsersDAL().GetUser((int)x.TrainerId).LastName)
            }).ToList();
            return View(trainingList);
        }



        [CustomAuthorize.CustomAuthorize(1)]
        public ActionResult TraineesList()
        {
            const int adminRoleID = 1;
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                if (Session["RoleId"] != null && (int)Session["RoleId"] == adminRoleID)
                {
                    List<OrderViewModel> ordersList = new List<OrderViewModel>();
                    var ordersDal = new OrdersDAL();
                    foreach (var order in ordersDal.GetAll())
                    {
                        ordersList.Add(new OrderViewModel
                        {
                            Id = order.ID,
                            UserId = order.UserID,
                            TrainingId = order.TrainingID,
                            PaymentId = order.PaymentID
                        });
                    }

                    List<UserViewModel> usersList = new List<UserViewModel>();
                    var usersDal = new UsersDAL();
                    foreach (var order in ordersList)
                    {
                        var user0 = usersDal.GetUser(order.UserId);
                        UserViewModel user = new UserViewModel
                            {
                                Id = user0.Id,
                                Username = user0.Username,
                                FirstName = user0.FirstName,
                                LastName = user0.LastName,
                                Email = user0.Email,
                                Password = user0.Password,
                            };
                        if (usersList.Find(u => u.Id == user.Id) == null)
                        {
                            usersList.Add(user);
                        }
                    }
                    return View(usersList);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
        }
    }
}
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
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult ManageTrainers(List<UserViewModel> model)
        {



            return View(model);
        }

        public ActionResult TrainingList()
        {
            var trainingList = new TrainingsDal().getAllTrainings().Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
            return View(trainingList);
        }




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

                    List<UserViewModel> usersListWithDuplicates = new List<UserViewModel>();
                    var usersDal = new UsersDAL();
                    foreach (var order in ordersList)
                    {
                        foreach (var user in usersDal.getUsersById(order.UserId))
                        {
                            usersListWithDuplicates.Add(new UserViewModel
                            {
                                Id = user.Id,
                                Username = user.Username,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                                Password = user.Password,
                            });
                        }
                    }

                    List<UserViewModel> usersList = new List<UserViewModel>();
                    foreach (var user in usersListWithDuplicates)
                    {
                        usersList.Add(user);
                    }

                    foreach (var user in usersListWithDuplicates)
                    {
                        int appearances = 0;
                        foreach (var user2 in usersListWithDuplicates)
                        {
                            bool appearancesIncremented = false;
                            if (user.Id == user2.Id)
                            {
                                appearances++;
                                appearancesIncremented = true;
                            }
                            if (appearances > 1 && appearancesIncremented == true )
                            {
                                usersList.Remove(user2);
                            }
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
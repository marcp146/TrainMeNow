using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TrainMeNowDAL;
using TrainMeNowMVC.Models;
using Microsoft.Ajax.Utilities;

namespace TrainMeNowMVC.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListTrainees()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                if (Session["RoleId"] != null && (int)Session["RoleId"] == 2)
                {
                    int currentTrainerID = (int)Session["User"];
                    var trainingsList = new List<TrainingViewModel>();
                    var trainingsDal = new TrainingsDal();
                    foreach (var training in trainingsDal.getTrainingsByTrainerId(currentTrainerID))
                    {
                        trainingsList.Add(new TrainingViewModel
                        {
                            Id = training.Id,
                            Name = training.Name,
                            TrainerId = training.TrainerId,
                            Price = training.Price,
                            MaxUsers = training.MaxUsers
                        });
                    }


                    List<OrderViewModel> ordersList = new List<OrderViewModel>();
                    var ordersDal = new OrdersDAL();
                    foreach (var tr in trainingsList)
                    {
                        foreach (var order in ordersDal.GetOrdersByTrainingID(tr.Id))
                        {
                            ordersList.Add(new OrderViewModel
                            {
                                Id = order.ID,
                                UserId = order.UserID,
                                TrainingId = order.TrainingID,
                                PaymentId = order.PaymentID
                            });
                        }
                    }

                    List<UserViewModel> usersList = new List<UserViewModel>();
                    var usersDal = new UsersDAL();
                    foreach (var or in ordersList)
                    {
                        foreach (var user in usersDal.getUsersById(or.UserId))
                        {
                            usersList.Add(new UserViewModel
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
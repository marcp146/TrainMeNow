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
        [CustomAuthorize.CustomAuthorize(1,2)]
        public ActionResult ListTrainees(int id)
        {
            const int adminRoleID = 1;
            const int trainerRoleID = 2;
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                if (Session["RoleId"] != null && ((int)Session["RoleId"] == trainerRoleID || (int)Session["RoleId"] == adminRoleID))
                {
                    int currentTrainingID = id;
                    List<OrderViewModel> ordersList = new List<OrderViewModel>();
                    var ordersDal = new OrdersDAL();
                    foreach (var order in ordersDal.GetOrdersByTrainingID(currentTrainingID))
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
                    foreach (var or in ordersList)
                    {
                        var user = usersDal.GetUser(or.UserId);
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
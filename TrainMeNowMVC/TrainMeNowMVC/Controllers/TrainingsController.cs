using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowDAL;
using TrainMeNowMVC.Models;

namespace TrainMeNowMVC.Controllers
{
    public class TrainingsController : Controller
    {
        // GET: Trainings
        public ActionResult TrainingsListByTrainerId(int? id)
        {
            var trainingsList = new TrainingsDal().getTrainings(id).Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
            return View(trainingsList);
        }

        public ActionResult MyTrainings()
        {
            if (Session["User"] != null)
            {
                List<OrdersViewModel> orderslist = new List<OrdersViewModel>();
                using (var ctx = new Internship2016NetTrainMeNowEntities())
                {
                    var user = ctx.Users.Find((int)Session["User"]);
                    var myTrainingList = user.Orders;
                    foreach (Order ord in myTrainingList)
                    {
                        orderslist.Add(new OrdersViewModel { Id = ord.ID, Name = ord.Training.Name, Trainer = ord.Training.User.FirstName + " " + ord.Training.User.LastName });
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
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
            return View();
        }

        [HttpPost]
        public JsonResult GetListTraineesData()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                int idDeProba = 1;
                //var trainingsList = ctx.Trainings.Where(t => t.TrainerId == idDeProba).ToList();
                var trainingsList = new TrainingsDal().getTrainings(idDeProba).Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();

                List<OrderViewModel> ordersList = null;
                foreach (var tr in trainingsList)
                {
                    //var ordersList2 = ctx.Orders.Where(o => o.TrainingID == tr.Id).ToList();
                    var ordersList2 = new OrdersDAL().GetOrdersByID(tr.Id).Select(x => new OrderViewModel { Id = x.ID, UserId = x.UserID, TrainingId = x.TrainingID, PaymentId = x.PaymentID }).ToList();
                    ordersList.AddRange(ordersList2);
                }

                List<UserViewModel> usersList = null;
                foreach (var or in ordersList)
                {
                    //var usersList2 = ctx.Users.Where(u => u.Id == or.UserID).ToList();
                    var usersList2 = new UsersDAL().getUsersById(or.Id).Select(x => new UserViewModel { Id = x.Id, Username = x.Username, FirstName = x.FirstName, LastName = x.LastName, Email = x.Email, Password = x.Password }).ToList();
                    usersList.AddRange(usersList2);
                }

                return Json(usersList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
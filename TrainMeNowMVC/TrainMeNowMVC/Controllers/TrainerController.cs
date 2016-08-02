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
                var trainingsList = ctx.Trainings.Where(t => t.TrainerId == idDeProba).ToList();

                List<Order> ordersList = null;
                foreach (var tr in trainingsList)
                {
                    var ordersList2 = ctx.Orders.Where(o => o.TrainingID == tr.Id).ToList();
                    ordersList.AddRange(ordersList2);
                }

                List<User> usersList = null;
                foreach (var or in ordersList)
                {
                    var users2List = ctx.Users.Where(u => u.Id == or.UserID).ToList();
                    usersList.AddRange(users2List);
                }

                return Json(usersList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
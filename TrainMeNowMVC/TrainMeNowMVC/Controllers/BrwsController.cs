using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowDAL;
using TrainMeNowMVC.Models;

namespace TrainMeNowMVC.Controllers
{
    public class BrwsController: Controller
    { 
        public ActionResult Display()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainings = ctx.Trainings.Select(x => new TrainingViewModel { Id=x.Id,Name = x.Name,TrainerId= x.TrainerId, Price= x.Price,MaxUsers= x.MaxUsers }).ToList();
                return View(trainings);
            }

        }
    }
}
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

        public ActionResult Display()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainings = ctx.Trainings.Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
                return View(trainings);
            }

        }

        public ActionResult BrowseByName(string name)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {

                var user = ctx.Users.Where(x => x.LastName == name).FirstOrDefault();
                int id = user.Id;
                var trainings = ctx.Trainings.Where(x => x.TrainerId == id).ToList();

                return View(trainings);

            }
        }
    }
}
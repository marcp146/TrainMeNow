
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
        public ActionResult TrainingsListByTrainerId()
        {
            if (Session["User"] != null)
            {
                int nr = (int)Session["User"];
                var trainingsList = new TrainingsDal().getTrainingsByTrainerId(nr).Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
                return View(trainingsList);
            }else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult CreateTraining()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult CreateTrainingData(TrainingViewModel model)
        {

            if (Session["User"] != null)
            {
                TrainingsDal trn = new TrainingsDal();
                model.TrainerId = (int)Session["User"];
                trn.Create(model.Name, model.TrainerId, model.Price, model.MaxUsers);

                return RedirectToAction("TrainingsListByTrainerId");
            }else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Display()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainings = ctx.Trainings.Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
                return View(trainings);
            }

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainings = ctx.Trainings.Where(x => x.Id==id).ToList();
                var trainingList = trainings.Select(x => new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();
                return View(trainingList);
            }
        }

        [HttpGet] 
        public ActionResult Buy(int id)
        {
            if (Session["User"] != null)
            {

                List<UserTrainingsViewModel> orderslist = new List<UserTrainingsViewModel>(); 
                using(var ctx= new Internship2016NetTrainMeNowEntities())
                {
                    var training = ctx.Trainings.Where(x => x.Id == id).FirstOrDefault();
                    int? maxUsers = training.MaxUsers;

                    var user = ctx.Users.Find((int)Session["User"]);
                    var myTrainingList = user.Orders;
                    if (maxUsers > 0)
                    {
                        maxUsers = maxUsers - 1; 

                        var order =;
                        myTrainingList.Add(order);
                    }

                }

            }

                return View();
        }

        public ActionResult BrowseByName(string id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {

                var user = ctx.Users.Where(x => x.LastName == id).FirstOrDefault();
                int ident = user.Id;
                var trainings = ctx.Trainings.Where(x => x.TrainerId == ident).ToList(); 
                var trainingList= trainings.Select(x=> new TrainingViewModel { Id = x.Id, Name = x.Name, TrainerId = x.TrainerId, Price = x.Price, MaxUsers = x.MaxUsers }).ToList();

                return View(trainingList);

            }
        }

            public ActionResult EnrolledTrainings()
        {
            if (Session["User"] != null)
            {
                List<UserTrainingsViewModel> orderslist = new List<UserTrainingsViewModel>();
                using (var ctx = new Internship2016NetTrainMeNowEntities())
                {
                    var user = ctx.Users.Find((int)Session["User"]);
                    var myTrainingList = user.Orders;
                    foreach (Order ord in myTrainingList)
                    {
                        orderslist.Add(new UserTrainingsViewModel { Id = ord.ID, Name = ord.Training.Name, Trainer = ord.Training.User.FirstName + " " + ord.Training.User.LastName,Email=ord.Training.User.Email });
                    }
                }
                return View(orderslist);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public ActionResult EditTraining(int id)
        {
            if (Session["User"] != null)
            {
                var rez = new TrainingsDal().GetTrainingById(id);
                if (rez != null)
                {

                    var rezmodel = new TrainingViewModel();
                    rezmodel.Id = rez.Id;
                    rezmodel.Name = rez.Name;
                    rezmodel.TrainerId = rez.TrainerId;
                    rezmodel.Price = rez.Price;
                    rezmodel.MaxUsers = rez.MaxUsers;
                    return View(rezmodel);
                }
                return RedirectToAction("TrainingsListByTrainerId");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public ActionResult EditTrainingData(TrainingViewModel p)
        {
            if ((new TrainingsDal().EditTraining(p.Id, p.Price, p.MaxUsers)) == true)
            {
                return RedirectToAction("TrainingsListByTrainerId");
            }
            else
            {
                return RedirectToAction("TrainingsListByTrainerId");
            }
        }
    }


}
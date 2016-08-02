using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowDAL;

namespace TrainMeNowMVC.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult Display(string criteria)
        { 
            using(var ctx=new Internship2016NetTrainMeNowEntities())
            {
                var trainings = ctx.Trainings.Select(x =>new );
            }
            return View(trainings);
        }
    }
}
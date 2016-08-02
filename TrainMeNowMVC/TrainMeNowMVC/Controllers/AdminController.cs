using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


            return View();
        }
        public ActionResult TrainingList()
        {
            return View();
        }
    }
}
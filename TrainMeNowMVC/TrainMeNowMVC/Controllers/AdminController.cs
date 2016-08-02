﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainMeNowMVC.Models;
using TrainMeNowDAL;
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
            var trainers = new List<UserViewModel>();
            foreach (var user in UsersDAL.getUsersByRole(2))
            {
                trainers.Add(new UserViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                });
            }
            return View(trainers);
        }
    }
}
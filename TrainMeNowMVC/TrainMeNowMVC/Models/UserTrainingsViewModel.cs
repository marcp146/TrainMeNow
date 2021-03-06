﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainMeNowMVC.Models
{
    public class UserTrainingsViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Trainer { get; set; }
        public virtual string Description { get; set; }
        public virtual string Email { get; set; }
    }
}
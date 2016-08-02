﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainMeNowMVC.Models
{
    public class TrainingViewModel
    { 
        public int Id{get; set;} 
        public string Name { get; set; } 
        public int? TrainerId { get; set; } 
        public decimal? Price { get; set; } 
        public int? MaxUsers { get; set; }
    }
}
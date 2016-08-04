using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrainMeNowMVC.Models
{
    public class TrainingViewModel
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } 
        public int TrainerId { get; set; } 
        public decimal Price { get; set; } 
        public int MaxUsers { get; set; }
        public string TrainerName { get; set; }
        public int EnrolledUsers { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int NumberOfRationgs { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainMeNowMVC.Models
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingId { get; set; }
        public int PaymentId { get; set; }
    }
}
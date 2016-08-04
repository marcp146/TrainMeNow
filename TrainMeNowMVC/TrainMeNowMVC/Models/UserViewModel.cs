using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrainMeNowMVC.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // public int RoleId { get; set; }
        public bool IsTrainer
        {
            get
            {
                return IsTrainer;
            }
            set
            {
                if (Id == 2)
                {
                    IsTrainer = true;
                }
                else
                {
                    IsTrainer = false;
                }
            }
        }
    }
}
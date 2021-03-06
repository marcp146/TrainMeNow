﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    public class UsersDAL
    {
        public User GetUser(int id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var user = ctx.Users.Find(id);
                return user;
            }
        }


        public static List<User> GetUsers()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var users = ctx.Users.ToList();
                return users;
            }
        }

        public static List<User> GetUsersByRole(int roleId)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var users = ctx.Users.Where(m => m.RoleId == roleId).ToList();
                return users;
            }
        }  

        public User GetUserByName(string name)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var user = ctx.Users.Where(m => m.LastName == name).FirstOrDefault();
                return user;
            }
        }

        public void SaveUser(User u)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                ctx.Users.Add(u);
                ctx.SaveChanges();

            }
        }
    }

}

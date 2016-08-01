using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    public class UsersDAL
    {
        public User getUser(int id)
        {
            using(var ctx=new Internship2016NetTrainMeNowEntities())
            {
                var user = ctx.Users.Find(id);
                return user;
            }
        }

        public List<User> getUsers()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var users = ctx.Users.ToList();
                return users;
            }
        }
    }

}

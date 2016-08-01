using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL.DAL_classes
{
    class TrainingsDAL
    {
        public Training GetByID(int id)
        {
            using(var ctx = new Internship2016NetTrainMeNowEntities())
            {
                return ctx.Trainings.Find(id);
            }
        }

        public void Delete(Training training)
        {
            using(var ctx = new Internship2016NetTrainMeNowEntities())
            {
                ctx.Trainings.Remove(training);
            }
        }

        public IEnumerable<Training> GetAllTrainings()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                return ctx.Trainings.ToList();
            }
        }
    }
}

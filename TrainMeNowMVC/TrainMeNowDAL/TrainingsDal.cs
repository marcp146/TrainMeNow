using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    public class TrainingsDal
    {
        public List<Training> getTrainingsByTrainerId(int? id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainingsList = ctx.Trainings.Where(x => x.TrainerId == id).ToList();
                return trainingsList;
            }
        }

        public bool EditTraining(int id, decimal? price, int? maxusers)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trn = ctx.Trainings.Find(id);
                if (trn != null)
                {
                    trn.Price = price;
                    trn.MaxUsers = maxusers;
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public List<Training> getAllTrainings()
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainingsList = ctx.Trainings.ToList();
                return trainingsList;
            }
        }

        public Training GetTrainingById(int? id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trn = ctx.Trainings.Find(id);
                return trn;
            }
        }
    }
}

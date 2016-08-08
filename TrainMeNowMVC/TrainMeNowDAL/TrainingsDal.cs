using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    public class TrainingsDal
    {
        public List<Training> GetTrainingsByTrainerId(int? id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trainingsList = ctx.Trainings.Where(x => x.TrainerId == id).ToList();
                return trainingsList;
            }
        }

        public bool EditTraining(int id, decimal price, int maxusers, string _Description, string _Language)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var trn = ctx.Trainings.Find(id);
                if (trn != null)
                {
                    trn.Price = price;
                    trn.MaxUsers = maxusers;
                    trn.Description = _Description;
                    trn.Language = _Language;
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void Create(Training training)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                ctx.Trainings.Add(new Training
                {
                    Name = training.Name,
                    TrainerId = training.TrainerId,
                    Price = training.Price,
                    MaxUsers = training.MaxUsers,
                    Description = training.Description,
                    NumberOfRationgs = 0,
                    Language = training.Language,
                    EnrolledUsers = 0,
                    Rating = 0
                });
                ctx.SaveChanges();
            }
        }

        public List<Training> GetAllTrainings()
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

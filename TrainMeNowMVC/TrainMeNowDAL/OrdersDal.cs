using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    class OrdersDal
    {
        protected Internship2016NetTrainMeNowEntities Context { get; set; }

        public OrdersDal()
        {
            Context = new Internship2016NetTrainMeNowEntities();
        }

        public OrdersDal(Internship2016NetTrainMeNowEntities context)
        {
            Context = context;
        }

        public void Insert(Order entity)
        {
            Context.Orders.Add(entity);
        }

        public List<Order> GetAll()
        {
            return Context.Orders.ToList();
        }

        public void Update(int id, Order entity)
        {
            Context.Orders.Find(id).ID = entity.ID;
            Context.Orders.Find(id).Payment = entity.Payment;
            Context.Orders.Find(id).PaymentID = entity.PaymentID;
            Context.Orders.Find(id).Training = entity.Training;
            Context.Orders.Find(id).TrainingID = entity.TrainingID;
            Context.Orders.Find(id).User = entity.User;
            Context.Orders.Find(id).UserID = entity.UserID;
        }

        public void Delete(int Id)
        {
            Context.Orders.Remove(GetByID(Id));
        }

        public Order GetByID(int id)
        {
            return Context.Orders.Find(id);
        }
    }
}

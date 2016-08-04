using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMeNowDAL
{
    public class OrdersDAL
    {
        protected Internship2016NetTrainMeNowEntities Context { get; set; }

        public OrdersDAL()
        {
            Context = new Internship2016NetTrainMeNowEntities();
        }

        public OrdersDAL(Internship2016NetTrainMeNowEntities context)
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
            Context.Orders.Remove(GetOrderByID(Id));
        }

        public Order GetOrderByID(int id)
        {
            return Context.Orders.Find(id);
        }

        public List<Order> GetOrdersByID(int id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var ordersList = ctx.Orders.Where(x => x.ID == id).ToList();
                return ordersList;
            }
        }

        public List<Order> GetOrdersByTrainingID(int id)
        {
            using (var ctx = new Internship2016NetTrainMeNowEntities())
            {
                var ordersList = ctx.Orders.Where(x => x.TrainingID == id).ToList();
                return ordersList;
            }
        }
    }
}

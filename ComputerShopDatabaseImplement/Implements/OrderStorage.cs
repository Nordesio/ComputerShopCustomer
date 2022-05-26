using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopContracts.BindingModel;
using ComputerShopDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
namespace ComputerShopDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new ComputerShopDatabase())
            {
                return context.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    OrderName = rec.OrderName,
                    Price = rec.Price,
                    CustomerLogin = context.Customers.FirstOrDefault(x => x.CustomerLogin == rec.CustomerLogin).CustomerLogin,
                }).ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ComputerShopDatabase())
            {
                return context.Orders
                .Where(rec => rec.CustomerLogin == model.CustomerLogin)
                 .Select(rec => new OrderViewModel
                 {
                     Id = rec.Id,
                     OrderName = rec.OrderName,
                     Price = rec.Price,
                     CustomerLogin = context.Customers.FirstOrDefault(x => x.CustomerLogin == rec.CustomerLogin).CustomerLogin,
                 }).ToList();
            }
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ComputerShopDatabase())
            {
                var subject = context.Orders
                .FirstOrDefault(rec => rec.OrderName == model.OrderName || rec.Id == model.Id);
                return subject != null ?
               new OrderViewModel
               {
                   Id = subject.Id,
                   OrderName = subject.OrderName,
                   Price = subject.Price,
                   CustomerLogin = context.Customers.FirstOrDefault(x => x.CustomerLogin == subject.CustomerLogin).CustomerLogin,
               } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (var context = new ComputerShopDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Order CreateModel(OrderBindingModel model, Order subject)
        {
            subject.OrderName = model.OrderName;
            subject.CustomerLogin = model.CustomerLogin;
            subject.Price = model.Price;
            return subject;
        }
    }
}

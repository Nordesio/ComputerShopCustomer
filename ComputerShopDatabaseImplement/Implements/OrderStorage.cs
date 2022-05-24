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
            using var context = new ComputerShopDatabase();
            return context.Orders
            .Include(rec => rec.Customer)
             .Include(rec => rec.AssemblyOrders)
                .ThenInclude(rec => rec.Assembly)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Orders
           .Include(rec => rec.Customer)
             .Include(rec => rec.AssemblyOrders)
                .ThenInclude(rec => rec.Assembly)
            .Where(rec => rec.OrderName.Contains(model.OrderName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var order = context.Orders
            .FirstOrDefault(rec => rec.OrderName == model.OrderName || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Order order = new Order()
                {
                    OrderName = model.OrderName,
                    Price = model.Price
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
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
                CreateModel(model, element, context);
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
        private static Order CreateModel(OrderBindingModel model, Order order, ComputerShopDatabase context)
        {
            order.OrderName = model.OrderName;
            order.Price = model.Price;
            order.DateCreate = model.DateCreate;
            order.DateReceipt = model.DateReceipt;
            order.CustomerLogin = model.CustomerLogin;
            if (order.Id == 0)
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }
            if (model.Id.HasValue)
            {
                var assemblyOrders = context.AssemblyOrders.Where(rec => rec.OrderId == model.Id.Value).ToList();

                context.AssemblyOrders.RemoveRange(assemblyOrders.Where(rec => !model.AssemblyOrders.ContainsKey(rec.OrderId)).ToList());
                context.SaveChanges();
            }

            foreach (var fc in model.AssemblyOrders)
            {
                context.AssemblyOrders.Add(new AssemblyOrder
                {
                    OrderId = order.Id,
                    AssemblyId = fc.Key
                });
                context.SaveChanges();
            }
            return order;
        }
        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Price = order.Price,
                CustomerLogin = order.CustomerLogin,
                OrderName = order.OrderName,
                DateCreate = order.DateCreate,
                DateReceipt = order.DateReceipt,
                AssemblyOrders = order.AssemblyOrders.ToDictionary(recPP => recPP.OrderId, recPP => (recPP.AssemblyId))
            };
        }
    }
}

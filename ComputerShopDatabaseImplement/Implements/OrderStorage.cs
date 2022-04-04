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
            .Include(rec => rec.OrderCustomers)
            .ThenInclude(rec => rec.Order)
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
            .Include(rec => rec.OrderCustomers)
            .ThenInclude(rec => rec.Order)
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
            .Include(rec => rec.OrderCustomers)
            .ThenInclude(rec => rec.Order)
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
            if (model.Id.HasValue)
            {
                var orderComponents = context.OrderCustomers.Where(rec => rec.OrderId == model.Id.Value).ToList();

                context.OrderCustomers.RemoveRange(orderComponents.Where(rec => !model.OrderCustomers.ContainsKey(rec.OrderId)).ToList());
                context.SaveChanges();

                foreach (var updateComponent in orderComponents)
                {
                    updateComponent.Count = model.OrderCustomers[updateComponent.OrderId].Item2;
                    model.OrderCustomers.Remove(updateComponent.OrderId);
                }
                context.SaveChanges();
            }

            foreach (var fc in model.OrderCustomers)
            {
                context.OrderCustomers.Add(new OrderCustomer
                {
                    OrderId = order.Id,
                    CustomerId = fc.Key,
                    Count = fc.Value.Item2
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
                OrderName = order.OrderName,
                DateCreate = order.DateCreate,
                DateReceipt = order.DateReceipt
            };
        }
    }
}

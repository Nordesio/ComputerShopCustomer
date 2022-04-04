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
            .Where(rec => rec.OrderName.Contains(model.OrderName))
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
            .FirstOrDefault(rec => rec.OrderName == model.OrderName || rec.Id
           == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
           model.Id);
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
        private static Order CreateModel(OrderBindingModel model, Order
       order)
        {
            order.OrderName = model.OrderName;
            order.Price = model.Price;
            order.DateCreate = model.DateCreate;
            order.DateReceipt = model.DateReceipt;

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

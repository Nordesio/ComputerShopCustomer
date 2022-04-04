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
    public class DeliveryStorage : IDeliveryStorage
    {
        public List<DeliveryViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Deliveries
            .Include(rec => rec.DeliveryComponents)
            .ThenInclude(rec => rec.Component)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<DeliveryViewModel> GetFilteredList(DeliveryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Deliveries
            .Include(rec => rec.DeliveryComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.DeliveryName.Contains(model.DeliveryName))
            .Select(CreateModel)
            .ToList();
        }
        public DeliveryViewModel GetElement(DeliveryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var delivery = context.Deliveries
            .FirstOrDefault(rec => rec.DeliveryName == model.DeliveryName || rec.Id
           == model.Id);
            return delivery != null ? CreateModel(delivery) : null;
        }
        public void Insert(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            context.Deliveries.Add(CreateModel(model, new Delivery()));
            context.SaveChanges();
           
        }
        public void Update(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            var element = context.Deliveries.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Delivery element = context.Deliveries.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Deliveries.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Delivery CreateModel(DeliveryBindingModel model, Delivery
       delivery)
        {
            delivery.DeliveryName = model.DeliveryName;
            delivery.DateCreate = model.DateCreate;
            
            return delivery;
        }
        private static DeliveryViewModel CreateModel(Delivery delivery)
        {
            return new DeliveryViewModel
            {
                Id = delivery.Id,
                DeliveryName = delivery.DeliveryName,
                DateCreate = delivery.DateCreate
            };
        }
    }
}

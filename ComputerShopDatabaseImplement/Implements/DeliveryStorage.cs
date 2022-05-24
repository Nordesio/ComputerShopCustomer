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
            .ToList()
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
            .Include(rec => rec.DeliveryComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.DeliveryName == model.DeliveryName || rec.Id == model.Id);
            return delivery != null ? CreateModel(delivery) : null;
        }
        public void Insert(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Delivery delivery = new Delivery()
                {
                    DeliveryName = model.DeliveryName,
                    
                };
                context.Deliveries.Add(delivery);
                context.SaveChanges();
                CreateModel(model, delivery, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Deliveries.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(DeliveryBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Delivery element = context.Deliveries.FirstOrDefault(rec => rec.Id == model.Id);
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
        private static Delivery CreateModel(DeliveryBindingModel model, Delivery delivery, ComputerShopDatabase context)
        {
            delivery.DeliveryName = model.DeliveryName;
           
            delivery.DateCreate = model.DateCreate;
           
            if (model.Id.HasValue)
            {
                var deliveryComponents = context.DeliveryComponents.Where(rec => rec.DeliveryId == model.Id.Value).ToList();

                context.DeliveryComponents.RemoveRange(deliveryComponents.Where(rec => !model.DeliveryComponents.ContainsKey(rec.DeliveryId)).ToList());
                context.SaveChanges();

                foreach (var updateComponent in deliveryComponents)
                {
                    updateComponent.Count = model.DeliveryComponents[updateComponent.DeliveryId].Item2;
                    model.DeliveryComponents.Remove(updateComponent.DeliveryId);
                }
                context.SaveChanges();
            }

            foreach (var fc in model.DeliveryComponents)
            {
                context.DeliveryComponents.Add(new DeliveryComponent
                {
                    DeliveryId = delivery.Id,
                    ComponentId = fc.Key,
                    Count = fc.Value.Item2
                });
                context.SaveChanges();
            }
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

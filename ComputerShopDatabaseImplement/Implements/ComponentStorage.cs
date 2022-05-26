using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopContracts.BindingModel;
using ComputerShopDatabaseImplement.Models;

namespace ComputerShopDatabaseImplement.Implements
{
    public class ComponentStorage : IComponentStorage
    {
        public List<ComponentViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Components
                .Include(rec => rec.ComponentDeliveries)
                .ThenInclude(rec => rec.Delivery)
                .Select(CreateModel)
                .ToList();
        }
        public List<ComponentViewModel> GetFilteredList(ComponentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Components
            .Include(rec => rec.ComponentDeliveries)
            .ThenInclude(rec => rec.Delivery)
            .Where(rec => rec.ComponentName.Contains(model.ComponentName))
            .Select(CreateModel)
            .ToList();
        }
        public ComponentViewModel GetElement(ComponentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new ComputerShopDatabase();
            var comp = context.Components
                .Include(rec => rec.ComponentDeliveries)
                .ThenInclude(rec => rec.Delivery)
                .FirstOrDefault(rec => rec.ComponentName == model.ComponentName || rec.Id == model.Id);
            return comp != null ? CreateModel(comp) : null;
        }
        public void Insert(ComponentBindingModel model)
        {
            var context = new ComputerShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                Component comp = new Component
                {
                    ComponentName = model.ComponentName
                };
                context.Components.Add(comp);
                context.SaveChanges();
                CreateModel(model, comp, context);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }
        public void Update(ComponentBindingModel model)
        {
            var context = new ComputerShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(ComponentBindingModel model)
        {
            var context = new ComputerShopDatabase();
            Component element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Components.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Component CreateModel(ComponentBindingModel model, Component comp, ComputerShopDatabase context)
        {
            
            comp.ComponentName = model.ComponentName;

            if (model.Id.HasValue)
            {
                var compDisciplines = context.DeliveryComponents.Where(rec => rec.Id == model.Id).ToList();
                context.DeliveryComponents.RemoveRange(compDisciplines);
                context.SaveChanges();
            }
            return comp;
        }
        public void BindingDelivery(int compId, int deliveryId)
        {
            var context = new ComputerShopDatabase();
            context.DeliveryComponents.Add(new DeliveryComponent
            {
                ComponentId = compId,
                DeliveryId = deliveryId,
            });
            context.SaveChanges();

        }
        private static ComponentViewModel CreateModel(Component component)
        {
            return new ComponentViewModel
            {
                Id = component.Id,
                ComponentName = component.ComponentName,
                Deliveries = component.ComponentDeliveries
                .ToDictionary(recSS => recSS.DeliveryId, recSS => recSS.Delivery.DeliveryName),
            };
        }
    }
}

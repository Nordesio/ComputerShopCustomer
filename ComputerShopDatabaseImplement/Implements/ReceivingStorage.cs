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
    public class ReceivingStorage : IReceivingStorage
    {
        public List<ReceivingViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Receivings
             .Select(rec => new ReceivingViewModel
             {
                 Id = rec.Id,
                 Price = rec.Price,
                 DeliveryName = context.Deliveries.FirstOrDefault(recOrder => rec.DeliveryId == recOrder.Id).DeliveryName,
                 DeliveryId =rec.DeliveryId,
                 DateDispatch = rec.DateDispatch,
             }).ToList();
        }
        public List<ReceivingViewModel> GetFilteredList(ReceivingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Receivings
              .Select(rec => new ReceivingViewModel
              {
                  Id = rec.Id,
                  Price = rec.Price,
                  DeliveryName = context.Deliveries.FirstOrDefault(recOrder => rec.DeliveryId == recOrder.Id).DeliveryName,
                  DeliveryId = rec.DeliveryId,
                  DateDispatch = rec.DateDispatch,
              }).ToList();
        }
        public ReceivingViewModel GetElement(ReceivingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var receiving = context.Receivings
            .FirstOrDefault(rec => rec.Id == model.Id);
            return receiving != null ?
               new ReceivingViewModel
               {
                   Id = receiving.Id,
                   Price = receiving.Price,
                   DeliveryName = context.Deliveries.FirstOrDefault(recOrder => receiving.DeliveryId == recOrder.Id).DeliveryName,
                   DeliveryId = receiving.DeliveryId,
                   DateDispatch = receiving.DateDispatch
               } :
                null;
        }
        public void Insert(ReceivingBindingModel model)
        {
            using (var context = new ComputerShopDatabase())
            {
                context.Receivings.Add(CreateModel(model, new Receiving()));
                context.SaveChanges();
            }
        }
        public void Update(ReceivingBindingModel model)
        {
            using (var context = new ComputerShopDatabase())
            {
                var element = context.Receivings.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(ReceivingBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Receiving element = context.Receivings.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Receivings.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Receiving CreateModel(ReceivingBindingModel model, Receiving receiving)
        {
            receiving.Price = model.Price;
            receiving.DateDispatch = DateTime.Now;
            receiving.DeliveryId = model.DeliveryId;
            return receiving;
        }
    }
}

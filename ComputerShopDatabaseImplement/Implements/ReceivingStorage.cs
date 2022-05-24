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
            .Select(CreateModel)
            .ToList();
        }
        public List<ReceivingViewModel> GetFilteredList(ReceivingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Receivings
            .Where(rec => rec.Id.Equals(model.Id))
            .Select(CreateModel)
            .ToList();
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
            return receiving != null ? CreateModel(receiving) : null;
        }
        public void Insert(ReceivingBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            context.Receivings.Add(CreateModel(model, new Receiving()));
            context.SaveChanges();
        }
        public void Update(ReceivingBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            var element = context.Receivings.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
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
        private static Receiving CreateModel(ReceivingBindingModel model, Receiving
       receiving)
        {
            
            receiving.Price = model.Price;
            receiving.Status = model.Status;
            receiving.DateDispatch = model.DateDispatch;
            receiving.DateCreate = model.DateCreate;

            return receiving;
        }
        private static ReceivingViewModel CreateModel(Receiving receiving)
        {
            return new ReceivingViewModel
            {
                Id = receiving.Id,
                Price = receiving.Price,
                Status = Enum.GetName(receiving.Status),
                DateCreate = receiving.DateCreate,
                DateDispatch = receiving.DateDispatch
            };
        }
    }
}

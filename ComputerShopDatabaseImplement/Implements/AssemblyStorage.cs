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
    public class AssemblyStorage : IAssemblyStorage
    {
        public List<AssemblyViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Assemblies
            .Include(rec => rec.AssemblyOrders)
            .ThenInclude(rec => rec.Order)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public List<AssemblyViewModel> GetFilteredList(AssemblyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Assemblies
            .Include(rec => rec.AssemblyOrders)
            .ThenInclude(rec => rec.Order)
            .Where(rec => rec.AssemblyName.Contains(model.AssemblyName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public AssemblyViewModel GetElement(AssemblyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var assembly = context.Assemblies
            .Include(rec => rec.AssemblyOrders)
            .ThenInclude(rec => rec.Order)
            .FirstOrDefault(rec => rec.AssemblyName == model.AssemblyName || rec.Id == model.Id);
            return assembly != null ? CreateModel(assembly) : null;
        }
        public void Insert(AssemblyBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Assembly assembly = new Assembly()
                {
                    AssemblyName = model.AssemblyName,
                    Price = model.Price
                };
                context.Assemblies.Add(assembly);
                context.SaveChanges();
                CreateModel(model, assembly, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(AssemblyBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Assemblies.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(AssemblyBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Assembly element = context.Assemblies.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Assemblies.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Assembly CreateModel(AssemblyBindingModel model, Assembly assembly, ComputerShopDatabase context)
        {
            assembly.AssemblyName = model.AssemblyName;
            assembly.Price = model.Price;
            if (model.Id.HasValue)
            {
                var assemblyComponents = context.AssemblyOrders.Where(rec => rec.AssemblyId == model.Id.Value).ToList();
                
                context.AssemblyOrders.RemoveRange(assemblyComponents.Where(rec => !model.AssemblyOrders.ContainsKey(rec.OrderId)).ToList());
                context.SaveChanges();
              
                foreach (var updateComponent in assemblyComponents)
                {
                    updateComponent.Count = model.AssemblyOrders[updateComponent.OrderId].Item2;
                    model.AssemblyOrders.Remove(updateComponent.OrderId);
                }
                context.SaveChanges();
            }
           
            foreach (var fc in model.AssemblyOrders)
            {
                context.AssemblyOrders.Add(new AssemblyOrder
                {
                    AssemblyId = assembly.Id,
                    OrderId = fc.Key,
                    Count = fc.Value.Item2
                });
                context.SaveChanges();
            }
            return assembly;
        }
        private static AssemblyViewModel CreateModel(Assembly assembly)
        {
            return new AssemblyViewModel
            {
                Id = assembly.Id,
                AssemblyName = assembly.AssemblyName,
                Price = assembly.Price,
                AssemblyOrders = assembly.AssemblyOrders
                .ToDictionary(recFC => recFC.OrderId, recFC => (recFC.Order?.OrderName, recFC.Count))
            };
        }
    }
}

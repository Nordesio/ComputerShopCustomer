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
    public class CustomerStorage : ICustomerStorage
    {
        public List<CustomerViewModel> GetFullList()
        {
            using var context = new ComputerShopDatabase();
            return context.Customers
            .Select(CreateModel)
            .ToList();
        }
        public List<CustomerViewModel> GetFilteredList(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Customers
            .Where(rec => rec.Email.Contains(model.Email))
            .Select(CreateModel)
            .ToList();
        }
        public CustomerViewModel GetElement(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var customer = context.Customers
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id
           == model.Id);
            return customer != null ? CreateModel(customer) : null;
        }
        public void Insert(CustomerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            context.Customers.Add(CreateModel(model, new Customer()));
            context.SaveChanges();
        }
        public void Update(CustomerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            var element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(CustomerBindingModel model)
        {
            using var context = new ComputerShopDatabase();
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Customer CreateModel(CustomerBindingModel model, Customer
       customer)
        {

            customer.Name = model.Name;

            customer.Password = model.Password;
            customer.Login = model.Login;
            customer.Email = model.Email;

            return customer;
        }
        private static CustomerViewModel CreateModel(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Password = customer.Password,
                Login = customer.Login,
                Email = customer.Email,

            };
        }
    }
}

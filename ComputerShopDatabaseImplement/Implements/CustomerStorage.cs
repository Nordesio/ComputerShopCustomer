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
            .Select(rec => new CustomerViewModel
               {
                   Login = rec.CustomerLogin,
                   Name = rec.Name,
                   Email = rec.Email,
                   Password = rec.Password
               }).ToList();
        }
        public List<CustomerViewModel> GetFilteredList(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            return context.Customers
                
                .Include(x => x.Order)
                .Where(rec => rec.CustomerLogin == model.CustomerLogin)
                        .Select(rec => new CustomerViewModel
                        {
                            Login = rec.CustomerLogin,
                            Name = rec.Name,
                            Email = rec.Email,
                            Password = rec.Password
                        }).ToList();
        }
        public CustomerViewModel GetElement(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ComputerShopDatabase();
            var customer = context.Customers
                .Include(x => x.Order)
            .FirstOrDefault(rec => rec.Email == model.Email || rec.CustomerLogin
           == model.CustomerLogin);
            return customer != null ?
                new CustomerViewModel
                {
                    Login = customer.CustomerLogin,
                    Name = customer.Name,
                    Email = customer.Email,
                    Password = customer.Password,
                }:
                null;
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
            var element = context.Customers.FirstOrDefault(rec => rec.CustomerLogin == model.CustomerLogin);
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
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerLogin ==
           model.CustomerLogin);
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
            customer.CustomerLogin = model.CustomerLogin;
            customer.Name = model.Name;
            customer.Password = model.Password;
           
            customer.Email = model.Email;

            return customer;
        }
     
    }
}

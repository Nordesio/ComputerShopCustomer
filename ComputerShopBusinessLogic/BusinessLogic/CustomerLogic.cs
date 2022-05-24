using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BusinessLogicsContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.StoragesContracts;
namespace ComputerShopBusinessLogic.BusinessLogic
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerStorage _customerStorage;

        public CustomerLogic(ICustomerStorage customerStorage)
        {
            _customerStorage = customerStorage;
        }

        public List<CustomerViewModel> Read(CustomerBindingModel model)
        {
            if (model == null)
            {
                return _customerStorage.GetFullList();
            }
            if (model.CustomerLogin != null)
            {
                return new List<CustomerViewModel> { _customerStorage.GetElement(model) };
            }
            return _customerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CustomerBindingModel model, bool update = false)
        {
            var element = _customerStorage.GetElement(new CustomerBindingModel
            {
                Name = model.Name,
            });
            if (element != null && element.Login != model.CustomerLogin)
            {
                throw new Exception("Уже есть клиент с такой почтой");
            }
            if (update)
            {
                _customerStorage.Update(model);
            }
            else
            {
                _customerStorage.Insert(model);
            }
        }
        public void Delete(CustomerBindingModel model)
        {
            var element = _customerStorage.GetElement(new CustomerBindingModel { CustomerLogin = model.CustomerLogin });
            if (element == null)
            {
                throw new Exception("Заказчик не найден");
            }
            _customerStorage.Delete(model);
        }
    }
}

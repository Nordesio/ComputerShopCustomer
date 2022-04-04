using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.BusinessLogicsContracts
{
    public interface ICustomerLogic
    {
        List<CustomerViewModel> Read(CustomerBindingModel model);
        void CreateOrUpdate(CustomerBindingModel model);
        void Delete(CustomerBindingModel model);
    }
}

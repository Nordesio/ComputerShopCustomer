using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.BusinessLogicsContracts
{
    public interface IDeliveryLogic
    {
        List<DeliveryViewModel> Read(DeliveryBindingModel model);
        void CreateOrUpdate(DeliveryBindingModel model);
        void Delete(DeliveryBindingModel model);
    }
}

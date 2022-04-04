using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.StoragesContracts
{
    public interface IDeliveryStorage
    {
        List<DeliveryViewModel> GetFullList();
        List<DeliveryViewModel> GetFilteredList(DeliveryBindingModel model);
        DeliveryViewModel GetElement(DeliveryBindingModel model);
        void Insert(DeliveryBindingModel model);
        void Update(DeliveryBindingModel model);
        void Delete(DeliveryBindingModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.StoragesContracts
{
    public interface IReceivingStorage
    {
        List<ReceivingViewModel> GetFullList();
        List<ReceivingViewModel> GetFilteredList(ReceivingBindingModel model);
        ReceivingViewModel GetElement(ReceivingBindingModel model);
        void Insert(ReceivingBindingModel model);
        void Update(ReceivingBindingModel model);
        void Delete(ReceivingBindingModel model);
    }
}

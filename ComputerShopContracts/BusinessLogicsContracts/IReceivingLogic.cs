using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.BusinessLogicsContracts
{
    public interface IReceivingLogic
    {
        List<ReceivingViewModel> Read(ReceivingBindingModel model);
        void CreateReceiving(CreateReceivingBindingModel model);
        void TakeReceivingInWork(ChangeReceivingStatusBindingModel model);
        void FinishReceiving(ChangeReceivingStatusBindingModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;

namespace ComputerShopContracts.BusinessLogicsContracts
{
    public interface IAssemblyLogic
    {
        List<AssemblyViewModel> Read(AssemblyBindingModel model);
        void CreateOrUpdate(AssemblyBindingModel model);
        void Delete(AssemblyBindingModel model);
    }
}

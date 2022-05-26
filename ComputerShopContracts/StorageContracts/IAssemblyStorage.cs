using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.ViewModels;
namespace ComputerShopContracts.StoragesContracts
{
    public interface IAssemblyStorage
    {
        List<AssemblyViewModel> GetFullList();
        List<AssemblyViewModel> GetFilteredList(AssemblyBindingModel model);
        AssemblyViewModel GetElement(AssemblyBindingModel model);
        void Insert(AssemblyBindingModel model);
        void Update(AssemblyBindingModel model);
        void Delete(AssemblyBindingModel model);
        public void BindingOrder(int orderId, int assemblyId);
    }
}

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
    public class AssemblyLogic : IAssemblyLogic
    {
        private readonly IAssemblyStorage _assemblyStorage;

        public AssemblyLogic(IAssemblyStorage assemblyStorage)
        {
            _assemblyStorage = assemblyStorage;
        }

        public List<AssemblyViewModel> Read(AssemblyBindingModel model)
        {
            if (model == null)
            {
                return _assemblyStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<AssemblyViewModel> { _assemblyStorage.GetElement(model) };
            }
            return _assemblyStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(AssemblyBindingModel model)
        {
            var element = _assemblyStorage.GetElement(new AssemblyBindingModel
            {
                AssemblyName = model.AssemblyName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть сборка с таким названием");
            }
            if (model.Id.HasValue)
            {
                _assemblyStorage.Update(model);
            }
            else
            {
                _assemblyStorage.Insert(model);
            }
        }
        public void Delete(AssemblyBindingModel model)
        {
            var element = _assemblyStorage.GetElement(new AssemblyBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Клиент не найден");
            }
            _assemblyStorage.Delete(model);
        }
        public void BindingOrder(int assemblyId, int orderId)
        {
            _assemblyStorage.BindingOrder(assemblyId, orderId);
        }
    }
}

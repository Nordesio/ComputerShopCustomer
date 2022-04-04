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
    public class DeliveryLogic : IDeliveryLogic
    {
        private readonly IDeliveryStorage _deliveryStorage;

        public DeliveryLogic(IDeliveryStorage deliveryStorage)
        {
            _deliveryStorage = deliveryStorage;
        }

        public List<DeliveryViewModel> Read(DeliveryBindingModel model)
        {
            if (model == null)
            {
                return _deliveryStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DeliveryViewModel> { _deliveryStorage.GetElement(model) };
            }
            return _deliveryStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DeliveryBindingModel model)
        {
            var element = _deliveryStorage.GetElement(new DeliveryBindingModel
            {
                DeliveryName = model.DeliveryName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть поставка с таким названием");
            }
            if (model.Id.HasValue)
            {
                _deliveryStorage.Update(model);
            }
            else
            {
                _deliveryStorage.Insert(model);
            }
        }
        public void Delete(DeliveryBindingModel model)
        {
            var element = _deliveryStorage.GetElement(new DeliveryBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Поставка не найдена");
            }
            _deliveryStorage.Delete(model);
        }
    }
}

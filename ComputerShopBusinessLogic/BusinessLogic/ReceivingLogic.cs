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
    public class ReceivingLogic : IReceivingLogic
    {
        private readonly IReceivingStorage _receivingStorage;
        public ReceivingLogic(IReceivingStorage receivingStorage)
        {
            _receivingStorage = receivingStorage;
        }

        public List<ReceivingViewModel> Read(ReceivingBindingModel model)
        {
            if (model == null)
            {
                return _receivingStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ReceivingViewModel> { _receivingStorage.GetElement(model) };
            }
            return _receivingStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ReceivingBindingModel model)
        {
            var element = _receivingStorage.GetElement(new ReceivingBindingModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть поставка");
            }
            if (model.Id.HasValue)
            {
                _receivingStorage.Update(model);
            }
            else
            {
                _receivingStorage.Insert(model);
            }
        }
        public void Delete(ReceivingBindingModel model)
        {
            var element = _receivingStorage.GetElement(new ReceivingBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Заказчик не найден");
            }
            _receivingStorage.Delete(model);
        }

    }
}

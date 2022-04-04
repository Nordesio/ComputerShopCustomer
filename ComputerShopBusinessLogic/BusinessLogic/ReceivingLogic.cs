using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.BusinessLogicsContracts;
using ComputerShopContracts.ViewModels;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.Enums;
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
        public void CreateReceiving(CreateReceivingBindingModel model)
        {
            _receivingStorage.Insert(new ReceivingBindingModel
            {
                DeliveryId = model.DeliveryId,
                Price = model.Price,
                DateCreate = DateTime.Now,
                Status = ReceivingStatus.Принят

            });
        }
        public void TakeReceivingInWork(ChangeReceivingStatusBindingModel model)
        {
            var receiving = _receivingStorage.GetElement(new ReceivingBindingModel { Id = model.ReceivingId });

            if (receiving == null) throw new Exception("Элемент не найден");

            if (!receiving.Status.Contains(ReceivingStatus.Принят.ToString())) throw new Exception("Не в статусе \"Принят\"");
            _receivingStorage.Update(new ReceivingBindingModel
            {
                Id = receiving.Id,
                DeliveryId = receiving.DeliveryId,
                Price = receiving.Price,
                DateCreate = receiving.DateCreate,
                Status = ReceivingStatus.Выполняется,
            });
        }
        public void FinishReceiving(ChangeReceivingStatusBindingModel model)
        {
            var receiving = _receivingStorage.GetElement(new ReceivingBindingModel { Id = model.ReceivingId });

            if (receiving == null) throw new Exception("Элемент не найден");

            if (!receiving.Status.Contains(ReceivingStatus.Принят.ToString())) throw new Exception("Не в статусе \"Выполняется\"");

            _receivingStorage.Update(new ReceivingBindingModel
            {
                Id = receiving.Id,
                DeliveryId = receiving.DeliveryId,
                Price = receiving.Price,
                DateCreate = receiving.DateCreate,
                DateDispatch = receiving.DateDispatch,
                Status = ReceivingStatus.Готов,
            });
        }
    }
}

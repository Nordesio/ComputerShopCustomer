using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.Enums;
namespace ComputerShopContracts.BindingModel
{
    public class ReceivingBindingModel
    {
        public int? Id { get; set; }
        public int? DeliveryId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreate { get; set; }
/// <summary>
/// Дата отправки
/// </summary>
        public DateTime DateDispatch { get; set; }
    }
}

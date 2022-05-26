using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.BindingModel
{
    public class ReceivingBindingModel
    {
        public int? Id { get; set; }
        public string DeliveryName { get; set; }
        public int DeliveryId { get; set; }
        public decimal Price { get; set; }
/// <summary>
/// Дата отправки
/// </summary>
        public DateTime DateDispatch { get; set; }
    }
}

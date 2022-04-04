using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ComputerShopContracts.ViewModels
{
    public class ReceivingViewModel
    {
        public int Id { get; set; }
        public int DeliveryId { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Сумма")]
        public decimal Price { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата отправки")]
        public DateTime DateDispatch { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ComputerShopContracts.ViewModels
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Поставка")]
        public string DeliveryName { get; set; }

        public int OrderId { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> DeliveryComponents { get; set; }
    }
}

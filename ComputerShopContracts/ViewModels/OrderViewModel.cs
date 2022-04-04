using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ComputerShopContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DisplayName("Заказ")]
        public string OrderName { get; set; }
        [DisplayName("Цена")]
        public int Price { get; set; }
        [DisplayName("Дата получения")]
        public DateTime DateReceipt { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> OrderCustomers { get; set; }
    }
}

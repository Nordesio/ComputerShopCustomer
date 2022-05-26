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
        public string CustomerLogin { get; set; }
      
    }
}

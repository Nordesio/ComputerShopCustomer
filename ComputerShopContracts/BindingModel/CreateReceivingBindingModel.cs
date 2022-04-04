using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.BindingModel
{
    public class CreateReceivingBindingModel
    {
        public int? DeliveryId { get; set; }
        public int Price { get; set; }
    }
}

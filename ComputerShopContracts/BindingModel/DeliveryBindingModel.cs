using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerShopContracts.Enums;

namespace ComputerShopContracts.BindingModel
{
    public class DeliveryBindingModel
    {
        public int? Id { get; set; }
        public int OrderId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreate { get; set; }
    }
}

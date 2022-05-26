using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.ViewModels
{
    public class ReportReceivingsViewModel
    {
        public string DeliveryName { get; set; }
        public DateTime DateDispatch { get; set; }
        public decimal Price { get; set; }
    }
}

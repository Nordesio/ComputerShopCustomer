using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopContracts.ViewModels
{
    public class ReportCustomerOrderViewModel
    {
        public string CustomerName { get; set; }
        public DateTime DateCreate { get; set; }
        public string OrderName { get; set; }
        public int Price { get; set; }
    }
}

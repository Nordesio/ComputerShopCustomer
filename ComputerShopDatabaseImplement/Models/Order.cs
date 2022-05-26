using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComputerShopDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string OrderName { get; set; }
        public int Price { get; set; }
        [ForeignKey("CustomerLogin")]
        public string CustomerLogin { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<AssemblyOrder> AssemblyOrders { get; set; }
    }
}

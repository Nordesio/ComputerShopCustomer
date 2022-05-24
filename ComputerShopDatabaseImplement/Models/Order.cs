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
        public string CustomerLogin { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        public DateTime DateReceipt { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<Delivery> Deliveries { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<AssemblyOrder> AssemblyOrders { get; set; }
    }
}

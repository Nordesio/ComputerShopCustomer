using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComputerShopDatabaseImplement.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string DeliveryName { get; set; }
        public int OrderId { get; set; }
        public DateTime DateCreate { get; set; }
        [ForeignKey("DeliveryId")]
        public virtual List<Receiving> Receivings { get; set; }
        [ForeignKey("DeliveryId")]
        public virtual List<DeliveryComponent> DeliveryComponents { get; set; }
        public virtual Order Order { get; set; }
    }
}
    
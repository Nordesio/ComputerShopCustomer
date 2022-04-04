using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabaseImplement.Models
{
    public class DeliveryComponent
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int DeliveryId { get; set; }
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}

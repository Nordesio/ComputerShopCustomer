using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ComputerShopDatabaseImplement.Models
{
    public class OrderCustomer
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Order Order { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

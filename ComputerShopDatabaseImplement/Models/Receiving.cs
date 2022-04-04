using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using ComputerShopContracts.Enums;
namespace ComputerShopDatabaseImplement.Models
{
    public class Receiving
    {
          public int Id { get; set; }
        public int DeliveryId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public ReceivingStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime DateDispatch { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}

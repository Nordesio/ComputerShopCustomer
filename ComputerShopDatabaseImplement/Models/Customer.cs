using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComputerShopDatabaseImplement.Models
{
    public class Customer
    {
        [Key]
        public string CustomerLogin { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [ForeignKey("CustomerLogin")]
        public List<Order> Order { get; set; }
    }
}

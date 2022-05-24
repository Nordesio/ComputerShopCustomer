using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComputerShopDatabaseImplement.Models
{
    public class Assembly
    {
        public int Id { get; set; }
        [Required]
        public string AssemblyName { get; set; }
        public int Price { get; set; }
       
        [ForeignKey("AssemblyId")]
        public virtual List<AssemblyOrder> AssemblyOrders { get; set; }
    }
}

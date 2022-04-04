using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ComputerShopDatabaseImplement.Models
{
    public class AssemblyOrder
    {
        public int Id { get; set; }
        public int AssemblyId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public virtual Assembly Assembly { get; set; }
        public virtual Order Order { get; set; }
    }
}

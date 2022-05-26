using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ComputerShopContracts.ViewModels
{
    public class AssemblyViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название сборки")]
        public string AssemblyName { get; set; }
        [DisplayName("Цена")]
        public int Price { get; set; }
        public Dictionary<int, string> Orders { get; set; }
    }
}

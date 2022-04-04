using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ComputerShopContracts.ViewModels
{
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Компонент")]
        public string ComponentName { get; set; }
    }
}

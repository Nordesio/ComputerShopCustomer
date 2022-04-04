using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ComputerShopContracts.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя заказчика")]
        public string Name { get; set; }
        [DisplayName("Логин заказчика")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DisplayName("Электронная почта")]
        public string Email { get; set; }
      
    }
}

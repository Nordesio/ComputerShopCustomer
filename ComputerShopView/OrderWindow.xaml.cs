using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.BindingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace ComputerShopView
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }

        private int? id;

        public string Login { set { login = value; } }

        private string login;

        private readonly OrderLogic _orderLogic;

        public OrderWindow(OrderLogic logic)
        {
            InitializeComponent();
            _orderLogic = logic;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    Id = id,
                    OrderName = TextBoxName.Text,
                    Price = Convert.ToInt32(TextBoxPrice.Text),
                    CustomerLogin = login,
                });
                MessageBox.Show("Создано", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}

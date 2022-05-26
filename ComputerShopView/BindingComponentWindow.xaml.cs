using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ComputerShopContracts.BindingModel;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.ViewModels;

namespace ComputerShopView
{
    /// <summary>
    /// Логика взаимодействия для BindingComponent.xaml
    /// </summary>
    public partial class BindingComponentWindow : Window
    {
        private readonly ComponentLogic _componentLogic;
        private readonly DeliveryLogic _deliveryLogic;
        public int Id { set { id = value; } }

        private int? id;
        public string Login { set { login = value; } }
        private string login;
        public BindingComponentWindow(DeliveryLogic deliveryLogic, ComponentLogic componentLogic)
        {
            InitializeComponent();
            _componentLogic = componentLogic;
            _deliveryLogic = deliveryLogic;
        }
        private void BindingComponent_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ComboBoxDelivery.ItemsSource = _deliveryLogic.Read(null);
                ListBoxComponent.ItemsSource = _componentLogic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonBinding_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDelivery.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите поставку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxComponent.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var component = _componentLogic.Read(new ComponentBindingModel { Id = (ListBoxComponent.SelectedItem as ComponentViewModel).Id })?[0];
                var delivery = _deliveryLogic.Read(new DeliveryBindingModel { Id = (ComboBoxDelivery.SelectedItem as DeliveryViewModel).Id })?[0];
                if (component == null)
                {
                    throw new Exception("Компонент не найден");
                }
                if (delivery == null)
                {
                    throw new Exception("Поставка не найдена");
                }
                if (component.Deliveries.ContainsKey(delivery.Id))
                {
                    throw new Exception("Компонент уже привязан к данному предмету");
                }
                _componentLogic.BindingDelivery(component.Id, delivery.Id);
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void ButtonComponent_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            _componentLogic.CreateOrUpdate(new ComponentBindingModel
            {
                Id = id,
                ComponentName = "Component" + rnd.Next(100)
            });
            LoadData();
        }
    }
}

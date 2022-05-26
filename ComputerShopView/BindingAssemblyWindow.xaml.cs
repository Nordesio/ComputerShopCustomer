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
    /// Логика взаимодействия для BindingAssemblyWindow.xaml
    /// </summary>
    public partial class BindingAssemblyWindow : Window
    {
        private readonly AssemblyLogic _assemblyLogic;
        private readonly OrderLogic _orderLogic;
        public int Id { set { id = value; } }

        private int? id;
        public string Login { set { login = value; } }
        private string login;
        public BindingAssemblyWindow(AssemblyLogic assemblyLogic, OrderLogic orderLogic)
        {
            InitializeComponent();
            _assemblyLogic = assemblyLogic;
            _orderLogic = orderLogic;
        }
        private void BindingAssembly_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ComboBoxOrder.ItemsSource = _orderLogic.Read(null);
                ListBoxAssembly.ItemsSource = _assemblyLogic.Read(null);
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
            if (ComboBoxOrder.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxAssembly.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите сборку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var assembly = _assemblyLogic.Read(new AssemblyBindingModel { Id = (ListBoxAssembly.SelectedItem as AssemblyViewModel).Id })?[0];
                var order = _orderLogic.Read(new OrderBindingModel { Id = (ComboBoxOrder.SelectedItem as OrderViewModel).Id })?[0];
                if (assembly == null)
                {
                    throw new Exception("Сборка не найдена");
                }
                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                if (assembly.Orders.ContainsKey(order.Id))
                {
                    throw new Exception("Сборка уже привязана к данному заказу");
                }
                _assemblyLogic.BindingOrder(assembly.Id, order.Id);
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
            _assemblyLogic.CreateOrUpdate(new AssemblyBindingModel
            {
                Id = id,
                AssemblyName = "Assembly" + rnd.Next(100),
                Price = rnd.Next(10000)
            });
            LoadData();
        }
    }
}

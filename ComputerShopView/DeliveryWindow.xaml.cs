using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using ComputerShopContracts.BindingModel;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.ViewModels;

namespace ComputerShopView
{
    /// <summary>
    /// Логика взаимодействия для DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;
        public int OrderID
        {
            get { return Convert.ToInt32((ComboBoxOrder.SelectedItem as OrderViewModel).Id); }
            set
            {
                subjectId = value;
            }
        }
        private int? subjectId;
        public string Login { set { login = value; } }

        private string login;
        private readonly DeliveryLogic _logicDelivery;
        private readonly OrderLogic _logicOrder;

        public DeliveryWindow(DeliveryLogic logicDelivery, OrderLogic logicOrder)
        {
            InitializeComponent();
            this._logicDelivery = logicDelivery;
            this._logicOrder = logicOrder;
        }
        private void DeliveryWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxOrder.ItemsSource = _logicOrder.Read(new OrderBindingModel { CustomerLogin = login });
            ComboBoxOrder.SelectedItem = SetValue(subjectId);
            if (id.HasValue)
            {
                try
                {
                    var view = _logicDelivery.Read(new DeliveryBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.DeliveryName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxOrder.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicDelivery.CreateOrUpdate(new DeliveryBindingModel
                {
                    Id = id,
                    DeliveryName = TextBoxName.Text,
                    DateCreate = DateTime.Now,
                    OrderId = OrderID,
                }) ;
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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

        private OrderViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxOrder.Items)
            {
                if ((item as OrderViewModel).Id == value)
                {
                    return item as OrderViewModel;
                }
            }
            return null;
        }
    }
}

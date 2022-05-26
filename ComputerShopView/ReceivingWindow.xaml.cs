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
    /// Логика взаимодействия для ReceivingWindow.xaml
    /// </summary>
    public partial class ReceivingWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }
        public int DeliveryID
        {
            get { return Convert.ToInt32((ComboBoxDelivery.SelectedItem as DeliveryViewModel).Id); }
            set
            {
                subjectId = value;
            }
        }
        private int? subjectId;
        private int? id;
        public string Login { set { login = value; } }

        private string login;
        private readonly ReceivingLogic _logicReceiving;
        private readonly DeliveryLogic _logicDelivery;


        public ReceivingWindow(ReceivingLogic logicReceiving, DeliveryLogic logicDelivery)
        {
            InitializeComponent();
            this._logicReceiving = logicReceiving;
            this._logicDelivery = logicDelivery;
        }
        private void ReceivingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxDelivery.ItemsSource = _logicDelivery.Read(null);
            ComboBoxDelivery.SelectedItem = SetValue(subjectId);
        }
            private void ButtonSave_Click(object sender, RoutedEventArgs e)
             {
                _logicReceiving.CreateOrUpdate(new ReceivingBindingModel
                {
                    Id = id,
                    DateDispatch = DateTime.Now,
                    Price = Convert.ToInt32(TextBoxPrice.Text),
                    DeliveryId = DeliveryID,
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
             }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private DeliveryViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxDelivery.Items)
            {
                if ((item as DeliveryViewModel).Id == value)
                {
                    return item as DeliveryViewModel;
                }
            }
            return null;
        }
    }
}

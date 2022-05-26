using System;
using System.Linq;
using System.Windows;
using Unity;
using ComputerShopContracts.BindingModel;
using ComputerShopBusinessLogic.BusinessLogic;

namespace ComputerShopView
{
    public partial class AuthorizationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly CustomerLogic _customerLogic;

        public AuthorizationWindow(CustomerLogic customerLogic)
        {
            InitializeComponent();
            _customerLogic = customerLogic;
        }
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCustomerLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                MessageBox.Show("Пустое поле 'Пароль'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var viewCustomer = _customerLogic.Read(new CustomerBindingModel
                {
                    CustomerLogin = TextBoxCustomerLogin.Text,
                });
                if (viewCustomer != null && viewCustomer[0] != null && viewCustomer.Count > 0 && viewCustomer[0].Password == TextBoxPassword.Password)
                {
                    var window = Container.Resolve<MainWindow>();
                    window.Login = viewCustomer[0].Login;
                    window.ShowDialog();
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<RegistrationWindow>();
            window.ShowDialog();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
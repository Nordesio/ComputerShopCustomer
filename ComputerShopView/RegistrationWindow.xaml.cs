using System;
using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using ComputerShopContracts.BindingModel;
using ComputerShopBusinessLogic.BusinessLogic;

namespace ComputerShopView
{
    public partial class RegistrationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly CustomerLogic _customerLogic;
        public RegistrationWindow(CustomerLogic customerLogic)
        {
            InitializeComponent();
            _customerLogic = customerLogic;
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
                MessageBox.Show("Пустое поле 'Имя'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxName.Text.Length <= 255 && TextBoxName.Text.Length >= 2))
            {
                MessageBox.Show("Имя должно иметь длину не более 255 символов и не менее 2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxCustomerLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxCustomerLogin.Text.Length <= 50 && TextBoxCustomerLogin.Text.Length >= 2))
            {
                MessageBox.Show("Логин должен иметь длину не более 50 и не менее 2 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                MessageBox.Show("Пустое поле 'Пароль'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxPassword.Password.Length <= 50 && TextBoxPassword.Password.Length >= 6))
            {
                MessageBox.Show("Пароль должен иметь длину не более 50 и не менее 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxEmail.Text))
            {
                MessageBox.Show("Пустое поле 'Email'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!Regex.IsMatch(TextBoxEmail.Text, @"^[A-Za-z0-9]+(?:[._%+-])?[A-Za-z0-9._-]+[A-Za-z0-9]@[A-Za-z0-9]+(?:[.-])?[A-Za-z0-9._-]+\.[A-Za-z]{2,6}$"))
            {
                MessageBox.Show("Email невалидный", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _customerLogic.CreateOrUpdate(new CustomerBindingModel
                {
                    Name = TextBoxName.Text,
                    Email = TextBoxEmail.Text,
                    CustomerLogin = TextBoxCustomerLogin.Text,
                    Password = TextBoxPassword.Password,
                }) ;
                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}

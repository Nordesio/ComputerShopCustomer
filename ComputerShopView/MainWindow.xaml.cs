using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.BindingModel;

namespace ComputerShopView
{
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Login { set { login = value; } }

        private string login;

        private readonly CustomerLogic _customerLogic;

        public MainWindow(CustomerLogic logic)
        {
            InitializeComponent();
            _customerLogic = logic;
        }
        
        private void MenuItemOrder_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrdersWindow>();
            form.Login = login;
            form.ShowDialog();
        }

        private void MenuItemDelivery_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<DeliverysWindow>();
            form.Login = login;
            form.ShowDialog();
        }
        private void MenuItemReceiving_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ReceivingsWindow>();
            form.Login = login;
            form.ShowDialog();
        }
        private void MenuItemComponent_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<BindingComponentWindow>();
            form.ShowDialog();
        }
        private void MenuItemAssembly_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<BindingAssemblyWindow>();
            form.ShowDialog();
        }
    }
}

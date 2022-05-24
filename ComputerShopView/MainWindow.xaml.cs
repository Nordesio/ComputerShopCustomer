using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using ComputerShopBusinessLogic.BusinessLogic;

namespace ComputerShopView
{
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string CustomerLogin { set { login = value; } }

        private string login;

        private readonly CustomerLogic _customerLogic;

        public MainWindow(CustomerLogic logic)
        {
            InitializeComponent();
            _customerLogic = logic;
        }
        
        private void MenuItemOrder_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<OrderWindow>();
            //form.CustomerLogin = login;
            form.ShowDialog();
        }

        private void MenuItemDelivery_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<DeliveryWindow>();
            //form.CustomerLogin = login;
            form.ShowDialog();
        }
        private void MenuItemReceiving_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ReceivingWindow>();
            //form.CustomerLogin = login;
            form.ShowDialog();
        }
        private void MenuItemList_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ListWindow>();
            //form.CustomerLogin = login;
            form.ShowDialog();
        }
        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<ReportWindow>();
            //form.CustomerLogin = login;
            form.ShowDialog();
        }
        
    }
}

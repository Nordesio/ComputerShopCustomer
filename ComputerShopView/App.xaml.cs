using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.StoragesContracts;
using ComputerShopContracts.BusinessLogicsContracts;
using ComputerShopBusinessLogic.OfficePackage;
using ComputerShopBusinessLogic.OfficePackage.Implements;
using ComputerShopDatabaseImplement.Implements;

namespace ComputerShopView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var startWindow = Container.Resolve<AuthorizationWindow>();
            startWindow.Show();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            
            currentContainer.RegisterType<IAssemblyStorage, AssemblyStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentStorage, ComponentStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerStorage, CustomerStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDeliveryStorage, DeliveryStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReceivingStorage, ReceivingStorage>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAssemblyLogic, AssemblyLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerLogic, CustomerLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDeliveryLogic, DeliveryLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReceivingLogic, ReceivingLogic>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<AbstractSaveToExcel, SaveToExcel>(new
            HierarchicalLifetimeManager());
         
            currentContainer.RegisterType<AbstractSaveToWord, SaveToWord>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}

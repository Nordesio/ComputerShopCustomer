using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using ComputerShopContracts.BindingModel;
using ComputerShopContracts.BusinessLogicsContracts;
using ComputerShopBusinessLogic.BusinessLogic;
using ComputerShopContracts.ViewModels;
using Microsoft.Win32;
namespace ComputerShopView
{
    /// <summary>
    /// Логика взаимодействия для ReceivingsWindow.xaml
    /// </summary>
    public partial class ReceivingsWindow : Window
    {
		[Dependency]
		public IUnityContainer Container { get; set; }
		private readonly IReportLogic _reportLogic;
		public string Login { set { login = value; } }

		private string login;

		private readonly ReceivingLogic logic;
		public ReceivingsWindow(ReceivingLogic logic, IReportLogic reportLogic)
		{

			InitializeComponent();
			this.logic = logic;
			_reportLogic = reportLogic;
		}
		private void DeliverysWindow_Loaded(object sender, RoutedEventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				var list = logic.Read(null);
				if (list != null)
				{
					DataGridView.ItemsSource = list;
					DataGridView.Columns[0].Visibility = Visibility.Hidden;
					DataGridView.Columns[2].Visibility = Visibility.Hidden;
					//DataGridView.Columns[5].Visibility = Visibility.Hidden;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			var window = Container.Resolve<ReceivingWindow>();
			window.Login = login;
			if (window.ShowDialog().Value)
			{
				LoadData();
			}
		}

		private void ButtonUpd_Click(object sender, RoutedEventArgs e)
		{

			if (DataGridView.SelectedCells.Count != 0)
			{
				var window = Container.Resolve<OrderWindow>();
				window.Id = ((DeliveryViewModel)DataGridView.SelectedCells[0].Item).Id;
				window.Login = login;
				if (window.ShowDialog().Value)
				{
					LoadData();
				}
			}

		}
		
		private void ButtonDel_Click(object sender, RoutedEventArgs e)
		{
			if (DataGridView.SelectedCells.Count != 0)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					int id = ((ReceivingViewModel)DataGridView.SelectedCells[0].Item).Id;
					try
					{
						logic.Delete(new ReceivingBindingModel { Id = id });
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					LoadData();
				}
			}
		}

		private void ButtonRef_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}

		/// <summary>
		/// Данные для привязки DisplayName к названиям столбцов
		/// </summary>
		private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
			if (!string.IsNullOrEmpty(displayName))
			{
				e.Column.Header = displayName;
			}
		}

		/// <summary>
		/// метод привязки DisplayName к названию столбца
		/// </summary>
		public static string GetPropertyDisplayName(object descriptor)
		{
			if (descriptor is PropertyDescriptor pd)
			{
				// Check for DisplayName attribute and set the column header accordingly
				if (pd.Attributes[typeof(DisplayNameAttribute)] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
				{
					return displayName.DisplayName;
				}
			}
			else
			{
				PropertyInfo pi = descriptor as PropertyInfo;
				if (pi != null)
				{
					// Check for DisplayName attribute and set the column header accordingly
					Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
					for (int i = 0; i < attributes.Length; ++i)
					{
						if (attributes[i] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
						{
							return displayName.DisplayName;
						}
					}
				}
			}
			return null;
		}
		
			private void ButtonWord_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "docx|*.docx";
			if (saveFileDialog.ShowDialog() == true)
			{
				_reportLogic.SaveReceivingsToWordFile(new ReportBindingModel
				{
					FileName = saveFileDialog.FileName
				});
				MessageBox.Show("Выполнено");
			}
		}
		private void ButtonExcel_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "xlsx|*.xlsx";
			if (saveFileDialog.ShowDialog() == true)
			{
				_reportLogic.SaveReceivingsToExcelFile(new ReportBindingModel
				{
					FileName = saveFileDialog.FileName
				});
				MessageBox.Show("Выполнено");
			}
		}
	}
}

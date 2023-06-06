using Greenhouse.Classes.WindowModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.TypeGreenhouse
{
	public class TypeGreenhouseListVM : BaseListVM<TypeGreenhouseVM>
	{
		public TypeGreenhouseListVM(BaseDataContext parent) : base(parent)
		{
			LoadItems();
		}

		#region Delegates

		public Action AfterCloseAction;

		#endregion

		#region Methods

		protected override void LoadItems()
		{
			var items = LoadTypeGreenhouse();
			foreach (var item in items)
			{
				ItemVMs.Add(new TypeGreenhouseVM(item, this));
			}
			base.LoadItems();
		}

		protected override void AddNewItem()
		{
			ItemVMs.Add(new TypeGreenhouseVM(new TypeGreenhouse(), this));
		}

		public void OpenDictionary()
		{
			var win = new WorkWindow
			{
				Title = "Виды оранжерей",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/TypesGreenhouse.xaml") },
				DataContext = new TypesGreenhouseModel()
			};
			win.Closing += AfterClose;
			win.Show();
		}

		private void AfterClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			AfterCloseAction?.Invoke();
		}

		#endregion

		#region Methods to Linq

		/// <summary>
		/// Получение списка видов оранжерей из БД
		/// </summary>
		private List<TypeGreenhouse> LoadTypeGreenhouse()
		{
			try
			{
				var dataContext = new TypeGreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<TypeGreenhouse> items = dataContext.LoadTypesGreenhouse().ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<TypeGreenhouse>();
			}
		}

		#endregion
	}
}

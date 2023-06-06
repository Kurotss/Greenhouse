using Greenhouse.Classes.WindowModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.Greenhouse
{
	public class GreenhouseListVM : BaseListVM<GreenhouseVM>
	{
		public GreenhouseListVM(BaseDataContext parent) : base(parent)
		{
			LoadItems();
		}

		#region Filters

		#region TypeGreenhouseId

		private int? _typeGreenhouseId;

		/// <summary>
		/// Фильтр по виду оранжереи
		/// </summary>
		public int? TypeGreenhouseId
		{
			get => _typeGreenhouseId;
			set
			{
				_typeGreenhouseId = value;
				OnPropertyChanged(nameof(TypeGreenhouseId));
				Refresh();
			}
		}

		#endregion

		public void ClearFilters()
		{
			TypeGreenhouseId = null;
		}

		#endregion

		#region Commands

		private RelayCommand _openCardCommand;

		public RelayCommand OpenCardCommand => _openCardCommand ?? (_openCardCommand = new RelayCommand(param => OpenCard(), param => true));

		private void OpenCard()
		{
			if (SelectedItem == null)
			{
				MessageBox.Show("Необходимо выбрать элемент.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			OpenCard(SelectedItem.GreenhouseId);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Метод для открытия карточки оранжереи
		/// </summary>
		public void OpenCard(int? greenhouseId)
		{
			var win = new WorkWindow
			{
				Title = "Оранжерея",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/Greenhouse.xaml") },
				DataContext = new GreenhouseModel(greenhouseId)
			};
			win.Closing += AfterCardClose;
			win.Show();
		}

		private void AfterCardClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			LoadItems();
		}

		protected override void AddNewItem()
		{
			OpenCard(null);
		}

		protected override void LoadItems()
		{
			var items = LoadGreenhouses();
			foreach (var item in items)
			{
				ItemVMs.Add(new GreenhouseVM(item, this));
			}
			base.LoadItems();
		}

		#endregion

		#region Methods to Linq

		/// <summary>
		/// Получение списка оранжерей из БД
		/// </summary>
		private List<Greenhouse> LoadGreenhouses()
		{
			try
			{
				var dataContext = new GreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<Greenhouse> items = dataContext.LoadGreenhouses(TypeGreenhouseId).ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<Greenhouse>();
			}
		}

		#endregion
	}
}

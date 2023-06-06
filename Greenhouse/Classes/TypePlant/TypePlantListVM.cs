using Greenhouse.Classes.WindowModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.TypePlant
{
	public class TypePlantListVM : BaseListVM<TypePlantVM>
	{
		public TypePlantListVM(BaseDataContext parent) : base(parent)
		{
			LoadItems();
		}

		#region Delegates

		public Action AfterCloseAction;

		#endregion

		#region Filters

		#region GroupCultureId

		private int? _groupCultureId;

		/// <summary>
		/// Фильтр по группе культуры
		/// </summary>
		public int? GroupCultureId
		{
			get => _groupCultureId;
			set
			{
				_groupCultureId = value;
				OnPropertyChanged(nameof(GroupCultureId));
				Refresh();
			}
		}

		#endregion

		#region TypeCultureId

		private int? _typeCultureId;

		/// <summary>
		/// Фильтр по виду культуры
		/// </summary>
		public int? TypeCultureId
		{
			get => _typeCultureId;
			set
			{
				_typeCultureId = value;
				OnPropertyChanged(nameof(TypeCultureId));
				Refresh();
			}
		}

		#endregion

		public void ClearFilters()
		{
			GroupCultureId = null;
			TypeCultureId = null;
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
			OpenCard(SelectedItem.TypePlantId);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Метод для открытия карточки вида растения
		/// </summary>
		public void OpenCard(int? typePlantId)
		{
			var win = new WorkWindow
			{
				Title = "Вид растения",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/TypePlant.xaml") },
				DataContext = new TypePlantModel(typePlantId)
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
			var items = LoadTypesPlant();
			foreach (var item in items)
			{
				ItemVMs.Add(new TypePlantVM(item, this));
			}
			base.LoadItems();
		}

		public void OpenDictionary()
		{
			var win = new WorkWindow
			{
				Title = "Виды растений",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/TypesPlant.xaml") },
				DataContext = new TypesPlantModel()
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
		/// Получение списка видов растений из БД
		/// </summary>
		private List<TypePlant> LoadTypesPlant()
		{
			try
			{
				var dataContext = new TypePlantContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<TypePlant> items = dataContext.LoadTypesPlant(GroupCultureId, TypeCultureId).ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<TypePlant>();
			}
		}

		#endregion
	}
}

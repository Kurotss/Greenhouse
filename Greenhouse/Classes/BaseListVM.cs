using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes
{
	public class BaseListVM<T> : BaseDataContext
	{
		#region Constructor

		public BaseListVM(BaseDataContext parent)
		{
		}

		#endregion

		#region Properties

		private ObservableCollection<T> _itemVMs;

		/// <summary>
		/// Коллекция VM
		/// </summary>
		public ObservableCollection<T> ItemVMs => _itemVMs ?? (_itemVMs = new ObservableCollection<T>());

		private T _selectedItem;

		/// <summary>
		/// Выбранный элемент
		/// </summary>
		public T SelectedItem
		{
			get => _selectedItem;
			set
			{
				_selectedItem = value;
				OnPropertyChanged(nameof(SelectedItem));
			}
		}

		public bool IsComboBoxMode { get; set; }

		#endregion

		#region Methods

		protected virtual void LoadItems()
		{
			if (!IsComboBoxMode)
				SelectedItem = ItemVMs.FirstOrDefault();
		}

		/// <summary>
		/// Обновление списка из БД
		/// </summary>
		public void Refresh()
		{
			LoadItems();
			foreach (var item in ItemVMs)
			{
				(item as BaseVM).RefreshChildItems();
			}
		}

		/// <summary>
		/// Сохранение данных
		/// </summary>
		public bool SaveItems()
		{
			var result = true;
			foreach (var item in ItemVMs)
			{
				result = (item as BaseVM).SaveItem();
				if (!result)
					break;
			}
			return result;
		}

		#endregion

		#region Commands

		/// <summary>
		/// Добавление нового элемента в список
		/// </summary>
		#region AddNewItemCommand

		private RelayCommand _addNewItemCommand;

		public RelayCommand AddNewItemCommand => _addNewItemCommand ?? (_addNewItemCommand = new RelayCommand(param => AddNewItem(), param => true));

		protected virtual void AddNewItem()
		{
			throw new Exception("Добавление элемента в список - не реализовано.");
		}

		#endregion

		/// <summary>
		/// Удаление элемента из списка
		/// </summary>
		#region DeleteItemCommand

		private RelayCommand _deleteItemCommand;

		public RelayCommand DeleteItemCommand => _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(param => DeleteItem(), param => true));

		private void DeleteItem()
		{
			if (SelectedItem == null)
			{
				MessageBox.Show("Выберите элемент.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (!(SelectedItem as BaseVM).CanDelete)
			{
				MessageBox.Show("Выбранный элемент невозможно удалить.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (MessageBox.Show("Удалить выбранный элемент?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Information) != MessageBoxResult.Yes)
				return;
			var result = (SelectedItem as BaseVM).DeleteItem();
			if (result)
				ItemVMs.Remove(SelectedItem);
			OnPropertyChanged(nameof(ItemVMs));
		}

		#endregion

		#endregion
	}
}

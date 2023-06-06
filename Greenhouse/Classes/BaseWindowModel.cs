using System;

namespace Greenhouse.Classes
{
	public class BaseWindowModel : BaseDataContext
	{
		#region Commands

		/// <summary>
		/// Обновление окна
		/// </summary>
		#region RefreshCommand

		private RelayCommand _refreshCommand;

		public RelayCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(param => Refresh(), param => true));

		protected virtual void Refresh()
		{
			throw new Exception("Обновление окна - нет реализации.");
		}

		#endregion

		/// <summary>
		/// Сохранение данных в окне
		/// </summary>
		#region SaveCommand

		private RelayCommand _saveCommand;

		public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(param => Save(), param => true));

		protected virtual void Save()
		{
			throw new Exception("Сохранение данных в окне - нет реализации.");
		}

		#endregion

		/// <summary>
		/// Сброс значений в фильтрах
		/// </summary>
		#region ResetFiltersCommand

		private RelayCommand _resetFiltersCommand;

		public RelayCommand ResetFiltersCommand => _resetFiltersCommand ?? (_resetFiltersCommand = new RelayCommand(param => ResetFilters(), param => true));

		protected virtual void ResetFilters()
		{
			throw new Exception("Сброс значений в фильтрах - нет реализации.");
		}

		#endregion

		#endregion
	}
}

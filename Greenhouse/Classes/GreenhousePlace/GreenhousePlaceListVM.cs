using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.GreenhousePlace
{
	public class GreenhousePlaceListVM : BaseListVM<GreenhousePlaceVM>
	{
		public GreenhousePlaceListVM(int greenhouseId, BaseDataContext parent) : base(parent)
		{
			_greenhouseId = greenhouseId;
			LoadItems();
		}

		private int _greenhouseId;

		#region Methods

		protected override void LoadItems()
		{
			var items = LoadGreenhousePlaces();
			foreach (var item in items)
			{
				ItemVMs.Add(new GreenhousePlaceVM(item, this));
			}
			base.LoadItems();
		}

		protected override void AddNewItem()
		{
			ItemVMs.Add(new GreenhousePlaceVM(new GreenhousePlace() { GreenhouseId = _greenhouseId }, this));
		}

		#endregion

		#region Methods to Linq

		/// <summary>
		/// Получение списка мест оранжереи из БД
		/// </summary>
		private List<GreenhousePlace> LoadGreenhousePlaces()
		{
			try
			{
				var dataContext = new GreenhousePlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<GreenhousePlace> items = dataContext.LoadGreenhousePlaces(_greenhouseId).ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<GreenhousePlace>();
			}
		}

		#endregion
	}
}

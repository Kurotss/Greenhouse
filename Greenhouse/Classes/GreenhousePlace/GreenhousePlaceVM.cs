using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows;

namespace Greenhouse.Classes.GreenhousePlace
{
	public class GreenhousePlaceVM : BaseVM
	{
		public GreenhousePlaceVM(GreenhousePlace greenhousePlace, BaseDataContext parent) : base(parent)
		{
			_item = greenhousePlace;
		}

		private GreenhousePlace _item;

		#region Properties

		/// <summary>
		/// ID места
		/// </summary>
		public int? PlaceId => _item.PlaceId;

		/// <summary>
		/// ID оранжереи
		/// </summary>
		public int GreenhouseId => _item.GreenhouseId;

		/// <summary>
		/// ID растения
		/// </summary>
		[Required(ErrorMessage = "вид растения обязателен для заполнения")]
		public int? TypePlantId
		{
			get => _item.TypePlantId;
			set
			{
				SetProperty(nameof(TypePlantId), ref _item.TypePlantId, value);
			}
		}

		/// <summary>
		/// Площадь места
		/// </summary>
		[Required(ErrorMessage = "площадь места обязательна для заполнения")]
		public decimal? PlaceSquare
		{
			get => _item.PlaceSquare;
			set
			{
				SetProperty(nameof(PlaceSquare), ref _item.PlaceSquare, value);
			}
		}

		public override bool CanDelete => PlaceId != null;

		public override bool CanAdd => PlaceId == null;

		#endregion

		#region Methods to Linq

		public override bool CreateItem()
		{
			try
			{
				if (TypePlantId == null || PlaceSquare == null)
					return false;
				var dataContext = new GreenhousePlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var placeId = 0;
				var result = dataContext.CreateGreenhousePlace(ref placeId, GreenhouseId, (int)TypePlantId, (decimal)PlaceSquare);
				_item.PlaceId = placeId;
				OnPropertyChanged(nameof(PlaceId));
				return true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

		public override bool UpdateItem()
		{
			try
			{
				if (PlaceId == null || TypePlantId == null || PlaceSquare == null)
					return false;
				var dataContext = new GreenhousePlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateGreenhousePlace((int)PlaceId, GreenhouseId, (int)TypePlantId, (decimal)PlaceSquare);
				return true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

		public override bool DeleteItem()
		{
			try
			{
				if (PlaceId == null)
					return false;
				var dataContext = new GreenhousePlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteGreenhousePlace((int)PlaceId);
				return true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

		#endregion
	}
}

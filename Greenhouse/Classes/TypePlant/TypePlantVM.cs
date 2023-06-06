using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.TypePlant
{
	public class TypePlantVM : BaseVM
	{
		#region Constructor

		public TypePlantVM(int? typePlantId, BaseDataContext parent) : base(parent)
		{
			_typePlantId = typePlantId;
			LoadItem();
		}

		public TypePlantVM(TypePlant typePlant, BaseDataContext parent) : base(parent)
		{
			_item = typePlant;
		}

		#endregion

		#region Fields

		private TypePlant _item;

		private int? _typePlantId;

		#endregion

		#region Properties

		/// <summary>
		/// ID вида растения
		/// </summary>
		public int? TypePlantId => _item.TypePlantId;

		/// <summary>
		/// Название
		/// </summary>
		[StringLength(400, MinimumLength = 5, ErrorMessage = "название должно быть длиной от 5 до 400 символов")]
		public string Descr
		{
			get => _item.Descr;
			set
			{
				SetProperty(nameof(Descr), ref _item.Descr, value);
			}
		}

		/// <summary>
		/// Площадь
		/// </summary>
		[Required(ErrorMessage = "площадь обязательна для заполнения")]
		public decimal? PlantSquare
		{
			get => _item.PlantSquare;
			set
			{
				SetProperty(nameof(PlantSquare), ref _item.PlantSquare, value);
			}
		}

		/// <summary>
		/// ID вида культуры
		/// </summary>
		public int? TypeCultureId
		{
			get => _item.TypeCultureId;
			set
			{
				SetProperty(nameof(TypeCultureId), ref _item.TypeCultureId, value);
			}
		}

		/// <summary>
		/// ID группы культуры
		/// </summary>
		public int? GroupCultureId
		{
			get => _item.GroupCultureId;
			set
			{
				SetProperty(nameof(GroupCultureId), ref _item.GroupCultureId, value);
			}
		}

		/// <summary>
		/// Название вида культуры
		/// </summary>
		public string TypeCultureDescr
		{
			get => _item.TypeCultureDescr;
			set
			{
				SetProperty(nameof(TypeCultureDescr), ref _item.TypeCultureDescr, value);
			}
		}

		/// <summary>
		/// Название группы культуры
		/// </summary>
		public string GroupCultureDescr
		{
			get => _item.GroupCultureDescr;
			set
			{
				SetProperty(nameof(GroupCultureDescr), ref _item.GroupCultureDescr, value);
			}
		}

		/// <summary>
		/// Температура от
		/// </summary>
		[Required(ErrorMessage = "температура от обязательна для заполнения")]
		public decimal? TemperatureFrom
		{
			get => _item.TemperatureFrom;
			set
			{
				SetProperty(nameof(TemperatureFrom), ref _item.TemperatureFrom, value);
			}
		}

		/// <summary>
		/// Температура до
		/// </summary>
		[Required(ErrorMessage = "температура до обязательна для заполнения")]
		public decimal? TemperatureTo
		{
			get => _item.TemperatureTo;
			set
			{
				SetProperty(nameof(TemperatureTo), ref _item.TemperatureTo, value);
			}
		}

		/// <summary>
		/// Высота
		/// </summary>
		[Required(ErrorMessage = "высота обязательна для заполнения")]
		public decimal? Height
		{
			get => _item.Height;
			set
			{
				SetProperty(nameof(Height), ref _item.Height, value);
			}
		}

		/// <summary>
		/// Влажность воздуха от
		/// </summary>
		[Required(ErrorMessage = "влажность воздуха от обязательна для заполнения")]
		public decimal? AirHumidityFrom
		{
			get => _item.AirHumidityFrom;
			set
			{
				SetProperty(nameof(AirHumidityFrom), ref _item.AirHumidityFrom, value);
			}
		}

		/// <summary>
		/// Влажность воздуха до
		/// </summary>
		[Required(ErrorMessage = "влажность воздуха до обязательна для заполнения")]
		public decimal? AirHumidityTo
		{
			get => _item.AirHumidityTo;
			set
			{
				SetProperty(nameof(AirHumidityTo), ref _item.AirHumidityTo, value);
			}
		}

		public override bool CanDelete => TypePlantId != null;

		public override bool CanAdd => TypePlantId == null;

		#endregion

		#region Methods to Linq

		public override bool LoadItem()
		{
			try
			{
				if (_typePlantId == null)
				{
					_item = new TypePlant();
					return false;
				}
				var dataContext = new TypePlantContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				_item = dataContext.LoadTypePlant((int)_typePlantId).ToList().FirstOrDefault();
				if (_item == null)
				{
					_item = new TypePlant();
					_typePlantId = null;
				}
				return true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}

		public override bool CreateItem()
		{
			try
			{
				if (PlantSquare == null || TypeCultureId == null || TemperatureFrom == null || TemperatureTo == null || Height == null || AirHumidityFrom == null || AirHumidityTo == null)
					return false;
				var dataContext = new TypePlantContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var typePlantId = 0;
				var result = dataContext.CreateTypePlant(ref typePlantId, Descr, (decimal)PlantSquare, (int)TypeCultureId, (decimal)TemperatureFrom, (decimal)TemperatureTo,
					(decimal)Height, (decimal)AirHumidityFrom, (decimal)AirHumidityTo);
				_typePlantId = typePlantId;
				Refresh();
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
				if (TypePlantId == null || PlantSquare == null || TypeCultureId == null || TemperatureFrom == null || TemperatureTo == null || Height == null || AirHumidityFrom == null || AirHumidityTo == null)
					return false;
				var dataContext = new TypePlantContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateTypePlant((int)TypePlantId, Descr, (decimal)PlantSquare, (int)TypeCultureId, (decimal)TemperatureFrom, (decimal)TemperatureTo,
					(decimal)Height, (decimal)AirHumidityFrom, (decimal)AirHumidityTo);
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
				if (TypePlantId == null)
					return false;
				var dataContext = new TypePlantContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteTypePlant((int)TypePlantId);
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

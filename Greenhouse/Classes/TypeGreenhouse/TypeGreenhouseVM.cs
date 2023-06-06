using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows;

namespace Greenhouse.Classes.TypeGreenhouse
{
	public class TypeGreenhouseVM : BaseVM
	{
		public TypeGreenhouseVM(TypeGreenhouse typeGreenhouse, BaseDataContext parent) : base(parent)
		{
			_item = typeGreenhouse;
		}

		private TypeGreenhouse _item;

		#region Properties

		/// <summary>
		/// ID вида оранжереи
		/// </summary>
		public int? TypeGreenhouseId => _item.TypeGreenhouseId;

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

		public override bool CanDelete => TypeGreenhouseId != null;

		public override bool CanAdd => TypeGreenhouseId == null;

		#endregion

		#region Methods to Linq

		public override bool CreateItem()
		{
			try
			{
				var dataContext = new TypeGreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var typeGreenhouseId = 0;
				var result = dataContext.CreateTypeGreenhouse(ref typeGreenhouseId, Descr, (decimal)TemperatureFrom, (decimal)TemperatureTo);
				_item.TypeGreenhouseId = typeGreenhouseId;
				OnPropertyChanged(nameof(TypeGreenhouseId));
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
				if (TypeGreenhouseId == null)
					return false;
				var dataContext = new TypeGreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateTypeGreenhouse((int)TypeGreenhouseId, Descr, (decimal)TemperatureTo, (decimal)TemperatureFrom);
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
				if (TypeGreenhouseId == null)
					return false;
				var dataContext = new TypeGreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteTypeGreenhouse((int)TypeGreenhouseId);
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

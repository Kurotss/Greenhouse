using Greenhouse.Classes.GreenhousePlace;
using Greenhouse.Classes.TypeCulture;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.Greenhouse
{
	public class GreenhouseVM : BaseVM
	{
		#region Constructor

		public GreenhouseVM(int? greenhouseId, BaseDataContext parent) : base(parent)
		{
			_greenhouseId = greenhouseId;
			LoadItem();
		}

		public GreenhouseVM(Greenhouse greenhouse, BaseDataContext parent) : base(parent)
		{
			_item = greenhouse;
		}

		#endregion

		#region Fields

		private Greenhouse _item;

		private int? _greenhouseId;

		#endregion

		#region Properties

		/// <summary>
		/// ID оранжереи
		/// </summary>
		public int? GreenhouseId => _item.GreenhouseId;

		/// <summary>
		/// ID вида оранжереи
		/// </summary>
		[Required(ErrorMessage = "вид оранжереи обязателен для заполнения")]
		public int? TypeGreenhouseId
		{
			get => _item.TypeGreenhouseId;
			set
			{
				SetProperty(nameof(TypeGreenhouseId), ref _item.TypeGreenhouseId, value);
			}
		}

		/// <summary>
		/// Название вида оранжереи
		/// </summary>
		public string TypeGreenhouseDescr => _item.TypeGreenhouseDescr;

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
		/// Влажность воздуха
		/// </summary>
		[Required(ErrorMessage = "влажность воздуха обязательна для заполнения")]
		public decimal? AirHumidity
		{
			get => _item.AirHumidity;
			set
			{
				SetProperty(nameof(AirHumidity), ref _item.AirHumidity, value);
			}
		}

		/// <summary>
		/// Примечание
		/// </summary>
		public string Notes
		{
			get => _item.Notes;
			set
			{
				SetProperty(nameof(Notes), ref _item.Notes, value);
			}
		}

		public override bool CanDelete => GreenhouseId != null;

		public override bool CanAdd => GreenhouseId == null;

		#endregion

		#region ViewModels

		/// <summary>
		/// Список мест оранжереи
		/// </summary>
		#region TypeCultureListVM

		private GreenhousePlaceListVM _greenhousePlaceListVM;

		public GreenhousePlaceListVM GreenhousePlaceListVM
		{
			get
			{
				if (_greenhousePlaceListVM == null && GreenhouseId != null)
				{
					_greenhousePlaceListVM = new GreenhousePlaceListVM((int)GreenhouseId, this);
				}
				return _greenhousePlaceListVM;
			}
		}

		#endregion

		#endregion

		#region Methods

		protected override bool UpdateChildItems()
		{
			var result = true;
			if (_greenhousePlaceListVM != null)
				result = GreenhousePlaceListVM.SaveItems();
			return result;
		}

		public override void RefreshChildItems()
		{
			if (_greenhousePlaceListVM != null)
				GreenhousePlaceListVM.Refresh();
		}

		#endregion

		#region Methods to Linq

		public override bool LoadItem()
		{
			try
			{
				if (_greenhouseId == null)
				{
					_item = new Greenhouse();
					return false;
				}
				var dataContext = new GreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				_item = dataContext.LoadGreenhouse((int)_greenhouseId).ToList().FirstOrDefault();
				if (_item == null)
				{
					_item = new Greenhouse();
					_greenhouseId = null;
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
				if (TypeGreenhouseId == null || Height == null || AirHumidity == null)
					return false;
				var dataContext = new GreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var greenhouseId = 0;
				var result = dataContext.CreateGreenhouse(ref greenhouseId, (int)TypeGreenhouseId, (decimal)Height, (int)AirHumidity, Notes);
				_greenhouseId = greenhouseId;
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
				if (GreenhouseId == null || AirHumidity == null)
					return false;
				var dataContext = new GreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateGreenhouse((int)GreenhouseId, (decimal)AirHumidity, Notes);
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
				if (GreenhouseId == null)
					return false;
				var dataContext = new GreenhouseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteGreenhouse((int)GreenhouseId);
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

using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows;

namespace Greenhouse.Classes.TypeCulture
{
	public class TypeCultureVM : BaseVM
	{
		public TypeCultureVM(TypeCulture typeCulture, BaseDataContext parent) : base(parent)
		{
			_item = typeCulture;
		}

		private TypeCulture _item;

		#region Properties

		/// <summary>
		/// ID вида культуры
		/// </summary>
		public int? TypeCultureId => _item.TypeCultureId;

		/// <summary>
		/// ID группы культуры
		/// </summary>
		public int GroupCultureId => _item.GroupCultureId;

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

		public override bool CanDelete => TypeCultureId != null;

		public override bool CanAdd => TypeCultureId == null;

		#endregion

		#region Methods to Linq

		public override bool CreateItem()
		{
			try
			{
				var dataContext = new TypeCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var typeCultureId = 0;
				var result = dataContext.CreateTypeCulture(ref typeCultureId, GroupCultureId, Descr);
				_item.TypeCultureId = typeCultureId;
				OnPropertyChanged(nameof(TypeCultureId));
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
				if (TypeCultureId == null)
					return false;
				var dataContext = new TypeCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateTypeCulture((int)TypeCultureId, Descr);
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
				if (TypeCultureId == null)
					return false;
				var dataContext = new TypeCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteTypeCulture((int)TypeCultureId);
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

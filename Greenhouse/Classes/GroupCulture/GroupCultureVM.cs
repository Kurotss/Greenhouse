using Greenhouse.Classes.TypeCulture;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows;

namespace Greenhouse.Classes.GroupCulture
{
	public class GroupCultureVM : BaseVM
	{
		public GroupCultureVM(GroupCulture groupCulture, BaseDataContext parent) : base(parent)
		{
			_item = groupCulture;
		}

		private GroupCulture _item;

		#region Properties

		/// <summary>
		/// ID группы культуры
		/// </summary>
		public int? GroupCultureId => _item.GroupCultureId;

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

		public override bool CanDelete => GroupCultureId != null;

		public override bool CanAdd => GroupCultureId == null;

		#endregion

		#region ViewModels

		/// <summary>
		/// Список групп культур
		/// </summary>
		#region TypeCultureListVM

		private TypeCultureListVM _typeCultureListVM;

		public TypeCultureListVM TypeCultureListVM
		{
			get
			{
				if (_typeCultureListVM == null && GroupCultureId != null)
				{
					_typeCultureListVM = new TypeCultureListVM((int)GroupCultureId, this);
				}
				return _typeCultureListVM;
			}
		}

		#endregion

		#endregion

		#region Methods

		protected override bool UpdateChildItems()
		{
			var result = true;
			if (_typeCultureListVM != null)
				result = TypeCultureListVM.SaveItems();
			return result;
		}

		public override void RefreshChildItems()
		{
			if (_typeCultureListVM != null)
				TypeCultureListVM.Refresh();
		}

		#endregion

		#region Methods to Linq

		public override bool CreateItem()
		{
			try
			{
				var dataContext = new GroupCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var groupCultureId = 0;
				var result = dataContext.CreateGroupCulture(ref groupCultureId, Descr);
				_item.GroupCultureId = groupCultureId;
				OnPropertyChanged(nameof(GroupCultureId));
				OnPropertyChanged(nameof(TypeCultureListVM));
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
				if (GroupCultureId == null)
					return false;
				var dataContext = new GroupCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateGroupCulture((int)GroupCultureId, Descr);
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
				if (GroupCultureId == null)
					return false;
				var dataContext = new GroupCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteGroupCulture((int)GroupCultureId);
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.TypeCulture
{
	public class TypeCultureListVM : BaseListVM<TypeCultureVM>
	{
		public TypeCultureListVM(int groupCultureId, BaseDataContext parent) : base(parent)
		{
			_groupCultureId = groupCultureId;
			LoadItems();
		}

		private int _groupCultureId;

		#region Methods

		protected override void LoadItems()
		{
			var items = LoadTypeCultures();
			foreach (var item in items)
			{
				ItemVMs.Add(new TypeCultureVM(item, this));
			}
			base.LoadItems();
		}

		protected override void AddNewItem()
		{
			ItemVMs.Add(new TypeCultureVM(new TypeCulture() { GroupCultureId = _groupCultureId }, this));
		}

		#endregion

		#region Methods to Linq

		/// <summary>
		/// Получение списка видов культур из БД
		/// </summary>
		private List<TypeCulture> LoadTypeCultures()
		{
			try
			{
				var dataContext = new TypeCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<TypeCulture> items = dataContext.LoadTypesCulture(_groupCultureId).ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<TypeCulture>();
			}
		}

		#endregion
	}
}

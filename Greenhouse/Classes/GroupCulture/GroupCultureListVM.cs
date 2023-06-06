using Greenhouse.Classes.WindowModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

namespace Greenhouse.Classes.GroupCulture
{
	public class GroupCultureListVM : BaseListVM<GroupCultureVM>
	{
		public GroupCultureListVM(BaseDataContext parent) : base(parent)
		{
			LoadItems();
		}

		#region Delegates

		public Action AfterCloseAction;

		#endregion

		#region Methods

		protected override void LoadItems()
		{
			var items = LoadGroupCultures();
			foreach (var item in items)
			{
				ItemVMs.Add(new GroupCultureVM(item, this));
			}
			base.LoadItems();
		}

		protected override void AddNewItem()
		{
			ItemVMs.Add(new GroupCultureVM(new GroupCulture(), this));
		}

		/// <summary>
		/// Метод для открытия справочника с группами и видами культур растений
		/// </summary>
		public void OpenDictionary()
		{
			var win = new WorkWindow
			{
				Title = "Группы и виды культур",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/GroupsAndTypesCulture.xaml") },
				DataContext = new GroupsAndTypesCultureModel(),
			};
			win.Closing += AfterClose;
			win.Show();
		}

		private void AfterClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			AfterCloseAction?.Invoke();
		}

		#endregion

		#region Methods to Linq

		/// <summary>
		/// Получение списка групп культур из БД
		/// </summary>
		private List<GroupCulture> LoadGroupCultures()
		{
			try
			{
				var dataContext = new GroupCultureContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				List<GroupCulture> items = dataContext.LoadGroupsCulture().ToList();
				ItemVMs.Clear();
				return items;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return new List<GroupCulture>();
			}
		}

		#endregion
	}
}

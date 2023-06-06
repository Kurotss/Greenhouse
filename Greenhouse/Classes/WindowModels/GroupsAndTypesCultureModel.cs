using Greenhouse.Classes.GroupCulture;

namespace Greenhouse.Classes.WindowModels
{
	public class GroupsAndTypesCultureModel : BaseWindowModel
	{
		#region Constructor

		public GroupsAndTypesCultureModel()
		{
		}

		#endregion

		#region ViewModels

		/// <summary>
		/// Список групп культур
		/// </summary>
		#region GroupCultureListVM

		private GroupCultureListVM _groupCultureListVM;

		public GroupCultureListVM GroupCultureListVM => _groupCultureListVM ?? (_groupCultureListVM = new GroupCultureListVM(this));

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			GroupCultureListVM.Refresh();
		}

		protected override void Save()
		{
			GroupCultureListVM.SaveItems();
		}

		#endregion
	}
}

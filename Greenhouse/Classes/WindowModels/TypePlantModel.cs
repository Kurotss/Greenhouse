using Greenhouse.Classes.GroupCulture;
using Greenhouse.Classes.TypePlant;

namespace Greenhouse.Classes.WindowModels
{
	public class TypePlantModel : BaseWindowModel
	{
		public TypePlantModel(int? typePlantId)
		{
			_typePlantId = typePlantId;
		}

		#region Fields

		private int? _typePlantId;

		#endregion

		#region ViewModels

		#region TypePlantVM

		private TypePlantVM _typePlantVM;

		/// <summary>
		/// Вид растения
		/// </summary>
		public TypePlantVM TypePlantVM => _typePlantVM ?? (_typePlantVM = new TypePlantVM(_typePlantId, this));

		#endregion

		#region GroupCultureListVM

		private GroupCultureListVM _groupCultureListVM;

		/// <summary>
		/// Группы культур
		/// </summary>
		public GroupCultureListVM GroupCultureListVM => _groupCultureListVM ?? (_groupCultureListVM = new GroupCultureListVM(this) { IsComboBoxMode = true });

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			TypePlantVM.Refresh();
			OnPropertyChanged(nameof(TypePlantVM));
		}

		protected override void Save()
		{
			TypePlantVM.SaveItem();
			OnPropertyChanged(nameof(TypePlantVM));
		}

		#endregion
	}
}

using Greenhouse.Classes.TypeGreenhouse;

namespace Greenhouse.Classes.WindowModels
{
	public class TypesGreenhouseModel : BaseWindowModel
	{
		#region Constructor

		public TypesGreenhouseModel()
		{
		}

		#endregion

		#region ViewModels

		/// <summary>
		/// Список видов оранежерей
		/// </summary>
		#region TypeGreenhouseListVM

		private TypeGreenhouseListVM _typeGreenhouseListVM;

		public TypeGreenhouseListVM TypeGreenhouseListVM => _typeGreenhouseListVM ?? (_typeGreenhouseListVM = new TypeGreenhouseListVM(this));

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			TypeGreenhouseListVM.Refresh();
		}

		protected override void Save()
		{
			TypeGreenhouseListVM.SaveItems();
		}

		#endregion
	}
}

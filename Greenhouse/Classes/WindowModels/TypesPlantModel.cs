using Greenhouse.Classes.GroupCulture;
using Greenhouse.Classes.TypePlant;
using System.Linq;

namespace Greenhouse.Classes.WindowModels
{
	public class TypesPlantModel : BaseWindowModel
	{
		#region Constructor

		public TypesPlantModel()
		{
			CheckGroupsAndTypes();
		}

		#endregion

		#region Properties

		#region CanAddPlants

		private bool _canAddPlants = true;

		public bool CanAddPlants
		{
			get => _canAddPlants;
			set
			{
				_canAddPlants = value;
				OnPropertyChanged(nameof(CanAddPlants));
			}
		}

		#endregion

		#endregion

		#region ViewModels

		/// <summary>
		/// Список видов растений
		/// </summary>
		#region TypePlantListVM

		private TypePlantListVM _typePlantListVM;

		public TypePlantListVM TypePlantListVM => _typePlantListVM ??
			(_typePlantListVM = new TypePlantListVM(this));

		#endregion

		#region GroupCultureListVM

		private GroupCultureListVM _groupCultureListVM;

		/// <summary>
		/// Группы культур
		/// </summary>
		public GroupCultureListVM GroupCultureListVM => _groupCultureListVM ?? (_groupCultureListVM = new GroupCultureListVM(this) { IsComboBoxMode = true, AfterCloseAction = CheckGroupsAndTypes });

		#endregion

		#endregion

		#region Commands

		#region OpenGroupsAndTypeCommand

		private RelayCommand _openGroupsAndTypeCommand;

		/// <summary>
		/// Команда для открытия справочника групп и видов растений
		/// </summary>
		public RelayCommand OpenGroupsAndTypeCommand => _openGroupsAndTypeCommand ?? (_openGroupsAndTypeCommand = new RelayCommand(param => OpenGroupsAndTypes(), param => true));

		private void OpenGroupsAndTypes()
		{
			GroupCultureListVM.OpenDictionary();
		}

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			TypePlantListVM.Refresh();
		}

		protected override void Save()
		{
			TypePlantListVM.SaveItems();
		}

		protected override void ResetFilters()
		{
			TypePlantListVM.ClearFilters();
		}

		public void CheckGroupsAndTypes()
		{
			GroupCultureListVM.Refresh();
			if (GroupCultureListVM?.ItemVMs?.Count == 0)
				CanAddPlants = false;
			else if (!GroupCultureListVM.ItemVMs.Any(item => item.TypeCultureListVM?.ItemVMs?.Count > 0))
				CanAddPlants = false;
			else
				CanAddPlants = true;
		}

		#endregion
	}
}

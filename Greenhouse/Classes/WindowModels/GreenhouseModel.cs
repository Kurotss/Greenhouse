using Greenhouse.Classes.Greenhouse;
using Greenhouse.Classes.TypeGreenhouse;
using Greenhouse.Classes.TypePlant;

namespace Greenhouse.Classes.WindowModels
{
	public class GreenhouseModel : BaseWindowModel
	{
		public GreenhouseModel(int? greenhouseId)
		{
			_greenhouseId = greenhouseId;
			CheckTypesPlant();
		}

		#region Fields

		private int? _greenhouseId;

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

		#region GreenhouseVM

		private GreenhouseVM _greenhouseVM;

		/// <summary>
		/// Оранжерея
		/// </summary>
		public GreenhouseVM GreenhouseVM => _greenhouseVM ?? (_greenhouseVM = new GreenhouseVM(_greenhouseId, this));

		#endregion

		/// <summary>
		/// Список видов оранжерей
		/// </summary>
		#region TypeGreenhouseListVM

		private TypeGreenhouseListVM _typeGreenhouseListVM;

		public TypeGreenhouseListVM TypeGreenhouseListVM => _typeGreenhouseListVM ?? (_typeGreenhouseListVM = new TypeGreenhouseListVM(this) { IsComboBoxMode = true });

		#endregion

		/// <summary>
		/// Список видов растений
		/// </summary>
		#region TypePlantListVM

		private TypePlantListVM _typePlantListVM;

		public TypePlantListVM TypePlantListVM => _typePlantListVM ?? (_typePlantListVM = new TypePlantListVM(this) { IsComboBoxMode = true, AfterCloseAction = CheckTypesPlant });

		#endregion

		#endregion

		#region Commands

		#region OpenTypesCommand

		private RelayCommand _openTypesCommand;

		/// <summary>
		/// Команда для открытия справочника видов растений
		/// </summary>
		public RelayCommand OpenTypesCommand => _openTypesCommand ?? (_openTypesCommand = new RelayCommand(param => OpenTypes(), param => true));

		private void OpenTypes()
		{
			TypePlantListVM.OpenDictionary();
			TypePlantListVM.OpenDictionary();
		}

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			GreenhouseVM.Refresh();
			OnPropertyChanged(nameof(GreenhouseVM));
			OnPropertyChanged(nameof(GreenhouseVM.GreenhousePlaceListVM));
		}

		protected override void Save()
		{
			GreenhouseVM.SaveItem();
			OnPropertyChanged(nameof(GreenhouseVM));
			OnPropertyChanged(nameof(GreenhouseVM.GreenhousePlaceListVM));
		}

		public void CheckTypesPlant()
		{
			TypePlantListVM.Refresh();
			if (TypePlantListVM?.ItemVMs?.Count == 0)
				CanAddPlants = false;
			else
				CanAddPlants = true;
		}

		#endregion
	}
}

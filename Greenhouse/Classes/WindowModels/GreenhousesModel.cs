using Greenhouse.Classes.Greenhouse;
using Greenhouse.Classes.GroupCulture;
using Greenhouse.Classes.TypeCulture;
using Greenhouse.Classes.TypeGreenhouse;
using Greenhouse.Classes.TypePlant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenhouse.Classes.WindowModels
{
	public class GreenhousesModel : BaseWindowModel
	{
		#region Constructor

		public GreenhousesModel()
		{
			CheckTypes();
		}

		#endregion

		#region Properties

		#region CanAddGreenhouses

		private bool _canAddGreenhouses = true;

		public bool CanAddGreenhouses
		{
			get => _canAddGreenhouses;
			set
			{
				_canAddGreenhouses = value;
				OnPropertyChanged(nameof(CanAddGreenhouses));
			}
		}

		#endregion

		#endregion

		#region ViewModels

		/// <summary>
		/// Список оранжерей
		/// </summary>
		#region GreenhouseListVM

		private GreenhouseListVM _greenhouseListVM;

		public GreenhouseListVM GreenhouseListVM => _greenhouseListVM ?? (_greenhouseListVM = new GreenhouseListVM(this));

		#endregion

		/// <summary>
		/// Список видов оранжерей
		/// </summary>
		#region TypeGreenhouseListVM

		private TypeGreenhouseListVM _typeGreenhouseListVM;

		public TypeGreenhouseListVM TypeGreenhouseListVM => _typeGreenhouseListVM ?? (_typeGreenhouseListVM = new TypeGreenhouseListVM(this) { IsComboBoxMode = true, AfterCloseAction = CheckTypes });

		#endregion

		#endregion

		#region Commands

		#region OpenTypesCommand

		private RelayCommand _openTypesCommand;

		/// <summary>
		/// Команда для открытия справочника видов оранжерей
		/// </summary>
		public RelayCommand OpenTypesCommand => _openTypesCommand ?? (_openTypesCommand = new RelayCommand(param => OpenTypes(), param => true));

		private void OpenTypes()
		{
			TypeGreenhouseListVM.OpenDictionary();
		}

		#endregion

		#endregion

		#region Methods

		protected override void Refresh()
		{
			GreenhouseListVM.Refresh();
		}

		protected override void Save()
		{
			GreenhouseListVM.SaveItems();
		}

		protected override void ResetFilters()
		{
			GreenhouseListVM.ClearFilters();
		}

		public void CheckTypes()
		{
			TypeGreenhouseListVM.Refresh();
			if (TypeGreenhouseListVM?.ItemVMs?.Count == 0)
				CanAddGreenhouses = false;
			else
				CanAddGreenhouses = true;
		}

		#endregion
	}
}

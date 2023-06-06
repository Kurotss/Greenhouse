using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;

namespace Greenhouse.Classes
{
	public class BaseVM : BaseDataContext
	{
		#region Constructor

		public BaseVM(BaseDataContext parent)
		{
			Parent = parent;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Возможно ли удалить
		/// </summary>
		public virtual bool CanDelete { get; set; }

		/// <summary>
		/// Возможно ли доабвить
		/// </summary>
		public virtual bool CanAdd { get; set; }

		/// <summary>
		/// Возможно ли сохранить
		/// </summary>
		public bool CanSave { get; set; }

		/// <summary>
		/// Родитель
		/// </summary>
		private BaseDataContext Parent { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Обновление значения свойства
		/// </summary>
		protected void SetProperty<T>(string property, ref T field, T value)
		{
			field = value;
			OnPropertyChanged(nameof(property));
			CanSave = true;
		}

		/// <summary>
		/// Сохранение значений
		/// </summary>
		public bool SaveItem()
		{
			if (CanSave)
			{
				var context = new ValidationContext(this);
				var results = new List<ValidationResult>();
				if (!Validator.TryValidateObject(this, context, results, true))
				{
					var sb = new StringBuilder();
					foreach (var error in results)
					{
						sb.AppendLine("- " + error.ErrorMessage);
					}
					MessageBox.Show("При сохранении результатов возникли следующие ошибки:" + Environment.NewLine + $"{sb}", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
					return false;
				}
				if (CanAdd ? CreateItem() : UpdateItem())
					CanSave = false;
				else
					return false;
				MessageBox.Show("Сохранение прошло успешно.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			if (!UpdateChildItems())
				return false;
			return true;
				
		}

		/// <summary>
		/// Загрузка из БД
		/// </summary>
		public virtual bool LoadItem()
		{
			throw new Exception("Загрузка элемента - нет реализации.");
		}

		/// <summary>
		/// Добавление в БД
		/// </summary>
		public virtual bool CreateItem()
		{
			throw new Exception("Добавление элемента - нет реализации.");
		}

		/// <summary>
		/// Изменение в БД
		/// </summary>
		public virtual bool UpdateItem()
		{
			throw new Exception("Изменение элемента - нет реализации.");
		}

		/// <summary>
		/// Удаление в БД
		/// </summary>
		public virtual bool DeleteItem()
		{
			throw new Exception("Удаление элемента - нет реализации.");
		}

		/// <summary>
		/// Сохранение дочерних элементов в БД
		/// </summary>
		protected virtual bool UpdateChildItems()
		{
			return true;
		}

		/// <summary>
		/// Обновление дочерних элементов
		/// </summary>
		public virtual void RefreshChildItems()
		{
		}

		/// <summary>
		/// Обновление элемента из БД
		/// </summary>
		public void Refresh()
		{
			LoadItem();
			RefreshChildItems();
		}

		#endregion
	}
}

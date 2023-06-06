using System.Data.Linq.Mapping;

namespace Greenhouse.Classes.TypeCulture
{
	public class TypeCulture
	{
		/// <summary>
		/// ID вида культуры
		/// </summary>
		[Column(Name = "type_culture_id")]
		public int? TypeCultureId;

		/// <summary>
		/// ID группы культуры
		/// </summary>
		[Column(Name = "group_culture_id")]
		public int GroupCultureId;

		/// <summary>
		/// Название
		/// </summary>
		[Column(Name = "descr")]
		public string Descr;
	}
}

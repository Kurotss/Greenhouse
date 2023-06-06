using System.Data.Linq.Mapping;

namespace Greenhouse.Classes.GroupCulture
{
	public class GroupCulture
	{
		/// <summary>
		/// ID группы культуры
		/// </summary>
		[Column(Name = "group_culture_id")]
		public int? GroupCultureId;

		/// <summary>
		/// Название
		/// </summary>
		[Column(Name = "descr")]
		public string Descr;
	}
}

using System.Data.Linq.Mapping;

namespace Greenhouse.Classes.Greenhouse
{
	public class Greenhouse
	{
		/// <summary>
		/// ID оранжереи
		/// </summary>
		[Column(Name = "greenhouse_id")]
		public int? GreenhouseId;

		/// <summary>
		/// ID вида оранжереи
		/// </summary>
		[Column(Name = "type_greenhouse_id")]
		public int? TypeGreenhouseId;

		/// <summary>
		/// Название вида оранжереи
		/// </summary>
		[Column(Name = "type_greenhouse_descr")]
		public string TypeGreenhouseDescr;

		/// <summary>
		/// Высота
		/// </summary>
		[Column(Name = "height")]
		public decimal? Height;

		/// <summary>
		/// Влажность воздуха
		/// </summary>
		[Column(Name = "air_humidity")]
		public decimal? AirHumidity;

		/// <summary>
		/// Примечание
		/// </summary>
		[Column(Name = "notes")]
		public string Notes;
	}
}

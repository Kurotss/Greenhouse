using System.Data.Linq.Mapping;

namespace Greenhouse.Classes.TypeGreenhouse
{
	public class TypeGreenhouse
	{
		/// <summary>
		/// ID вида оранжереи
		/// </summary>
		[Column(Name = "type_greenhouse_id")]
		public int? TypeGreenhouseId;

		/// <summary>
		/// Название
		/// </summary>
		[Column(Name = "descr")]
		public string Descr;

		/// <summary>
		/// Температура от
		/// </summary>
		[Column(Name = "temperature_from")]
		public decimal? TemperatureFrom;

		/// <summary>
		/// Температура до
		/// </summary>
		[Column(Name = "temperature_to")]
		public decimal? TemperatureTo;
	}
}

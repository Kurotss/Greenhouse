using System.Data.Linq.Mapping;

namespace Greenhouse.Classes.TypePlant
{
	public class TypePlant
	{
		/// <summary>
		/// ID вида растения
		/// </summary>
		[Column(Name = "type_plant_id")]
		public int? TypePlantId;

		/// <summary>
		/// Название
		/// </summary>
		[Column(Name = "descr")]
		public string Descr;

		/// <summary>
		/// Площадь
		/// </summary>
		[Column(Name = "plant_square")]
		public decimal? PlantSquare;

		/// <summary>
		/// ID вида культуры
		/// </summary>
		[Column(Name = "type_culture_id")]
		public int? TypeCultureId;

		/// <summary>
		/// ID группы культуры
		/// </summary>
		[Column(Name = "group_culture_id")]
		public int? GroupCultureId;

		/// <summary>
		/// Название вида культуры
		/// </summary>
		[Column(Name = "type_culture_descr")]
		public string TypeCultureDescr;

		/// <summary>
		/// Название группы культуры
		/// </summary>
		[Column(Name = "group_culture_descr")]
		public string GroupCultureDescr;

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

		/// <summary>
		/// Высота
		/// </summary>
		[Column(Name = "height")]
		public decimal? Height;

		/// <summary>
		/// Влажность воздуха от
		/// </summary>
		[Column(Name = "air_humidity_from")]
		public decimal? AirHumidityFrom;

		/// <summary>
		/// Влажность воздуха до
		/// </summary>
		[Column(Name = "air_humidity_to")]
		public decimal? AirHumidityTo;
	}
}

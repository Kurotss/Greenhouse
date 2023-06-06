using System.Data.Linq.Mapping;


namespace Greenhouse.Classes.GreenhousePlace
{
	public class GreenhousePlace
	{
		/// <summary>
		/// ID места
		/// </summary>
		[Column(Name = "place_id")]
		public int? PlaceId;

		/// <summary>
		/// ID оранжереи
		/// </summary>
		[Column(Name = "greenhouse_id")]
		public int GreenhouseId;

		/// <summary>
		/// ID растения
		/// </summary>
		[Column(Name = "type_plant_id")]
		public int? TypePlantId;

		/// <summary>
		/// Площадь места
		/// </summary>
		[Column(Name = "place_square")]
		public decimal? PlaceSquare;
	}
}

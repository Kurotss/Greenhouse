using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.GreenhousePlace
{
	public class GreenhousePlaceContext : DataContext
	{
		public GreenhousePlaceContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка списка мест оранжереи
		/// </summary>
		[Function(Name = "LoadGreenhousePlaces")]
		public ISingleResult<GreenhousePlace> LoadGreenhousePlaces
		(
			[Parameter(Name = "greenhouse_id")] int greenhouseId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), greenhouseId);
			return (ISingleResult<GreenhousePlace>)result.ReturnValue;
		}

		/// <summary>
		/// Создание места оранжереи
		/// </summary>
		[Function(Name = "CreateGreenhousePlace")]
		public int CreateGreenhousePlace
		(
			[Parameter(Name = "place_id")] ref int placeId,
			[Parameter(Name = "greenhouse_id")] int greenhouseId,
			[Parameter(Name = "type_plant_id")] int typePlantId,
			[Parameter(Name = "place_square")] decimal placeSquare
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), placeId, greenhouseId, typePlantId, placeSquare);
			placeId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление места оранжереи
		/// </summary>
		[Function(Name = "UpdateGreenhousePlace")]
		public int UpdateGreenhousePlace
		(
			[Parameter(Name = "place_id")] int placeId,
			[Parameter(Name = "greenhouse_id")] int greenhouseId,
			[Parameter(Name = "type_plant_id")] int typePlantId,
			[Parameter(Name = "place_square")] decimal placeSquare
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), placeId, greenhouseId, typePlantId, placeSquare);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление места оранжереи
		/// </summary>
		[Function(Name = "DeleteGreenhousePlace")]
		public int DeleteGreenhousePlace
		(
			[Parameter(Name = "place_id")] int placeId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), placeId);
			return (int)result.ReturnValue;
		}
	}
}

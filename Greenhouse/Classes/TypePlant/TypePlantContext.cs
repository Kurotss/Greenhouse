using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.TypePlant
{
	public class TypePlantContext : DataContext
	{
		public TypePlantContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка вида растения
		/// </summary>
		[Function(Name = "LoadTypePlant")]
		public ISingleResult<TypePlant> LoadTypePlant
		(
			[Parameter(Name = "type_plant_id")] int typePlantId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typePlantId);
			return (ISingleResult<TypePlant>)result.ReturnValue;
		}

		/// <summary>
		/// Загрузка списка видов растений
		/// </summary>
		[Function(Name = "LoadTypesPlant")]
		public ISingleResult<TypePlant> LoadTypesPlant
		(
			[Parameter(Name = "group_culture_id")] int? groupCultureId,
			[Parameter(Name = "type_culture_id")] int? typeCultureId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), groupCultureId, typeCultureId);
			return (ISingleResult<TypePlant>)result.ReturnValue;
		}

		/// <summary>
		/// Создание вида растения
		/// </summary>
		[Function(Name = "CreateTypePlant")]
		public int CreateTypePlant
		(
			[Parameter(Name = "type_plant_id")] ref int typePlantId,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "plant_square")] decimal plantSquare,
			[Parameter(Name = "type_culture_id")] int typeCultureId,
			[Parameter(Name = "temperature_from")] decimal temperatureFrom,
			[Parameter(Name = "temperature_to")] decimal temperatureTo,
			[Parameter(Name = "height")] decimal height,
			[Parameter(Name = "air_humidity_from")] decimal airHumidityFrom,
			[Parameter(Name = "air_humidity_to")] decimal airHumidityTo
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typePlantId, descr, plantSquare, typeCultureId, temperatureFrom, temperatureTo,
				height, airHumidityFrom, airHumidityTo);
			typePlantId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление вида растения
		/// </summary>
		[Function(Name = "UpdateTypePlant")]
		public int UpdateTypePlant
		(
			[Parameter(Name = "type_plant_id")] int typePlantId,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "plant_square")] decimal plantSquare,
			[Parameter(Name = "type_culture_id")] int typeCultureId,
			[Parameter(Name = "temperature_from")] decimal temperatureFrom,
			[Parameter(Name = "temperature_to")] decimal temperatureTo,
			[Parameter(Name = "height")] decimal height,
			[Parameter(Name = "air_humidity_from")] decimal airHumidityFrom,
			[Parameter(Name = "air_humidity_to")] decimal airHumidityTo
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typePlantId, descr, plantSquare, typeCultureId, temperatureFrom, temperatureTo,
				height, airHumidityFrom, airHumidityTo);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление вида растения
		/// </summary>
		[Function(Name = "DeleteTypePlant")]
		public int DeleteTypePlant
		(
			[Parameter(Name = "type_plant_id")] int typePlantId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typePlantId);
			return (int)result.ReturnValue;
		}
	}
}

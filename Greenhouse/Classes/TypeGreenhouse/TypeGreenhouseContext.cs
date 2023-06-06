using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.TypeGreenhouse
{
	public class TypeGreenhouseContext : DataContext
	{
		public TypeGreenhouseContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка списка видов оранжерей
		/// </summary>
		[Function(Name = "LoadTypesGreenhouse")]
		public ISingleResult<TypeGreenhouse> LoadTypesGreenhouse
		(
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod());
			return (ISingleResult<TypeGreenhouse>)result.ReturnValue;
		}

		/// <summary>
		/// Создание вида оранжереи
		/// </summary>
		[Function(Name = "CreateTypeGreenhouse")]
		public int CreateTypeGreenhouse
		(
			[Parameter(Name = "type_greenhouse_id")] ref int typeGreenhouseId,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "temperature_from")] decimal temperatureFrom,
			[Parameter(Name = "temperature_to")] decimal temperatureTo
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeGreenhouseId, descr, temperatureFrom, temperatureTo);
			typeGreenhouseId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление вида оранжереи
		/// </summary>
		[Function(Name = "UpdateTypeGreenhouse")]
		public int UpdateTypeGreenhouse
		(
			[Parameter(Name = "type_greenhouse_id")] int typeGreenhouseId,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "temperature_from")] decimal temperatureFrom,
			[Parameter(Name = "temperature_to")] decimal temperatureTo
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeGreenhouseId, descr, temperatureFrom, temperatureTo);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление вида оранжереи
		/// </summary>
		[Function(Name = "DeleteTypeGreenhouse")]
		public int DeleteTypeGreenhouse
		(
			[Parameter(Name = "type_greenhouse_id")] int typeGreenhouseId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeGreenhouseId);
			return (int)result.ReturnValue;
		}
	}
}

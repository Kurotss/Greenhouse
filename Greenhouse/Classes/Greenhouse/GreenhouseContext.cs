using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.Greenhouse
{
	public class GreenhouseContext : DataContext
	{
		public GreenhouseContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка оранжереи
		/// </summary>
		[Function(Name = "LoadGreenhouse")]
		public ISingleResult<Greenhouse> LoadGreenhouse
		(
			[Parameter(Name = "greenhouse_id")] int greenhouseId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), greenhouseId);
			return (ISingleResult<Greenhouse>)result.ReturnValue;
		}

		/// <summary>
		/// Загрузка списка оранжерей
		/// </summary>
		[Function(Name = "LoadGreenhouses")]
		public ISingleResult<Greenhouse> LoadGreenhouses
		(
			[Parameter(Name = "type_greenhouse_id")] int? typeGreenhouseId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeGreenhouseId);
			return (ISingleResult<Greenhouse>)result.ReturnValue;
		}

		/// <summary>
		/// Создание оранжереи
		/// </summary>
		[Function(Name = "CreateGreenhouse")]
		public int CreateGreenhouse
		(
			[Parameter(Name = "greenhouse_id")] ref int greenhouseId,
			[Parameter(Name = "type_greenhouse_id")] int typeGreenhouseId,
			[Parameter(Name = "height")] decimal height,
			[Parameter(Name = "air_humidity")] decimal airHumidity,
			[Parameter(Name = "notes")] string notes
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), greenhouseId, typeGreenhouseId, height, airHumidity, notes);
			greenhouseId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление оранжереи
		/// </summary>
		[Function(Name = "UpdateGreenhouse")]
		public int UpdateGreenhouse
		(
			[Parameter(Name = "greenhouse_id")] int greenhouseId,
			[Parameter(Name = "air_humidity")] decimal airHumidity,
			[Parameter(Name = "notes")] string notes
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), greenhouseId, airHumidity, notes);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление оранжереи
		/// </summary>
		[Function(Name = "DeleteGreenhouse")]
		public int DeleteGreenhouse
		(
			[Parameter(Name = "greenhouse_id")] int greenhouseId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), greenhouseId);
			return (int)result.ReturnValue;
		}
	}
}

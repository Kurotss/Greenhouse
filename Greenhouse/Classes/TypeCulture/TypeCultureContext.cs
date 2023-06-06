using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.TypeCulture
{
	public class TypeCultureContext : DataContext
	{
		public TypeCultureContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка списка видов культур
		/// </summary>
		[Function(Name = "LoadTypesCulture")]
		public ISingleResult<TypeCulture> LoadTypesCulture
		(
			[Parameter(Name = "group_culture_id")] int groupCultureId
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), groupCultureId);
			return (ISingleResult<TypeCulture>)result.ReturnValue;
		}

		/// <summary>
		/// Создание вида культуры
		/// </summary>
		[Function(Name = "CreateTypeCulture")]
		public int CreateTypeCulture
		(
			[Parameter(Name = "type_culture_id")] ref int typeCultureId,
			[Parameter(Name = "group_culture_id")] int groupCultureId,
			[Parameter(Name = "descr")] string descr
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeCultureId, groupCultureId, descr);
			typeCultureId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление вида культуры
		/// </summary>
		[Function(Name = "UpdateTypeCulture")]
		public int UpdateTypeCulture
		(
			[Parameter(Name = "type_culture_id")] int typeCultureId,
			[Parameter(Name = "descr")] string descr
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeCultureId, descr);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление вида культуры
		/// </summary>
		[Function(Name = "DeleteTypeCulture")]
		public int DeleteTypeCulture
		(
			[Parameter(Name = "type_culture_id")] int typeCultureId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), typeCultureId);
			return (int)result.ReturnValue;
		}
	}
}

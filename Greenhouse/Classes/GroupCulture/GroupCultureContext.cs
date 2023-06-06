using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Greenhouse.Classes.GroupCulture
{
	public class GroupCultureContext : DataContext
	{
		public GroupCultureContext(string connection) : base(connection)
		{
		}

		/// <summary>
		/// Загрузка списка групп культур
		/// </summary>
		[Function(Name = "LoadGroupsCulture")]
		public ISingleResult<GroupCulture> LoadGroupsCulture
		(
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod());
			return (ISingleResult<GroupCulture>)result.ReturnValue;
		}

		/// <summary>
		/// Создание группы культуры
		/// </summary>
		[Function(Name = "CreateGroupCulture")]
		public int CreateGroupCulture
		(
			[Parameter(Name = "group_culture_id")] ref int groupCultureId,
			[Parameter(Name = "descr")] string descr
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), groupCultureId, descr);
			groupCultureId = (int)result.GetParameterValue(0);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Обновление группы культуры
		/// </summary>
		[Function(Name = "UpdateGroupCulture")]
		public int UpdateGroupCulture
		(
			[Parameter(Name = "group_culture_id")] int groupCultureId,
			[Parameter(Name = "descr")] string descr
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), groupCultureId, descr);
			return (int)result.ReturnValue;
		}

		/// <summary>
		/// Удаление группу культуры
		/// </summary>
		[Function(Name = "DeleteGroupCulture")]
		public int DeleteGroupCulture
		(
			[Parameter(Name = "group_culture_id")] int groupCultureId
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), groupCultureId);
			return (int)result.ReturnValue;
		}
	}
}

using Gulliver.DoCol.UtilityServices;

namespace Gulliver.DoCol.Library.Sercurity
{
	public static class Permission
	{
		/// <summary>
		/// Gets the permision.
		/// </summary>
		/// <returns></returns>
		public static string GetPermision()
		{
			return CacheUtil.GetCache<string>( "GLV_SYS_PERMISION" );
		}

		/// <summary>
		/// Has Permission
		/// </summary>
		/// <returns></returns>
		public static bool HasPermision( string permision )
		{
			string getPermissionValue = GetPermision();

			if (string.IsNullOrEmpty( permision ))
				return false;

			if (string.IsNullOrEmpty( getPermissionValue ))
				return false;

			return getPermissionValue.Contains( permision );
		}
	}
}
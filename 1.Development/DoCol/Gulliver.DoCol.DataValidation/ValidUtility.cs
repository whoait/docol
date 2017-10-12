namespace Gulliver.DoCol.DataValidation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;

	public class ValidUtility
	{
		#region Type

		internal class Type
		{
			public const string EXFullSize = "exfullsize";
			public const string EXHalfSize = "exhalfsize";
			public const string EXAlphaNumberic = "exalphanumberic";
			public const string EXCompareDateForm = "other";
			public const string EXEmail = "exemail";
			public const string EXNumberic = "exnumberic";
			public const string EXRange = "exrange";
			public const string EXRegex = "exregex";
			public const string EXRequired = "exrequired";
			public const string EXStringLength = "exstringlength";
			public const string EXSDate = "exdate";
			public const string EXCompare = "excompare";
		}

		#endregion

		#region error infomation

        [Serializable]
		public class ErrorInfoItem
		{
			public string Key { get; internal set; }
			public string Type { get; internal set; }
			public string Message { get; internal set; }
		}

		private List<ErrorInfoItem> errorInfo = null;

		public List<ErrorInfoItem> ErrorInfo
		{
			get
			{
				if (errorInfo == null)
				{
					if (HttpContext.Current.Session["GLV_SYS_DATA_VALID_ERROR"] == null)
					{
						HttpContext.Current.Session["GLV_SYS_DATA_VALID_ERROR"] = new List<ErrorInfoItem>();
					}
					errorInfo = (List<ErrorInfoItem>)HttpContext.Current.Session["GLV_SYS_DATA_VALID_ERROR"];
				}
				return errorInfo;
			}
		}

		public static void RemoveErrorInfo()
		{
			HttpContext.Current.Session["GLV_SYS_DATA_VALID_ERROR"] = null;
		}

		public static string ErrorInfoString
		{
			get
			{
				List<ErrorInfoItem> error = (List<ErrorInfoItem>)HttpContext.Current.Session["GLV_SYS_DATA_VALID_ERROR"];
				if (error == null)
				{
					return null;
				}
				StringBuilder result = new StringBuilder();

				foreach (ErrorInfoItem item in error)
				{
					if (result.Length > 0)
					{
						result.Append( "|" );
					}
					result.Append( item.Key + ":" + item.Type );
				}

				return result.ToString();
			}
		}

		#endregion

		internal void SaveError( string key, string type, string message )
		{
			ErrorInfo.Add( new ErrorInfoItem { Key = key, Type = type, Message = message } );
		}
	}
}
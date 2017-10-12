namespace Gulliver.DoCol.Constants
{
	public enum ReturnCode
	{
		Success = 0,
		Failure = -1,
	}

	public class TextKey
	{
		public const string PRE_EXCEPTION_MSG = "GLV_SYS_Exception_Message";
		public const string COOKIE_CARUPGRADE = "COOKIE_CARUPGRADE";
	}

	public class ExceptionKey
	{
		public const string GLV_CMN_NotFoundException = "GLV_CMN_NotFoundException";
		public const string GLV_CMN_InvalidAccessException = "GLV_SYS_InvalidAccessException";
		public const string GLV_CMN_NotAuthenticated = "GLV_SYS_NotAuthenticated";
		public const string GLV_CMN_DBException = "GLV_SYS_DBException";
		public const string GLV_CMN_LoginException = "GLV_SYS_LoginException";
	}

	public class DCW003Constant
	{
		public const int DEFAULT_PAGE_INDEX = 1;
		public const int DEFAULT_PAGE_SIZE = 300;
		public const string DEF_RD0010_FILE = @"\DoCol\UAT\RD0020.fcp";
		public const string SHOHIN_TYPE_DN = "101";
		public const string SHOHIN_TYPE_AA = "102";
	}

	public class NUMBER
	{
		public const string NUM_0 = "0";
		public const string NUM_1 = "1";
	}
}
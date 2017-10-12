//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.Constants;
using Gulliver.DoCol.UtilityServices;
using System.Web.Mvc;

namespace Gulliver.DoCol.Areas.Common.Controllers
{
	public class ErrorController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Error";
			ViewBag.Message = "Error.";
			return View();
		}

		public ActionResult SqlException()
		{
			ViewBag.Title = "System Error";
			ViewBag.StatusCode = "500";
			ViewBag.Message = "システム異常が発生しました。";
			return View( "Index" );
		}

		public ActionResult SysException()
		{
			ViewBag.Title = "System Error";
			ViewBag.StatusCode = "500";
			ViewBag.Message = "システム異常が発生しました。";
			return View( "Index" );
		}

		public ActionResult NotPermission()
		{
			ViewBag.Title = "Forbidden";
			ViewBag.StatusCode = "403";
			ViewBag.Message = "権限がありません。管理者に問い合わせてください。";
			return View( "Index" );
		}

		public ActionResult NotAuthenticated()
		{
			ViewBag.Title = "NotAuthenticated";
			ViewBag.Message = "NotAuthenticated";
			return View( "Index" );
		}

		public ActionResult NotFound( string message )
		{
			ViewBag.Title = "Not Found";
			ViewBag.StatusCode = "404";
			ViewBag.Message = string.IsNullOrEmpty( message ) ? string.Empty : message.Replace( ExceptionKey.GLV_CMN_NotFoundException, "" );
			return View( "Index" );
		}

		public ActionResult Login()
		{
			ViewBag.Title = "Time out";
            ViewBag.Message = "システムがタイムアウトされました。";
			return View( "Index" );
		}

		public ActionResult LoginFail()
		{
			ViewBag.Title = "Login";
            ViewBag.Message = "ログインに失敗しました。";
			return View( "Index" );
		}

        public ActionResult LogOut()
        {
            ViewBag.Title = "Logout";
            ViewBag.Message = "システムがログアウトされました。";
            return View("Index");
        }
	}
}
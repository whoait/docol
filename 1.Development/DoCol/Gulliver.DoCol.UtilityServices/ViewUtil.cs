//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Gulliver.DoCol.UtilityServices
{
	public class ViewUtil
	{
		private string actionName = string.Empty;
		private string controllerName = string.Empty;
		private string updateTargetId = string.Empty;
		private byte? currentSortDirection = null;
		private string currentSortItem = string.Empty;
		private object ajaxHelper = null;

		public ViewUtil( object ajaxHelper
							, string actionName
							, string controllerName
							, string updateTargetId
							, byte? currentSortDirection
							, string currentSortItem )
		{
			this.ajaxHelper = ajaxHelper;
			this.actionName = actionName;
			this.controllerName = controllerName;
			this.updateTargetId = updateTargetId;
			this.currentSortDirection = currentSortDirection;
			this.currentSortItem = currentSortItem;
		}

		public MvcHtmlString ActionLinkSortHeader( string linkText
															, string colName
															, bool automaticAddSortSymbol = true
															, object htmlAttributes = null )
		{
			return ActionLinkSortHeader( (AjaxHelper)this.ajaxHelper
											, linkText
											, colName
											, this.actionName
											, this.controllerName
											, this.updateTargetId
											, this.currentSortDirection
											, this.currentSortItem
											, htmlAttributes
											, automaticAddSortSymbol );
		}

		public MvcHtmlString ActionLinkSortHeader( AjaxHelper ajaxHelper
													, string linkText
													, string colName
													, string actionName
													, string controllerName
													, string updateTargetId
													, byte? currentSortDirection
													, string currentSortItem
													, object htmlAttributes = null
													, bool automaticAddSortSymbol = true )
		{
			string currentSortSymbol;
			byte invertSortDirection;
			if (currentSortItem != colName || currentSortDirection == null)
			{
				currentSortSymbol = string.Empty;
				invertSortDirection = 0;
			}
			else
			{
				if (currentSortDirection == 0)
				{
					currentSortSymbol = "▲";
					invertSortDirection = 1;
				}
				else
				{
					currentSortSymbol = "▼";
					invertSortDirection = 0;
				}
			}

			if (!automaticAddSortSymbol)
			{
				currentSortSymbol = string.Empty;
			}

			if (String.IsNullOrEmpty( controllerName ))
			{
				return MvcHtmlString.Create( ajaxHelper.ActionLink( linkText
						, actionName
						, new { SortItem = colName, SortDirection = invertSortDirection, screenroute = UriUtility.RouteValue }
						, new AjaxOptions { HttpMethod = "Post", UpdateTargetId = updateTargetId }
						, htmlAttributes ).ToHtmlString() + currentSortSymbol );
			}
			else
			{
				return MvcHtmlString.Create( ajaxHelper.ActionLink( linkText
						, actionName
						, controllerName
						, new { SortItem = colName, SortDirection = invertSortDirection, screenroute = UriUtility.RouteValue }
						, new AjaxOptions { HttpMethod = "Post", UpdateTargetId = updateTargetId }
						, htmlAttributes ).ToHtmlString() + currentSortSymbol );
			}
		}
	}
}
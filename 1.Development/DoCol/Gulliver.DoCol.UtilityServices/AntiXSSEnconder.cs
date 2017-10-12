//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

namespace Gulliver.DoCol.UtilityServices
{
	using Microsoft.Security.Application;
	using System.IO;
	using System.Web.Util;

	/// <summary>
	/// Summary description for AntiXss
	/// </summary>
	public class AntiXssEncoder : HttpEncoder
	{
		/// <summary>
		/// AntiXssEncoder contructor
		/// </summary>
		public AntiXssEncoder()
		{
		}

		/// <summary>
		/// HtmlEncode encode html
		/// </summary>
		/// <param name="value">source</param>
		/// <param name="output">output</param>
		protected override void HtmlEncode( string value, TextWriter output )
		{
			output.Write( Encoder.HtmlEncode( value ) );
		}

		/// <summary>
		/// HtmlAttributeEncode
		/// </summary>
		/// <param name="value">source</param>
		/// <param name="output">output</param>
		protected override void HtmlAttributeEncode( string value, TextWriter output )
		{
			output.Write( Encoder.HtmlAttributeEncode( value ) );
		}

		/// <summary>
		/// HtmlDecode
		/// </summary>
		/// <param name="value">source</param>
		/// <param name="output">output</param>
		protected override void HtmlDecode( string value, TextWriter output )
		{
			base.HtmlDecode( value, output );
		}
	}
}
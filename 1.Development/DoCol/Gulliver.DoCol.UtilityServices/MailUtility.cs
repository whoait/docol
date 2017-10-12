//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using Gulliver.DoCol.Entities;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace Gulliver.DoCol.UtilityServices
{
	/// <summary>
	///
	/// </summary>
	public class MailUtility
	{
		//#region Const

		//public const int STATUS_SUCCESS = 0;
		//public const int STATUS_INPUT_ERROR = 1;
		//public const int STATUS_SEND_ERROR = 2;
		//public const int STATUS_TEMPLATE_ERROR = 3;

		//#endregion

		#region Varibles

		/// <summary>
		/// The template identifier
		/// </summary>
		private string templatePath = string.Empty;

		/// <summary>
		/// The body replacement
		/// </summary>
		private List<ReplacementModel> bodyReplacement = new List<ReplacementModel>();

		/// <summary>
		/// The s mt p_ user name
		/// </summary>
		private string sMTP_UserName = "AKIAICEQF6VUX2QI5Y2A";

		/// <summary>
		/// The s mt p_ password
		/// </summary>
		private string sMTP_Password = "Asd9K9POfI4J+5JhDwnjGEJrkYpv+BcWSUgwPEXuEpQU";

		/// <summary>
		/// The host
		/// </summary>
		private string host = "email-smtp.us-east-1.amazonaws.com";

		/// <summary>
		/// The port
		/// </summary>
		private int port = 587;

		/// <summary>
		/// From
		/// </summary>
		private MailAddress from = new MailAddress( "noreply_rikusosys@glv.co.jp" );

		/// <summary>
		/// To
		/// </summary>
		private List<MailAddress> to = null;

		/// <summary>
		/// The c c
		/// </summary>
		private List<MailAddress> cC = null;

		/// <summary>
		/// The b cc
		/// </summary>
		private List<MailAddress> bCC = null;

		/// <summary>
		/// The is HTML mode
		/// </summary>
		private bool isHTMLMode = true;

		/// <summary>
		/// The subject
		/// </summary>
		private string subject = string.Empty;

		/// <summary>
		/// The mail body
		/// </summary>
		private string mailBody = string.Empty;

		//private int status = -1;

		#endregion Varibles

		#region Properties

		/// <summary>
		/// Sets the template identifier.
		/// </summary>
		/// <value>
		/// The template identifier.
		/// </value>
		public string TemplatePath
		{
			set
			{
				this.templatePath = value;
			}

			private get
			{
				return this.templatePath;
			}
		}

		/// <summary>
		/// Sets the body replacement.
		/// </summary>
		/// <value>
		/// The body replacement.
		/// </value>
		public List<ReplacementModel> BodyReplacement
		{
			set
			{
				this.bodyReplacement = value;
			}

			private get
			{
				return this.bodyReplacement;
			}
		}

		/// <summary>
		/// Sets to.
		/// </summary>
		/// <value>
		/// To.
		/// </value>
		public List<MailAddress> To
		{
			set
			{
				this.to = value;
			}
		}

		/// <summary>
		/// Sets the cc.
		/// </summary>
		/// <value>
		/// The cc.
		/// </value>
		public List<MailAddress> CC
		{
			set
			{
				this.cC = value;
			}
		}

		/// <summary>
		/// Sets the BCC.
		/// </summary>
		/// <value>
		/// The BCC.
		/// </value>
		public List<MailAddress> BCC
		{
			set
			{
				this.bCC = value;
			}
		}

		/// <summary>
		/// Sets a value indicating whether [is HTML mode].
		/// </summary>
		/// <value>
		///   <c>true</c> if [is HTML mode]; otherwise, <c>false</c>.
		/// </value>
		public bool IsHTMLMode
		{
			set
			{
				this.isHTMLMode = value;
			}
		}

		/// <summary>
		/// The subject
		/// </summary>
		public string Subject
		{
			set
			{
				this.subject = value;
			}
		}

		#endregion Properties

		#region Private methods

		/// <summary>
		/// Determines whether [is valid input].
		/// </summary>
		/// <returns></returns>
		private bool isValidInput()
		{
			if (string.IsNullOrEmpty( this.sMTP_UserName ))
			{
				return false;
			}

			if (string.IsNullOrEmpty( this.sMTP_Password ))
			{
				return false;
			}

			if (string.IsNullOrEmpty( this.host ))
			{
				return false;
			}

			if (this.to == null || this.to.Count < 1)
			{
				return false;
			}

			if (string.IsNullOrEmpty( this.templatePath ))
			{
				return false;
			}

			if (string.IsNullOrEmpty( this.subject ))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Messages the specified mail to.
		/// </summary>
		/// <param name="mailTo">The mail to.</param>
		/// <param name="mailCc">The mail cc.</param>
		/// <param name="mailBcc">The mail BCC.</param>
		/// <returns></returns>
		private MailMessage Message( List<MailAddress> mailTo, List<MailAddress> mailCc, List<MailAddress> mailBcc )
		{
			MailMessage message = new MailMessage();

			message.From = this.from;

			foreach (MailAddress to in mailTo)
			{
				message.To.Add( to );
			}

			if (mailCc != null)
			{
				foreach (MailAddress cc in mailCc)
				{
					message.To.Add( cc );
				}
			}

			if (mailBcc != null)
			{
				foreach (MailAddress bcc in mailBcc)
				{
					message.To.Add( bcc );
				}
			}

			message.Subject = this.subject;

			AlternateView body;

			if (this.isHTMLMode)
			{
				body = AlternateView.CreateAlternateViewFromString( this.mailBody, Encoding.GetEncoding( "shift-jis" ), MediaTypeNames.Text.Html );
			}
			else
			{
				body = AlternateView.CreateAlternateViewFromString( this.mailBody, Encoding.UTF8, MediaTypeNames.Text.Plain );
			}

			message.AlternateViews.Add( body );

			return message;
		}

		/// <summary>
		/// Gets the mail temporary.
		/// </summary>
		/// <returns></returns>
		private bool GetMailTemp()
		{
			// Get info
			this.mailBody = File.ReadAllText( HttpContext.Current.Server.MapPath( "~/MailTemplates/" + this.templatePath ) );

			if (string.IsNullOrEmpty( this.mailBody ))
			{
				return false;
			}

			// Replace body
			if (this.bodyReplacement != null)
			{
				foreach (ReplacementModel bodyItem in this.bodyReplacement)
				{
					this.mailBody = this.mailBody.Replace( bodyItem.Key, bodyItem.value );
				}
			}

			if (!this.isHTMLMode)
			{
				this.mailBody = this.mailBody.Replace( "<br />", "\n" );
			}
			return true;
		}

		#endregion Private methods

		#region Send methods

		/// <summary>
		/// Sends the mail.
		/// </summary>
		/// <returns></returns>
		public bool SendMail()
		{
			if (!this.isValidInput())
			{
				return false;
			}

			this.GetMailTemp();

			NetworkCredential credentials = new NetworkCredential( this.sMTP_UserName, this.sMTP_Password );

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
			using (var client = new SmtpClient( this.host, this.port ))
			{
				client.Credentials = credentials;
				client.EnableSsl = true;
				client.Timeout = 6000;
				MailMessage message = this.Message( this.to, this.cC, this.bCC );

				try
				{
					client.Send( message );
				}
				catch
				{
					//Email address is not verified.
					return false;
				}

				return true;
			}
		}

		#endregion Send methods
	}
}
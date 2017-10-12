namespace Gulliver.DoCol.Library
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.IO;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Script.Serialization;
	using Gulliver.DoCol.BusinessServices.Common;
	using Gulliver.DoCol.Entities;
	using Gulliver.DoCol.Entities.Welcome;

	public class WebService
	{
		public static string GetSiireNoList( string jsonShodanList )
		{
			try
			{
				// Create a request using a URL that can receive a post.
				WebRequest request = WebRequest.Create(ConfigurationManager.AppSettings["TAPSWebService"].ToString());
				request.Method = "POST";
				string postData = @"jsonShodanList=" + jsonShodanList;
				byte[] byteArray = Encoding.UTF8.GetBytes(postData);
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = byteArray.Length;
				Stream dataStream = request.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();
				WebResponse response = request.GetResponse();

				// Get the stream containing content returned by the server.
				dataStream = response.GetResponseStream();

				// Open the stream using a StreamReader for easy access.
				if (dataStream != null)
				{
					StreamReader reader = new StreamReader(dataStream);

					// Read the content.
					string responseFromServer = reader.ReadToEnd();

					// Clean up the streams.
					reader.Close();
					dataStream.Close();
					response.Close();

					return responseFromServer;
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		public static async Task<string> GetSiireNoListAsnyc( string jsonShodanList )
		{
			using (var client = new HttpClient())
			{
				var values = new Dictionary<string, string>
					{
						{ "jsonShodanList", jsonShodanList }
					};

				var content = new FormUrlEncodedContent( values );

				var response = await client.PostAsync( ConfigurationManager.AppSettings["TAPSWebService"].ToString(), content );

				var responseString = await response.Content.ReadAsStringAsync();

				return responseString;
			}
		}

		public static DataTable GetShiireNo( string OrderNo )
		{
			List<string> listOrderNo = new List<string>();
			if (!string.IsNullOrWhiteSpace( OrderNo ))
			{
				listOrderNo.Insert( 0, OrderNo );
			}

			return GetShiireNo( listOrderNo );
		}

		public static DataTable GetShiireNo( List<string> listOrderNo, bool? isBilling = null )
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();

			#region Get DataTable from input parameter
			// Set list item to insert to db.
			DataTable tableOrder = new DataTable();

			// Define datatable property
			tableOrder.Columns.Add( "OrderNo", typeof( string ) );

			if (listOrderNo != null)
			{
				// Loop and add data to datatable
				foreach (string order in listOrderNo)
				{
					// Add value for row
					DataRow row = tableOrder.NewRow();
					row["OrderNo"] = order;

					// Add row to table
					tableOrder.Rows.Add( row );
				}
			}
			#endregion

			using (var service = new CmnService())
			{
				List<CmnShiireNo> listShiireNo = service.GetShiireNoService( tableOrder, isBilling );
				List<CmnShiireNo> listNeedGetShiireNoFromService =
					listShiireNo.Where( x => x.TapsOrderFlg.HasValue && x.TapsOrderFlg.Value.Equals( true ) ).ToList();

				string parameterGetShiireNo =
					jss.Serialize(
						listNeedGetShiireNoFromService.Select(
							a => new { TEMPO_CD = a.TEMPO_CD, SHODAN_KANRI_NO = a.SHODAN_KANRI_NO, SHODAN_KANRI_EDA_NO = a.SHODAN_KANRI_EDA_NO } ) );

				Log.SaveLogToFile( Log.LogLevel.INFO,
									"GetShiireNo",
									"FromDB",
									"",
									"",
									parameterGetShiireNo,
									"" );

				string getShiireNoFromService = GetSiireNoList( parameterGetShiireNo );

				Log.SaveLogToFile( Log.LogLevel.INFO,
									"GetShiireNo",
									"FromWebService",
									"",
									"",
									getShiireNoFromService,
									"" );

				if (!string.IsNullOrWhiteSpace( getShiireNoFromService ))
				{
					List<CmnShiireNo> listGetShiireNoFromService = jss.Deserialize<List<CmnShiireNo>>( getShiireNoFromService );
					if (listGetShiireNoFromService != null && listGetShiireNoFromService.Count > 0)
					{
						foreach (var item in listNeedGetShiireNoFromService)
						{
							string sss =
								listGetShiireNoFromService.Find(
									x => x.TEMPO_CD == item.TEMPO_CD && x.SHODAN_KANRI_EDA_NO == item.SHODAN_KANRI_EDA_NO
									&& x.SHODAN_KANRI_NO == item.SHODAN_KANRI_NO ).SHIIRE_NO;
							item.SHIIRE_NO = sss ?? item.SHIIRE_NO;
						}
					}

					return EntityHelper<CmnShiireNo>.ConvertToDataTable( listNeedGetShiireNoFromService );
				}
			}

			return null;
		}
	}
}
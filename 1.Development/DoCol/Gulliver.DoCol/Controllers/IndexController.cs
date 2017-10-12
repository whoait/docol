using Gulliver.DoCol.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gulliver.DoCol.WebReference;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gulliver.DoCol.BusinessServices;
using Gulliver.DoCol.ReportServices;
namespace Gulliver.DoCol.Controllers
{
    public class IndexController : Controller
    {
        private Dictionary<string, string> _contentType = new Dictionary<string, string>();
        string cn = ConfigurationManager.AppSettings.Get("ConnectionString");
        private string DEF_RD0010_FILE = @"\DoCol\RD0020.fcp";

        public ActionResult DCW0003Print()
        {
            return View();
        }

        public ActionResult Index(string[] param1, string ReportID = "RD0020")
        {
            //string ReportID = "RD0020";
            string[] DocControlNo = { "1", "2", "3", "4" };
            DataTable table = new DataTable();
            //table.Columns.Add("ID", typeof(string));
            //table.Rows.Add("1");
            //table.Rows.Add("2");

            //DataTable DocControlNo = table;
            MemoryStream stream = new MemoryStream();
            #region getFileReport
            try
            {
                // コンテントタイプの辞書を作成
                _contentType.Clear();
                _contentType.Add(".fcp", "application/x-fmfcp");
                _contentType.Add(".fcx", "application/x-fmfcx");

                _contentType.Add(".dat", "text/plain");
                _contentType.Add(".xml", "application/x-fmdat+xml");
                _contentType.Add(".csv", "text/csv");
                _contentType.Add(".tsv", "text/tab-separated-value");
                _contentType.Add(".fcq", "application/x-fmfcq");

                _contentType.Add(".bmp", "image/x-bmp");
                _contentType.Add(".emf", "image/x-emf");
                _contentType.Add(".wmf", "image/x-wmf");
                _contentType.Add(".tif", "image/tiff");
                _contentType.Add(".jpg", "image/jpeg");
                _contentType.Add(".gif", "image/gif");
                _contentType.Add(".png", "image/png");

                _contentType.Add(".dev", "application/XmlReadMode-fmfcp");

                _contentType.Add(".fmcsm", "Application/x-fmcsm");
                _contentType.Add(".fmcsmd", "Application/x-fmcsmd");

                FMWebService service = new FMWebService();
                service.Timeout = 1200 * 1000;
                service.Url = ConfigurationManager.AppSettings["WebReference.FMWebService"];

                FMWSRequest request = new FMWSRequest();
                FMWSKeyValue[] headers = new FMWSKeyValue[0];
                request.headers = headers;

                //リクエストパラメータの生成と、リクエストへの登録
                List<FMWSKeyValue> parameters = new List<FMWSKeyValue>();
                FMWSKeyValue param = new FMWSKeyValue();

                List<FMWSData> datas = new List<FMWSData>();

                System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
                string fpath = Server.MapPath(@"~/iwfm/") + "iwfm_" + DateTime.Now.ToString("yMMdd_HHmmss") + ".dat";
                System.IO.StreamWriter sr = new System.IO.StreamWriter(fpath, false, enc);

                if (ReportID == "RD0020") param.value = DEF_RD0010_FILE;
                ///// 開始
                sr.WriteLine(@"[Control Section]");
                sr.WriteLine(@"VERSION=7.2");
                sr.WriteLine(@"OPTION=FIELDATTR");
                sr.WriteLine(@";");

                // Get data for report
                DataTable dataReport = new DataTable();
                DataReportsClients getDataReportClient = new DataReportsClients();
                //DataTable table = new DataTable();
                table.Columns.Add("Item", typeof(string));
                for (int i = 0; i < DocControlNo.Length; i++)
                    table.Rows.Add(DocControlNo[i].ToString());
                var pList = new SqlParameter("@list", SqlDbType.Structured);
                pList.TypeName = "dbo.StringList";
                pList.Value = table;
                if (ReportID == "RD0020")
                {
                    dataReport = getDataReportClient.ExportPDFRD0020(table);
                }
                if (dataReport.Rows.Count > 0)
                {
                    param.key = "fm-formfilename";
                    parameters.Add(param);
                    param = new FMWSKeyValue();
                    param.key = "fm-outputtype";
                    param.value = "pdf";
                    parameters.Add(param);
                    param = new FMWSKeyValue();
                    param.key = "fm-action";
                    param.value = "view";
                    parameters.Add(param);
                    param = new FMWSKeyValue();
                    param.key = "fm-target";
                    param.value = "client";
                    parameters.Add(param);
                    request.parameters = parameters.ToArray();

                    foreach (DataRow dr in dataReport.Rows)
                    {
                        //if (dr["AA会場"].ToString().Contains("JU"))
                        //{

                        #region create data file RD0020.DAT
                        sr.WriteLine(@"[Body Data Section]");
                        sr.WriteLine(string.Format(@"<line1>={0}", dr["SHOP_CD"].ToString()));
                        sr.WriteLine(string.Format(@"<line1_1>={0}", dr["TEMPO_NAME"].ToString()));
                        sr.WriteLine(string.Format(@"<line1_2>={0}", "在庫"));
                        sr.WriteLine(string.Format(@"<line2>={0}", "車庫証明・自社名依頼申請書"));
                        sr.WriteLine(string.Format(@"<line3>={0}", "店舗コード"));
                        sr.WriteLine(string.Format(@"<line3_1>={0}", dr["DJ_SHOPCD"].ToString()));
                        sr.WriteLine(string.Format(@"<line4>={0}", "店舗名"));
                        sr.WriteLine(string.Format(@"<line4_1>={0}", dr["TEMPO_NAME"].ToString()));
                        sr.WriteLine(string.Format(@"<line5>={0}", "車名"));
                        sr.WriteLine(string.Format(@"<line5_1>={0}", dr["CAR_NAME"].ToString()));
                        sr.WriteLine(string.Format(@"<line6>={0}", "車台番号"));
                        sr.WriteLine(string.Format(@"<line6_1>={0}", dr["CHASSIS_NO"].ToString()));
                        sr.WriteLine(string.Format(@"<line7>={0}", "仕入番号"));
                        sr.WriteLine(string.Format(@"<line7_1>={0}", dr["SHIIRE_NO"].ToString()));
                        sr.WriteLine(string.Format(@"<line8>={0}", "※ 車庫証明を取得し、自社名変をお願い致します"));
                        sr.WriteLine(string.Format(@"<line9>={0}", "※ 自社名終了後は車検原本をこの申請書と一緒に幕張オフィスDNチームにお送り下さい。"));
                        sr.WriteLine(string.Format(@"<line10>={0}", "DN書類チーム"));
                        sr.WriteLine(string.Format(@"<line11>={0}", "J-NET TEL"));
                        sr.WriteLine(string.Format(@"<line11_1>={0}", "20000-28"));
                        sr.WriteLine(string.Format(@"<line12>={0}", "          FAX"));
                        sr.WriteLine(string.Format(@"<line12_1>={0}", "20000-38"));
                        sr.WriteLine(string.Format(@"<line13>={0}", "管理番号: "));
                        sr.WriteLine(string.Format(@"<line13_1>={0}", dr["CONTROL_NUMBER"].ToString()));

                        sr.WriteLine(string.Format(@"[Form Section]"));
                        sr.WriteLine(string.Format(@"NEXTPAGE"));
                        sr.WriteLine(string.Format(@";"));
                        #endregion
                    }
                }

                sr.Close();

                //DATファイルの登録
                FMWSData datdata = new FMWSData();
                datdata.content = System.IO.File.ReadAllBytes(fpath);
                datdata.contentName = Path.GetFileName(fpath);
                datdata.contentType = _contentType[Path.GetExtension(fpath).ToLower()];
                datas.Add(datdata);

                request.attachments = datas.ToArray();

                FMWSResponse response = service.overlay(request);
                Dictionary<string, List<String>> headerMap = new Dictionary<string, List<string>>();
                foreach (FMWSKeyValue header in response.headers)
                {
                    List<string> list;
                    if (!headerMap.ContainsKey(header.key))
                    {
                        list = new List<string>();
                        headerMap.Add(header.key, list);
                    }
                    else
                    {
                        list = headerMap[header.key];
                    }
                    list.Add(header.value);
                }
                string serviceStatus = headerMap["x-service-status"][0];//.ElementAt(0);
                //ステータスの確認
                if (serviceStatus != "200")
                {
                    string serviceMessage = headerMap["x-service-message"][0];//.ElementAt(0);
                    throw new Exception("overlay failure: " + serviceMessage);
                }

                Response.ClearContent();
                Response.Buffer = true;

                foreach (FMWSData attachment in response.attachments)
                {

                    //IEの場合はファイル名をURLエンコードする
                    String file = attachment.contentName;
                    if (Request.Browser.Browser == "IE")
                    {
                        file = HttpUtility.UrlEncode(file);
                    }
                    Response.AddHeader("Content-Disposition", "inline;filename=Report" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(attachment.content);
                    break;
                }

                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return new EmptyResult();
        }

        public ActionResult OutputCsv()
        {
            DataReportsClients getDataReportClient = new DataReportsClients();
            MemoryStream stream = new MemoryStream();
            DataTable table = new DataTable();
            table.Columns.Add("ファイルNO", typeof(string));
            table.Columns.Add("車台番号", typeof(string));
            table.Rows.Add("1","123");
            table.Rows.Add("2","123");
            stream = getDataReportClient.ExportCSV(table);


            string attachment = "attachment; filename= " + DateTime.Now.ToString("yyyyMMddhhmmss") + "_TO_HD_CHECK.csv";
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            //Response.AddHeader("ファイルNO", "車台番号");
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            return new EmptyResult();
        }

    }
}

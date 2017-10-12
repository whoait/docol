//---------------------------------------------------------------------------
// Version			: 001
// Designer			: BinhVT5-FPT
// Programmer		: BinhVT5-FPT
// Date				: 2015/11/24
// Comment			: Create new
//---------------------------------------------------------------------------


namespace Gulliver.DoCol.Areas.DCW.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Gulliver.DoCol.Entities.DCW.DCW002Model;
    using Gulliver.DoCol.BusinessServices;
    using Gulliver.DoCol.Controllers;
    using System.IO;
    using Gulliver.DoCol.UtilityServices;
    using System.Web;
    using System.Data;
    using Gulliver.DoCol.Constants;
    using Gulliver.DoCol.ReportServices;
	using System.Text;


    /// <summary>
    /// Show menu screen
    /// </summary>
    public class DCW002Controller : BaseController
    {

        private const string DCW002CheckMess = "DCW002CheckMess";
        /// <summary>
        /// Page load Shop menu screen
        /// </summary>
        /// <returns>view screen</returns>
        public ActionResult DCW002Menu()
        {

            string msgType = string.Empty;
            string msgContent = string.Empty;
            
            DCW002Model cache = base.GetCache<DCW002Model>(DCW002CheckMess);
            if (cache != null)
            {
                switch (cache.checkMess)
                {
                    //Khong dung dinh dang
                    case 1: Utility.GetMessage(MessageCd.E0002, cache.FileName, out msgType, out msgContent); break;
                    //Khong phai file csv
                    case 2: Utility.GetMessage(MessageCd.E0003, "", out msgType, out msgContent); break;
                    //Chua input file
                    case 3: Utility.GetMessage(MessageCd.E0001, "", out msgType, out msgContent); break;

                    case 4: Utility.GetMessage(MessageCd.E0008, "", out msgType, out msgContent); break;

                    case 0: { msgType = string.Empty; msgContent = string.Empty; } break;
                }

                ViewBag.MessageType = msgType;

                ViewBag.MessageContent = msgContent;

                ViewBag.Mode = cache.mode;

                ViewBag.Check = cache.check;

                ViewBag.FileName = cache.FileName;
            }
            else {
            }
            //base.RemoveCacheInTab();
            base.RemoveCache("DCW003ConditionCache");
            return View();
        }

        public ActionResult DCW002UploadFile(HttpPostedFileBase file, int mode, int check)
        {
            int checkMess = 0;
            string msgType = string.Empty;
            string msgContent = string.Empty;
            
            DataTable dt = new DataTable();

            if (file != null)
            {
                var postfile = Request.Files[0];

                int rowCount = 0;
                string[] columnNames = null;
                string[] DataValues = null;

                if (Path.GetExtension(postfile.FileName) == ".csv")
                {
					var csvReader = new System.IO.StreamReader( postfile.InputStream, Encoding.GetEncoding( "shift-jis" ) );
                    ViewBag.FileName = postfile.FileName;
                    if (mode == 6 && postfile.FileName.Contains(Import1.FileName))
                    {
                        List<string> lst = new List<string>();
                        while (!csvReader.EndOfStream && checkMess == 0)
                        {
                            string RowData = csvReader.ReadLine().Trim();
                            if (RowData.Length > 0)
                            {
                                DataValues = RowData.Split(',');
                                if (rowCount == 0)
                                {
                                    rowCount = 1;
                                    columnNames = DataValues;
                                    foreach (string csvHeader in columnNames)
                                    {
                                        DataColumn dc = new DataColumn(csvHeader.ToUpper(), typeof(string));
                                        dc.DefaultValue = string.Empty;
                                        dt.Columns.Add(dc);
                                    }
                                    if (columnNames.Length != 7) checkMess = 1;
                                    else
                                    {
                                        //if (columnNames[0] != Import1.Col0 || columnNames[1] != Import1.Col1
                                        //    || columnNames[2] != Import1.Col2 || columnNames[3] != Import1.Col3
                                        //    || columnNames[4] != Import1.Col4 || columnNames[5] != Import1.Col5 
                                        //    || columnNames[6] != Import1.Col6
                                        //    ) checkMess = 1;
                                    }
                                }
                                else
                                {
                                    DataRow dr = dt.NewRow();

                                    for (int i = 0; i < columnNames.Length; i++)
                                    {
                                        dr[columnNames[i]] = DataValues[i] == null ? string.Empty : DataValues[i].ToString();
                                    }
                                    if (string.IsNullOrEmpty(DataValues[0]) || string.IsNullOrEmpty(DataValues[1]) || string.IsNullOrEmpty(DataValues[2])
                                        || string.IsNullOrEmpty(DataValues[4]) || string.IsNullOrEmpty(DataValues[6])
                                        ) checkMess = 1;
                                    else if (DataValues[0].Length > 5 || DataValues[2].Length > 24 || DataValues[3].Length > 1
                                        || DataValues[4].Length > 24 || DataValues[5].Length > 12 || DataValues[6].Length > 1
                                        ) checkMess = 1;
                                    else dt.Rows.Add(dr);
                                    lst.Add(DataValues[4]);
                                    
                                }
                            }
                        }
                        for (int i = 0; i < lst.Count - 1; i++)
                        {
                            for (int j = i + 1; j < lst.Count; j++)
                            {
                                if (lst[i] == lst[j])
                                {
                                    checkMess = 4; break;
                                }
                            }
                        }

                    }

                    else if (mode == 7 && postfile.FileName.Contains(Import2.FileName))
                    {
                        List<string> lst = new List<string>();
                        while (!csvReader.EndOfStream && checkMess == 0)
                        {
                            string RowData = csvReader.ReadLine().Trim();
                            if (RowData.Length > 0)
                            {
                                DataValues = RowData.Split(',');
                                if (rowCount == 0)
                                {
                                    rowCount = 1;
                                    columnNames = DataValues;
                                    foreach (string csvHeader in columnNames)
                                    {
                                        DataColumn dc = new DataColumn(csvHeader.ToUpper(), typeof(string));
                                        dc.DefaultValue = string.Empty;
                                        dt.Columns.Add(dc);
                                    }
                                    if (columnNames.Length != 6) checkMess = 1;
                                    else
                                    {
                                        if (columnNames[0] != Import2.Col0 || columnNames[1] != Import2.Col1
                                            || columnNames[2] != Import2.Col2 || columnNames[3] != Import2.Col3
                                            || columnNames[4] != Import2.Col4 || columnNames[5] != Import2.Col5
                                            ) checkMess = 1;
                                    }
                                }
                                else
                                {
                                    DataRow dr = dt.NewRow();

                                    for (int i = 0; i < columnNames.Length; i++)
                                    {
                                        dr[columnNames[i]] = DataValues[i] == null ? string.Empty : DataValues[i].ToString();
                                    }
                                    if (string.IsNullOrEmpty(DataValues[0]) || string.IsNullOrEmpty(DataValues[1]) || string.IsNullOrEmpty(DataValues[3])
                                        || string.IsNullOrEmpty(DataValues[5])
                                        ) checkMess = 1;
                                    else if (DataValues[0].Length > 1 || DataValues[1].Length > 24 || DataValues[2].Length > 1
                                        || DataValues[3].Length > 24 || DataValues[4].Length > 12 || DataValues[5].Length > 1
                                        ) checkMess = 1;
                                    else dt.Rows.Add(dr);
                                    lst.Add(DataValues[3]);
                                }
                            }
                        }
                        for (int i = 0; i < lst.Count - 1; i++)
                        {
                            for (int j = i + 1; j < lst.Count; j++)
                            {
                                if (lst[i] == lst[j])
                                {
                                    checkMess = 4; break;
                                }
                            }
                        }
                    }
                    else if (mode == 8 && postfile.FileName.Contains(Import3.FileName))
                    {
                        while (!csvReader.EndOfStream && checkMess == 0)
                        {
                            string RowData = csvReader.ReadLine().Trim();
                            if (RowData.Length > 0)
                            {
                                DataValues = RowData.Split(',');
                                if (rowCount == 0)
                                {
                                    rowCount = 1;
                                    columnNames = DataValues;
                                    foreach (string csvHeader in columnNames)
                                    {
                                        DataColumn dc = new DataColumn(csvHeader.ToUpper(), typeof(string));
                                        dc.DefaultValue = string.Empty;
                                        dt.Columns.Add(dc);
                                    }
                                    if (columnNames.Length != 1) checkMess = 1;
                                    else
                                    {
                                        if (columnNames[0] != Import3.Col0) checkMess = 1;  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dt.NewRow();

                                    for (int i = 0; i < columnNames.Length; i++)
                                    {
                                        dr[columnNames[i]] = DataValues[i] == null ? string.Empty : DataValues[i].ToString();
                                    }
                                    if (DataValues[0].Length > 25 || string.IsNullOrEmpty(DataValues[0])) checkMess = 1;
                                    else dt.Rows.Add(dr);
                                }
                            }
                        }

                    }

                    else if (mode == 10)
                    {
                        #region
                        //while (!csvReader.EndOfStream && checkMess == 0)
                        //{
                        //    string RowData = csvReader.ReadLine().Trim();
                        //    if (RowData.Length > 0)
                        //    {
                        //        DataValues = RowData.Split(',');
                        //        if (rowCount == 0)
                        //        {
                        //            rowCount = 1;
                        //            columnNames = DataValues;
                        //            foreach (string csvHeader in columnNames)
                        //            {
                        //                DataColumn dc = new DataColumn(csvHeader.ToUpper(), typeof(string));
                        //                dc.DefaultValue = string.Empty;
                        //                dt.Columns.Add(dc);
                        //            }
                        //            if (columnNames.Length != 1) checkMess = 1;
                        //            else
                        //            {
                        //                if (columnNames[0] != "RFIDキー") checkMess = 1;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            DataRow dr = dt.NewRow();

                        //            for (int i = 0; i < columnNames.Length; i++)
                        //            {
                        //                dr[columnNames[i]] = DataValues[i] == null ? string.Empty : DataValues[i].ToString();
                        //            }
                        //            if (DataValues[0].Length > 24) checkMess = 1;
                        //        }
                        //    }
                        //}
                        #endregion
                        checkMess = 1;
                    }

                    else if (mode == 9)
                    {
                        List<string> lst = new List<string>();
                        while (!csvReader.EndOfStream && checkMess == 0)
                        {
                            string RowData = csvReader.ReadLine().Trim();
                            if (RowData.Length > 0)
                            {
                                DataValues = RowData.Split(',');
                                if (rowCount == 0)
                                {
                                    rowCount = 1;
                                    columnNames = DataValues;
                                    foreach (string csvHeader in columnNames)
                                    {
                                        DataColumn dc = new DataColumn(csvHeader.ToUpper(), typeof(string));
                                        dc.DefaultValue = string.Empty;
                                        dt.Columns.Add(dc);
                                    }
                                    if (columnNames.Length != 7) checkMess = 1;
                                    else
                                    {
                                        if (columnNames[0] != Import4.Col0 || columnNames[1] != Import4.Col1 || columnNames[2] != Import4.Col2 || columnNames[3] != Import4.Col3
                                            || columnNames[4] != Import4.Col4 || columnNames[5] != Import4.Col5 || columnNames[6] != Import4.Col6
                                            )
                                        {
                                            checkMess = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    DataRow dr = dt.NewRow();

                                    for (int i = 0; i < columnNames.Length; i++)
                                    {
                                        dr[columnNames[i]] = DataValues[i] == null ? string.Empty : DataValues[i].ToString();
                                    }

                                    if (string.IsNullOrEmpty(DataValues[0]) || string.IsNullOrEmpty(DataValues[1]) || string.IsNullOrEmpty(DataValues[2]) || string.IsNullOrEmpty(DataValues[3])
                                        || string.IsNullOrEmpty(DataValues[4]) || string.IsNullOrEmpty(DataValues[6]))
                                    {
                                        checkMess = 1;
                                    }

                                    else if (DataValues[0].Length > 10 || DataValues[1].Length > 6 || DataValues[2].Length > 30
                                        || DataValues[3].Length > 50 || DataValues[4].Length > 25 || DataValues[6].Length > 500
                                        || (DataValues[5] != Import4.Type1 && DataValues[5] != Import4.Type2)
                                        )
                                    {
                                        checkMess = 1;
                                    }
                                    else dt.Rows.Add(dr);
                                    lst.Add(DataValues[4]);
                                }
                            }
                        }
                        for (int i = 0; i < lst.Count - 1; i++)
                        {
                            for (int j = i + 1; j < lst.Count; j++)
                            {
                                if (lst[i] == lst[j])
                                {
                                    checkMess = 4; break;
                                }
                            }
                        }
                    }
                    else
                    {
                        checkMess = 1;
                    }
                }
                else
                {
                    checkMess = 2;
                }
            }
            else
            {
                checkMess = 3;
            }
            DCW002Model model = new DCW002Model();
            model.checkMess = checkMess;
            model.FileName = ViewBag.FileName;
            model.mode = mode;
            model.check = check;
            if (checkMess != 0) 
            {
                base.SaveCache(DCW002CheckMess, model);
                return base.Redirect("DCW002Menu", "DCW002");
            }
            else
            {
                TempData["tblCsv"] = dt;
                base.RemoveCache("DCW003ConditionCache");
                base.SaveCache(DCW002CheckMess, model);
                return base.Redirect("DCW003Index", "DCW003", new { Area = "DCW", mode, check });
            }
        }

        public ActionResult GoToDCW003(string mode, int check)
        {
            base.RemoveCache("DCW003ConditionCache");
            return base.Redirect("DCW003Index", "DCW003", new { Area = "DCW", mode, check });
        }

        public ActionResult DCW002ExportCsv()
        {
            DataReportsClients getDataReportClient = new DataReportsClients();
            MemoryStream stream = new MemoryStream();
            stream = getDataReportClient.ExportCsvRC002();

            string attachment = "attachment; filename= " + DateTime.Now.ToString("yyyyMMddhhmmss") + "_TO_HD_MST.csv";
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/csv";
            //Response.AddHeader("ファイルNO");
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
            return new EmptyResult();
        }
    }
}

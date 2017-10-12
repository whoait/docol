using Gulliver.DoCol.DataAccess.Framework;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gulliver.DoCol.ReportServices
{
    public class DataReportsClients : IDataReports
    {
        public DataReportsClients()
        {
            System.Net.ServicePointManager.Expect100Continue = false;
        }

        private string GetPathImageServer()
        {
            string pathImageServer = "";
            try
            {
                pathImageServer = System.Web.Configuration.WebConfigurationManager.AppSettings["ImageServer"];
            }
            catch (Exception)
            {
            }
            return pathImageServer;
        }

        #region Estimation

        /// <summary>
        /// Export reports RS3010 to file PDF 
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@orderNo"
        /// -Index [0][1] = value orderNo
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3010(string[,] param, ref string error)
        {
            //string[,] mParam = new string[1, 2];
            //mParam[0, 0] = "@RequestEstimationNo";
            //mParam[0, 1] = requestEstimationNo.Trim();
            return GetFileReports("RS3010", param, ref error);
        }

        /// <summary>
        /// Export reports RS3011 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@EstimationNo"
        /// -Index [0][1] = value EstimationNo
        /// -Index [1][0] = "@CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3011(string[,] param, ref string error)
        {
            return GetFileReports("RS3011", param, ref error);
        }

        /// <summary>
        /// Export reports RS3012 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3012(string[,] param, ref string error)
        {
            //string[,] mParam = new string[1, 2];
            //mParam[0, 0] = "@OrderNo";
            //mParam[0, 1] = orderNo.Trim();
            return GetFileReports("RS3012", param, ref error);
        }

        /// <summary>
        /// Export reports RS3013 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// -Index [1][0] = "@CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3013(string[,] param, ref string error)
        {
            return GetFileReports("RS3013", param, ref error);
        }

        /// <summary>
        /// Export reports RS3014 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@DeliveryNo"
        /// -Index [0][1] = value DeliveryNo
        /// -Index [1][0] = "@CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3014(string[,] param, ref string error)
        {
            return GetFileReports("RS3014", param, ref error);
        }

        /// <summary>
        /// Export reports RS3015 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@BillingNo"
        /// -Index [0][1] = value BillingNo
        /// -Index [1][0] = "@CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS3015(string[,] param, ref string error)
        {
            return GetFileReports("RS3015", param, ref error);
        }

        /// <summary>
        /// Export reports RS7010 to file PDF
        /// </summary>
        /// <param name="listBilling">DataTable</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRS7010(DataTable listBilling, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            result = GetDataReports(listBilling, ConstantsStpReports.RS7010, ref error);
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReports = reportService.ExportPDF("RS7010", result, ref error);
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReports;
        }

        /// <summary>
        /// Export CSV RS3016 to file CSV
        /// Save file wwith type : Denpyo_yyyyMMddHHmm.zip
        /// </summary>
        /// <param name="listOrder">DataTable</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRS3016(DataTable listOrder, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReportsCSV = new MemoryStream();
            ReportServices reportService = new ReportServices();
            result = GetDataReports(listOrder, ConstantsStpReports.RS3016, ref error);
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        //streamReportsCSV = reportService.ExportCSV("RS3016", result, ref error);
                        streamReportsCSV = ExportPDF("RS3016", result);
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        #endregion

        #region Order

        /// <summary>
        /// Export reports RH2030 to file PDF 
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2030(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2030", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2031 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2031(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2031", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2032 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2032(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2032", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2033 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2033(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2033", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2050 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value orderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2050(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2050", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2051 to file PDF
        /// </summary>
        /// <param name="dtRH2051">DataSet: 2 table</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRH2051(DataSet dtRH2051, ref string error)
        {
            //DataSet result = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            try
            {
                if (dtRH2051.Tables.Count > 0)
                {
                    if (dtRH2051.Tables[0].Rows.Count > 0)
                    {
                        streamReports = reportService.ExportPDF("RH2051", dtRH2051, ref error);
                    }
                    else
                    {
                        error = "I0007";
                    }
                }
                else
                {
                    error = "I0007";
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }

            return streamReports;
        }

        /// <summary>
        /// Export reports RH2060 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value orderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2060(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2060", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2061 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2061(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2061", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2070 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDFRH2070(string[,] mParam, ref string error)
        {
            return GetFileReports("RH2070", mParam, ref error);
        }

        /// <summary>
        /// Export reports RH2080 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "@OrderNo"
        /// -Index [0][1] = value OrderNo
        /// -Index [1][0] = "@CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// -Index [2][0] = "@IsGLV"
        /// -Index [2][1] = [value = 0: does not include components GLV; value = 1: include components GLV; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRH2080(string[,] param, ref string error)
        {
            return GetFileReports("RH2080", param, ref error);
        }

        /// <summary>
        /// Export reports RH4040 to file PDF
        /// </summary>
        /// <param name="listBillingNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRH4040(DataTable listBillingNo, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            result = GetDataReports(listBillingNo, ConstantsStpReports.RH4040, "@TableListBillingNo", ref error);
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count > 0)
                    {
                        error = "W0040";
                    }
                    else if (result.Tables[1].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        result.Tables.RemoveAt(0);
                        streamReports = reportService.ExportPDF("RH4040", result, ref error);
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReports;
        }

        /// <summary>
        /// Export reports RH5050 to file PDF
        /// </summary>
        /// <param name="TableKeyFind"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRH5050(DataTable TableKeyFind, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            result = GetDataReports(TableKeyFind, ConstantsStpReports.RH5050, "@TableBilling", ref error);
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[1].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReports = reportService.ExportPDF("RH5050", result, ref error);
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReports;
        }

        /// <summary>
        /// Export reports RH9040 to file PDF
        /// </summary>
        /// <param name="TableContactFubi"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportPDFRH9040(DataTable TableContactFubi, ref string error)
        {
            DataSet dataSetResult = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (TableContactFubi.Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        dataSetResult.Tables.Add(TableContactFubi);
                        streamReports = reportService.ExportPDF("RH9040", dataSetResult, ref error);
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReports;
        }
        #endregion

        #region GetDataReport
        /// <summary>
        /// Get data to report
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nameStoreProcedure"></param>
        /// <param name="messErr"></param>
        /// <returns>DataSet</returns>
        private DataSet GetDataReports(string[,] param, string nameStoreProcedure, ref string messErr)
        {
            DataSet dataSetResult = new DataSet();
            try
            {
                using (DBManager dbManager = new DBManager(nameStoreProcedure))
                {
                    for (int i = param.GetLowerBound(0); i <= param.GetUpperBound(0); i++)
                    {
                        if (param[i, 0] != "")
                        {
                            if (!string.IsNullOrEmpty(param[i, 1]))
                            {
                                dbManager.Add(param[i, 0].ToString(), param[i, 1].ToString());
                            }
                            else
                            {
                                dbManager.Add(param[i, 0].ToString(), DBNull.Value);
                            }
                        }
                    }
                    dataSetResult = dbManager.GetDataSet();
                }
            }
            catch (Exception)
            {
                messErr = "E0003";//err.Message;
            }
            return dataSetResult;
        }

        /// <summary>
        /// Get data to report from input param table
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="nameStoreProcedure"></param>
        /// <param name="messErr"></param>
        /// <returns></returns>
        private DataSet GetDataReports(DataTable listData, string nameStoreProcedure, ref string messErr)
        {
            DataSet dataSetResult = new DataSet();
            try
            {
                using (DBManager dbManager = new DBManager(nameStoreProcedure))
                {
                    dbManager.Add("@TableListBillingNo", SqlDbType.Structured, listData);
                    dataSetResult = dbManager.GetDataSet();
                }
            }
            catch (Exception err)
            {
                messErr = err.Message;
            }
            return dataSetResult;
        }

        private DataSet GetDataReports(DataTable listData, string nameStoreProcedure, string paramList, ref string messErr)
        {
            DataSet dataSetResult = new DataSet();
            try
            {
                using (DBManager dbManager = new DBManager(nameStoreProcedure))
                {
                    dbManager.Add(paramList, SqlDbType.Structured, listData);
                    dataSetResult = dbManager.GetDataSet();
                }
            }
            catch (Exception err)
            {
                messErr = err.Message;
            }
            return dataSetResult;
        }

        /// <summary>
        /// Get file report with type MemoryStream
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="param"></param>
        /// <param name="messErr"></param>
        /// <returns>MemoryStream</returns>
        private MemoryStream GetFileReports(string reportID, string[,] param, ref string messErr)
        {
            DataSet result = new DataSet();
            MemoryStream streamReports = new MemoryStream();
            ReportServices reportService = new ReportServices();
            switch (reportID)
            {
                #region Estimation
                case "RS3010":
                    result = GetDataReports(param, ConstantsStpReports.RS3010, ref messErr);
                    break;
                case "RS3011":
                    result = GetDataReports(param, ConstantsStpReports.RS3011, ref messErr);
                    SetStreamImageInTable(result, 5);
                    break;
                case "RS3012":
                    result = GetDataReports(param, ConstantsStpReports.RS3012, ref messErr);
                    break;
                case "RS3013":
                    result = GetDataReports(param, ConstantsStpReports.RS3013, ref messErr);
                    break;
                case "RS3014":
                    result = GetDataReports(param, ConstantsStpReports.RS3014, ref messErr);
                    break;
                case "RS3015":
                    result = GetDataReports(param, ConstantsStpReports.RS3015, ref messErr);
                    break;
                case "RS7010":
                    result = GetDataReports(param, ConstantsStpReports.RS7010, ref messErr);
                    break;
                #endregion

                #region Order
                case "RH2030":
                    result = GetDataReports(param, ConstantsStpReports.RH2030, ref messErr);
                    break;
                case "RH2031":
                    result = GetDataReports(param, ConstantsStpReports.RH2031, ref messErr);
                    break;
                case "RH2032":
                    result = GetDataReports(param, ConstantsStpReports.RH2032, ref messErr);
                    break;
                case "RH2033":
                    result = GetDataReports(param, ConstantsStpReports.RH2033, ref messErr);
                    break;
                case "RH2050":
                    result = GetDataReports(param, ConstantsStpReports.RH2050, ref messErr);
                    break;
                case "RH2060":
                    result = GetDataReports(param, ConstantsStpReports.RH2060, ref messErr);
                    break;
                case "RH2061":
                    result = GetDataReports(param, ConstantsStpReports.RH2061, ref messErr);
                    break;
                case "RH2070":
                    result = GetDataReports(param, ConstantsStpReports.RH2070, ref messErr);
                    break;
                case "RH2080":
                    result = GetDataReports(param, ConstantsStpReports.RH2080, ref messErr);
                    SetStreamImageInTable(result, 5);
                    break;
                case "RH4040":
                    result = GetDataReports(param, ConstantsStpReports.RH4040, ref messErr);
                    break;
                case "RH5050":
                    result = GetDataReports(param, ConstantsStpReports.RH5050, ref messErr);
                    break;
                case "RH9040":
                    result = GetDataReports(param, ConstantsStpReports.RH9040, ref messErr);
                    break;
                #endregion
            }

            try
            {
                if (string.IsNullOrEmpty(messErr))
                {
                    if (result.Tables.Count <= 0)
                    {
                        messErr = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        messErr = "I0007";
                    }
                    else
                    {
                        streamReports = reportService.ExportPDF(reportID, result, ref messErr);
                    }
                }
            }
            catch (Exception)
            {
                messErr = "E0002";
            }

            return streamReports;
        }

        /// <summary>
        /// Get image from media server, set stream image into table Image
        /// </summary>
        /// <param name="listData"></param>
        private void SetStreamImageInTable(DataSet listData, int indexTable)
        {
            if (listData.Tables[indexTable].Rows.Count > 0)
            {
                listData.Tables[indexTable].Rows[0]["ImgFile1"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath1"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile2"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath2"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile3"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath3"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile4"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath4"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile5"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath5"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile6"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath6"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile7"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath7"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile8"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath8"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile9"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath9"].ToString());
                listData.Tables[indexTable].Rows[0]["ImgFile10"] = GetImageFromServer(listData.Tables[indexTable].Rows[0]["ImgPath10"].ToString());
            }
        }

        /// <summary>
        /// Get image from media server, read to array byte
        /// </summary>
        /// <param name="shortPathImg"></param>
        /// <returns></returns>
        public byte[] GetImageFromServer(string shortPathImg)
        {
            byte[] dataImg;
            string fullPathImg = "";
            string pathImgConfig = "";
            string resizeImagePath = "";
            try
            {
                if (!string.IsNullOrEmpty(shortPathImg))
                {
                    pathImgConfig = GetPathImageServer();
                    resizeImagePath = "seibi/thumb/" + resizeImg(shortPathImg);
                    pathImgConfig = pathImgConfig.TrimEnd('/');
                    shortPathImg = shortPathImg.TrimStart('/');
                    shortPathImg = shortPathImg.Replace("seibi/org/", resizeImagePath);
                    //155x134 : pixcel
                    fullPathImg = string.Format("{0}/{1}", pathImgConfig, shortPathImg);
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                    {
                        dataImg = webClient.DownloadData(fullPathImg);
                    }
                    return dataImg;
                }
                else
                {
                    return new byte[0];
                }
            }
            catch (Exception)
            {
            }
            return new byte[0];
        }

        private string resizeImg(string shortPathImg)
        {
            string fileName = Path.GetFileName(shortPathImg);
            string[] sizeFile;
            float width = 0;
            float height = 0;
            float resizeWidth = 0;
            float resizeHeigh = 0;
            if (fileName.IndexOf("_") > 0)
            {
                fileName = fileName.Substring(0, fileName.IndexOf("_"));
                if (fileName.Equals("243x134"))
                {
                    return "0243x0134/";
                }
                else
                {
                    sizeFile = fileName.Split('x');
                    width = float.Parse(sizeFile[0].Trim());
                    height = float.Parse(sizeFile[1].Trim());
                    // Try resize height = heightBox
                    resizeWidth = width * 134 / height;

                    // Check width resize < widthbox
                    if (resizeWidth < 243)
                    {
                        // Set height image = heightBox
                        resizeHeigh = 134;
                    }
                    else
                    {
                        // Else resize width = widthBox
                        resizeWidth = 243;
                        resizeHeigh = height * 243 / width;
                    }
                }
            }
            else
            {
                return "0243x0134/";
            }
            return string.Format("{0}x{1}{2}", resizeWidth.ToString("0000"), resizeHeigh.ToString("0000"), "/");
        }

        #endregion

        #region Create file CSV

        #region Estimation
        DataSet dsRS3016;
        const int tbRS3016 = 0;
        private ZipFile zip;
        private List<MemoryStream> listStreamCSV;
       
        public MemoryStream ExportPDF(string reportID, System.Data.DataSet dataSource)
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream streamDenpyo = new MemoryStream();
            MemoryStream streamDenpyoKihonMeisai = new MemoryStream();
            MemoryStream streamDenpyoBuhinMeisai = new MemoryStream();
            dsRS3016 = new DataSet();
            dsRS3016 = dataSource;
            if (this.listStreamCSV == null)
            {
                this.listStreamCSV = new List<MemoryStream>();
            }

            streamDenpyo = OutPutCSV.ExportCSV(dsRS3016.Tables[0], "");
            this.listStreamCSV.Add(streamDenpyo);
            streamDenpyoKihonMeisai = OutPutCSV.ExportCSV(dsRS3016.Tables[1], "");
            this.listStreamCSV.Add(streamDenpyoKihonMeisai);
            streamDenpyoBuhinMeisai = OutPutCSV.ExportCSV(dsRS3016.Tables[2], "");
            this.listStreamCSV.Add(streamDenpyoBuhinMeisai);
            stream = ExportCSVZip(new string[] { "Denpyo", "Denpyo_Kihon_Meisai", "Denpyo_Buhin_Meisai" });

            return stream;
        }
        #endregion

        #region Order
        /// <summary>
        /// Get file CSV 検索データ出力
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRH9080(DataTable listData, ref string error)
        {
            MemoryStream streamReportsCSV = new MemoryStream();
            try
            {
                if (listData != null)
                {
                    if (listData.Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(listData, "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        /// <summary>
        /// Get file CSV 伝票データ検索画面
        /// </summary>
        /// <param name="listOrder"></param>
        /// <param name="isCheck">false:簡易;true:全て</param>
        /// <param name="error"></param>
        /// <returns></returns>
        //public MemoryStream ExportCSVRH9085(DataTable listOrder, bool isCheck , ref string error)
        //{
        //    string[] nameFileCSV = new string[] { "meisai_part", "meisai_all", "header_part", "header_all" };
        //    DataSet result = new DataSet();
        //    MemoryStream streamReportsCSV = new MemoryStream();
        //    MemoryStream headerAll = new MemoryStream();
        //    MemoryStream meisaiAll = new MemoryStream();

        //    if (isCheck)
        //    {
        //        result = GetDataReports(listOrder, ConstantsStpReports.RH9085_meisai_all, "@ListOrderNo", ref error);
        //        nameFileCSV = new string[] { "header_all", "meisai_all" };
        //    }
        //    else
        //    {
        //        result = GetDataReports(listOrder, ConstantsStpReports.RH9085_meisai_part, "@ListOrderNo", ref error);
        //        nameFileCSV = new string[] { "header_part", "meisai_part" };
        //    }
        //    try
        //    {
        //        if (string.IsNullOrEmpty(error))
        //        {
        //            if (result.Tables.Count <= 0)
        //            {
        //                error = "I0007";
        //            }
        //            else if (result.Tables[0].Rows.Count <= 0)
        //            {
        //                error = "I0007";
        //            }
        //            else
        //            {
        //                this.listStreamCSV = new List<MemoryStream>();
        //                headerAll = OutPutCSV.ExportCSV(result.Tables[0], "");
        //                this.listStreamCSV.Add(headerAll);
        //                if (result.Tables.Count >= 2)
        //                {
        //                    meisaiAll = OutPutCSV.ExportCSV(result.Tables[1], "");
        //                    this.listStreamCSV.Add(meisaiAll);
        //                }
        //                streamReportsCSV = ExportCSVZip(nameFileCSV, "伝票データ検索画面");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        error = "E0002";
        //    }
        //    return streamReportsCSV;
        //}

        public MemoryStream ExportCSVRH9085(DataTable listOrder, bool isCheck, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReportsCSV = new MemoryStream();
            if (isCheck)
            {
                result = GetDataReports(listOrder, ConstantsStpReports.RH9085_meisai_all, "@ListOrderNo", ref error);
            }
            else
            {
                result = GetDataReports(listOrder, ConstantsStpReports.RH9085_meisai_part, "@ListOrderNo", ref error);
            }
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(result.Tables[0], "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        /// <summary>
        /// Get file CSV ヘッダレベル出力
        /// </summary>
        /// <param name="listOrder"></param>
        /// <param name="isCheck">false:簡易;true:全て</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRH9086(DataTable listOrder, bool isCheck, ref string error)
        {
            DataSet result = new DataSet();
            MemoryStream streamReportsCSV = new MemoryStream();
            ReportServices reportService = new ReportServices();
            if (isCheck)
            {
                result = GetDataReports(listOrder, ConstantsStpReports.RH9086_header_all, "@ListOrderNo", ref error);
            }
            else
            {
                result = GetDataReports(listOrder, ConstantsStpReports.RH9086_header_part, "@ListOrderNo", ref error);
            }
            try
            {
                if (string.IsNullOrEmpty(error))
                {
                    if (result.Tables.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else if (result.Tables[0].Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(result.Tables[0], "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        /// <summary>
        /// Get file CSV 残高明細出力
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRH9090(DataTable listData, ref string error)
        {
            MemoryStream streamReportsCSV = new MemoryStream();
            try
            {
                if (listData != null)
                {
                    if (listData.Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(listData, "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        /// <summary>
        /// Get file CSV 残高明細・合計出力
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRH9091(DataTable listData, ref string error)
        {
            MemoryStream streamReportsCSV = new MemoryStream();
            try
            {
                if (listData != null)
                {
                    if (listData.Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(listData, "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        /// <summary>
        /// Get file CSV H7050
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public MemoryStream ExportCSVRH7050(DataTable listData, ref string error)
        {
            MemoryStream streamReportsCSV = new MemoryStream();
            try
            {
                if (listData != null)
                {
                    if (listData.Rows.Count <= 0)
                    {
                        error = "I0007";
                    }
                    else
                    {
                        streamReportsCSV = OutPutCSV.ExportCSV(listData, "");
                    }
                }
            }
            catch (Exception)
            {
                error = "E0002";
            }
            return streamReportsCSV;
        }

        #endregion

        public MemoryStream ExportCSVZip(string[] nameCSV)
        {
            // create new zip instance
            this.zip = new ZipFile();
            //Denpyo_yyyyMMddHHmm
            string nameFileDate = System.DateTime.Now.ToString("yyyyMMddHHmm");
            // create new stream
            MemoryStream stream = new MemoryStream();
            int i = 0;
            if (this.listStreamCSV != null)
            {
                foreach (MemoryStream item in this.listStreamCSV)
                {
                    string fileName = nameCSV[i].ToString() + ".csv";
                    this.zip.AddEntry(fileName, nameFileDate, item.ToArray());
                    this.zip.CompressionLevel = CompressionLevel.Default;
                    i++;
                }
            }

            this.zip.Save(stream);

            return stream;
        }

        public MemoryStream ExportCSVZip(string[] nameCSV, string nameFileZip)
        {
            // create new zip instance
            this.zip = new ZipFile();
            //Denpyo_yyyyMMddHHmm
            string nameFileDate = nameFileZip + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmfff");
            // create new stream
            MemoryStream stream = new MemoryStream();
            int i = 0;
            if (this.listStreamCSV != null)
            {
                foreach (MemoryStream item in this.listStreamCSV)
                {
                    string fileName = nameCSV[i].ToString() + ".csv";
                    this.zip.AddEntry(fileName, nameFileDate, item.ToArray());
                    this.zip.CompressionLevel = CompressionLevel.Default;
                    i++;
                }
            }

            this.zip.Save(stream);

            return stream;
        }
        #endregion

        public MemoryStream ExportCsvRC002()
        {
            MemoryStream stream = new MemoryStream();
            DataTable dataResult = new DataTable();
            using (DBManager dbManager = new DBManager(ConstantsStpReports.RC002))
            {
                dataResult = dbManager.GetDataTable();
            }
            stream = OutPutCSV.ExportCSV(dataResult, "ファイルNO,車台番号,タグID");
            return stream;
        }

        public MemoryStream ExportCSV(DataTable dt)
        {
            MemoryStream stream = new MemoryStream();
            stream = OutPutCSV.ExportCSV(dt, "ファイルNO");
            return stream;
        }

        public DataTable ExportPDFRD0020(DataTable table)
        {
            DataTable dt = new DataTable();
            DataTable dataSetResult = new DataTable();

            using (DBManager dbManager = new DBManager(ConstantsStpReports.RD0020))
            {
                dbManager.Add("@list", SqlDbType.Structured, table);
                dataSetResult = dbManager.GetDataTable();
            }

            return dataSetResult;
        }
    }

        public static class OutPutCSV
        {
            public static MemoryStream ExportCSV(DataTable dt, string header = "")
            {
                // Create the CSV file to which grid data will be exported.
                MemoryStream ms = new MemoryStream();

                StreamWriter sw = new StreamWriter(ms, Encoding.GetEncoding("shift-jis"));

                dt = dt == null ? new DataTable() : dt;

                // First we will write the headers.
                int colCount = dt.Columns.Count;

                if (string.IsNullOrEmpty(header))
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        sw.Write("\"" + dt.Columns[i] + "\"");

                        if (i < colCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                }
                else
                {
                    sw.Write(header);
                }

                sw.Write(sw.NewLine);

                // Now write all the rows.
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        sw.Write("" + Sanitize(Convert.ToString(dr[i])) + "");
                        if (i < colCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Flush();
                //ms.Close();
                return ms;
            }

            public static string Sanitize(string stringValue)
            {
                return stringValue == null ? null : stringValue
                    //.RegexReplace("-{2,}", "-")                 
                    //.RegexReplace(@"[*/]+", string.Empty)      
                            .RegexReplace(@"(;|\s)(exec|execute|select|insert|update|delete|create|alter|drop|rename|truncate|backup|restore)\s", string.Empty, RegexOptions.IgnoreCase)
                            .RegexReplace("\r", "")
                            .RegexReplace("\n", "").Trim();
            }

            private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith)
            {
                return Regex.Replace(stringValue, matchPattern, toReplaceWith);
            }

            private static string RegexReplace(this string stringValue, string matchPattern, string toReplaceWith, RegexOptions regexOptions)
            {
                return Regex.Replace(stringValue, matchPattern, toReplaceWith, regexOptions);
            }
        }
    }

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.ReportServices
{
    public interface IDataReports
    {
        #region  Estimation

        #region Export PDF
        /// <summary>
        /// Export reports RS3010 to file PDF 
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "RequestEstimationNo"
        /// -Index [0][1] = value RequestEstimationNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3010(string[,] param, ref string error);

        /// <summary>
        /// Export reports RS3011 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "EstimationNo"
        /// -Index [0][1] = value EstimationNo
        /// -Index [1][0] = "CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3011(string [,] param, ref string error);

        /// <summary>
        /// Export reports RS3012 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "OrderNo"
        /// -Index [0][1] = value OrderNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3012(string[,] param, ref string error);

        /// <summary>
        /// Export reports RS3013 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "OrderNo"
        /// -Index [0][1] = value OrderNo
        /// -Index [1][0] = "CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3013(string [,] param, ref string error);

        /// <summary>
        /// Export reports RS3014 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "DeliveryNo"
        /// -Index [0][1] = value DeliveryNo
        /// -Index [1][0] = "CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3014(string [,] param, ref string error);

        /// <summary>
        /// Export reports RS3015 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "BillingNo"
        /// -Index [0][1] = value BillingNo
        /// -Index [1][0] = "CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS3015(string [,] param, ref string error);

        /// <summary>
        /// Export reports RS7010 to file PDF
        /// </summary>
        /// <param name="listBilling"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRS7010(DataTable listBilling, ref string error);

        /// <summary>
        /// Export CSV RS3016 to file CSV
        /// </summary>
        /// <param name="listOrder"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportCSVRS3016(DataTable listOrder, ref string error);

        
        #endregion

        #region Export CSV
        //MemoryStream ExportCSVRS3016([DataSet], ref string error);
        #endregion

        #endregion

        #region Order

        #region Export PDF
        /// <summary>
        /// Export reports RH2030 to file PDF 
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "requestEstimationNo"
        /// -Index [0][1] = value requestEstimationNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2030(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2031 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "cancelRequestEstimationNo"
        /// -Index [0][1] = value cancelRequestEstimationNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2031(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2032 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "requestEstimationConcurrentOrder"
        /// -Index [0][1] = value requestEstimationConcurrentOrder
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2032(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2033 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "cancelRequestEstimationConcurrentOrder"
        /// -Index [0][1] = value cancelRequestEstimationConcurrentOrder
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2033(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2050 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "orderNo"
        /// -Index [0][1] = value orderNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2050(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2051 to file PDF
        /// </summary>
        /// <param name="dtRH2051">DataSet: 2table</param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2051(DataSet dtRH2051, ref string error);

        /// <summary>
        /// Export reports RH2060 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "orderNo"
        /// -Index [0][1] = value orderNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2060(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2061 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "requestSeibiNo"
        /// -Index [0][1] = value requestSeibiNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2061(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2070 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "cancelOrderNo"
        /// -Index [0][1] = value cancelOrderNo
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2070(string[,] param, ref string error);

        /// <summary>
        /// Export reports RH2080 to file PDF
        /// </summary>
        /// <param name="param">
        /// -Index [0][0] = "OrderNo"
        /// -Index [0][1] = value OrderNo
        /// -Index [1][0] = "CheckTaxRate"
        /// -Index [1][1] = [value = 0: tax exemption; value = 1: with taxes; value # 0 or value # 1: other]
        /// -Index [2][0] = "IsGLV"
        /// -Index [2][1] = [value = 0: does not include components GLV; value = 1: include components GLV; value # 0 or value # 1: other]
        /// </param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH2080(string [,] param, ref string error);

        /// <summary>
        /// Export reports RH4040 to file PDF
        /// </summary>
        /// <param name="listBillingNo"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH4040(DataTable listBillingNo, ref string error);

        /// <summary>
        /// Export reports RH5050 to file PDF
        /// </summary>
        /// <param name="TableKeyFind"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH5050(DataTable TableKeyFind, ref string error);

        /// <summary>
        /// Export reports RH9040 to file PDF
        /// </summary>
        /// <param name="TableContactFubi"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        MemoryStream ExportPDFRH9040(DataTable TableContactFubi, ref string error);
        #endregion

        #region Export CSV

        #endregion

        #endregion
    }
}

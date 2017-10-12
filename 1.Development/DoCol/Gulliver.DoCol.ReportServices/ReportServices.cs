using Gulliver.DoCol.ReportServices.Service;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Gulliver.DoCol.ReportServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportServices
    {
        public static bool IgnoreCertificateErrorHandler(object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Exports the PDF.
        /// </summary>
        /// <param name="reportID">The report identifier.</param>
        /// <param name="dataSource">The data source.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public MemoryStream ExportPDF(string reportID, DataSet dataSource)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler);
            ServicesClient client = new ServicesClient();
            MemoryStream result = client.ExportPDF(reportID, dataSource);
            if (result == null)
            {
                throw new Exception();
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Exports the PDF and return messenger error
        /// </summary>
        /// <param name="reportID">string</param>
        /// <param name="dataSource">DataSet</param>
        /// <param name="messErr">string</param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportPDF(string reportID, DataSet dataSource, ref string messErr)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler);
            ServicesClient client = new ServicesClient();
            MemoryStream result = client.ExportPDFOutput(reportID, dataSource, ref messErr);
            if (result == null)
            {
                throw new Exception();
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Exports the CSV and return messenger error
        /// </summary>
        /// <param name="reportID"></param>
        /// <param name="dataSource"></param>
        /// <param name="messErr"></param>
        /// <returns></returns>
        public MemoryStream ExportCSV(string reportID, DataSet dataSource, ref string messErr)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler);

            ServicesClient client = new ServicesClient();
            MemoryStream result = client.ExportCSVOutput(reportID, dataSource, ref messErr);

            if (result == null)
            {
                throw new Exception();
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Create CSV File
        /// </summary>
        /// <param name="dt">Data Table</param>
        /// <returns>
        /// byte array
        /// </returns>
        public byte[] ExportCSV(DataTable dt, string header = "")
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
                    sw.Write("\"" + Convert.ToString(dr[i]) + "\"");
                    if (i < colCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Flush();
            byte[] bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }

    }
}
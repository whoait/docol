using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Constants
{
    public static class PDFReports
    {
        //Estimation
        public const string RS3010 = "RS3010ReportEstimation.pdf";
        public const string RS3011 = "RS3011ReportEstimation.pdf";
        public const string RS3012 = "RS3012ReportEstimation.pdf";
        public const string RS3013 = "RS3013ReportEstimation.pdf";
        public const string RS3014 = "RS3014ReportEstimation.pdf";
        public const string RS3015 = "RS3015ReportEstimation.pdf";
        public const string RS7010 = "RS7010ReportEstimation.pdf";

        //Order
        static string rH2030 = "RH2030ReportOrder";
        public static string RH2030
        {
            get { return PDFReports.rH2030 + ".pdf"; }
            set { PDFReports.rH2030 = value; }
        }

        static string rH2031 = "RH2031ReportOrder";
        public static string RH2031
        {
            get { return PDFReports.rH2031 + ".pdf"; }
            set { PDFReports.rH2031 = value; }
        }

        static string rH2032 = "RH2032ReportOrder";
        public static string RH2032
        {
            get { return PDFReports.rH2032 + ".pdf"; }
            set { PDFReports.rH2032 = value; }
        }

        static string rH2033 = "RH2033ReportOrder";
        public static string RH2033
        {
            get { return PDFReports.rH2033 + ".pdf"; }
            set { PDFReports.rH2033 = value; }
        }

        static string rH2050 = "RH2050ReportOrder";
        public static string RH2050
        {
            get { return PDFReports.rH2050 + ".pdf"; }
            set { PDFReports.rH2050 = value; }
        }

        static string rH2051 = "RH2051ReportOrder";
        public static string RH2051
        {
            get { return PDFReports.rH2051 + ".pdf"; }
            set { PDFReports.rH2051 = value; }
        }

        static string rH2060 = "RH2060ReportOrder";
        public static string RH2060
        {
            get { return PDFReports.rH2060 + ".pdf"; }
            set { PDFReports.rH2060 = value; }
        }

        static string rH2061 = "RH2061ReportOrder";
        public static string RH2061
        {
            get { return PDFReports.rH2061 + ".pdf"; }
            set { PDFReports.rH2061 = value; }
        }

        static string rH2070 = "RH2070ReportOrder";
        public static string RH2070
        {
            get { return PDFReports.rH2070 + ".pdf"; }
            set { PDFReports.rH2070 = value; }
        }

        static string rH2080 = "RH2080ReportOrder";
        public static string RH2080
        {
            get { return PDFReports.rH2080 + ".pdf"; }
            set { PDFReports.rH2080 = value; }
        }

        static string rH4040 = "RH4040ReportOrder";
        public static string RH4040
        {
            get { return PDFReports.rH4040 + ".pdf"; }
            set { PDFReports.rH4040 = value; }
        }

        static string rH5050 = "RH5050ReportOrder";
        public static string RH5050
        {
            get { return PDFReports.rH5050 + ".pdf"; }
            set { PDFReports.rH5050 = value; }
        }

        static string rH9040 = "RH9040ReportOrder";
        public static string RH9040
        {
            get { return PDFReports.rH9040 + ".pdf"; }
            set { PDFReports.rH9040 = value; }
        }

               
    }
    public static class CSVReports
    {
        //Estimation
        public static string RS3016()
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmm");
            return "Denpyo_" + nameCSV + ".zip";
        }

        //Estimation
        public static string RS91C0()
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmm");
            return "Denpyo_" + nameCSV + ".zip";
        }
        //Order
        public static string RH9080()
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
            //return string.Format("{0}{1}{2}","Data_Ouput_",nameCSV,".csv");
            return string.Format("{0}{1}", "RH9080_検索データ出力", ".csv");
        }
        /// <summary>
        /// Get name CSV
        /// </summary>
        /// <param name="isCheck">false:簡易;true:全て</param>
        /// <returns></returns>
        /// 
        //public static string RH9085()
        //{
        //    string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
        //    return string.Format("{0}{1}{2}", "伝票データ検索画面_", nameCSV, ".zip");
        //}

        public static string RH9085(bool isCheck)
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
            if (isCheck)
            {
                return string.Format("{0}{1}{2}", "Detail_All_", nameCSV, ".csv");
            }
            else
            {
                return string.Format("{0}{1}{2}", "Detail_Part_", nameCSV, ".csv");
            }
        }
        /// <summary>
        /// Get name CSV 
        /// </summary>
        /// <param name="isCheck">false:簡易;true:全て</param>
        /// <returns></returns>
        public static string RH9086(bool isCheck)
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
            if (isCheck)
            {
                return string.Format("{0}{1}{2}", "Header_All_", nameCSV, ".csv");
            }
            else
            {
                return string.Format("{0}{1}{2}", "Header_Parts_", nameCSV, ".csv");
            }
        }
        public static string RH9090()
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
            //return string.Format("{0}{1}{2}","Zandaka_",nameCSV,".csv");
            return string.Format("{0}{1}", "RH9090_残高明細出力", ".csv");
        }
        public static string RH9091()
        {
            string nameCSV = DateTime.Now.ToString("yyyyMMddHHmmfff");
            //return string.Format("{0}{1}{2}", "Zandaka_Summary_", nameCSV, ".csv");
            return string.Format("{0}{1}", "RH9091_残高明細・合計出力", ".csv");
        }

		//Order
		public static string CSVH7050()
		{
			string nameCSV = DateTime.Now.ToString( "yyyyMMddHHmmss" );
			//return string.Format("{0}{1}{2}","Data_Ouput_",nameCSV,".csv");
			return string.Format( "{0}{1}{2}", "H7050_", nameCSV, ".csv" );
		}
    }
}
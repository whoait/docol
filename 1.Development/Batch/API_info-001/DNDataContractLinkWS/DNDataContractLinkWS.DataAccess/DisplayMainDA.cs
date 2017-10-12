using DNDataContractLinkWS.Common;
using DNDataContractLinkWS.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDataContractLinkWS.DataAccess
{
    public class DisplayMainDA
    {
        /// <summary>
        /// Connect DB and get data
        /// </summary>
        /// <param name="input"></param>
        /// <param name="mode"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public List<DisplayMainDTO> GetResponse(string input, int mode, string sqlConn, string commandTimeout)
        {
            SqlConnection sqlCn = new SqlConnection(sqlConn);
            DataTable dt = new DataTable();
            List<DisplayMainDTO> displayMainList = new List<DisplayMainDTO>();
            DisplayMainDTO displayMain = new DisplayMainDTO();
            int timeout = 0;
            try
            {
                timeout = Convert.ToInt16(commandTimeout);
                StringBuilder sb = new StringBuilder();
                try
                {
                    if (sqlCn.State == ConnectionState.Closed)
                    {
                        sqlCn.Open();
                    }
                }
                catch
                {
                    throw new SqlExceptionAPI(ItemIdCS.E002);
                }
                if (mode == 1)
                {
                    #region sql
                    sb.AppendLine(" SELECT M006.SHUPPIN_TOROKU_NO ");
                    sb.AppendLine(" ,A002.CHASSIS_NO ");
                    sb.AppendLine(" ,M006.SHOP_CD ");
                    sb.AppendLine(" ,M006.RAKUSATSU_SHOP_CD ");
                    sb.AppendLine(" ,M006.SEIYAKU_DATE ");
                    sb.AppendLine(" ,A003.BBNO ");
                    sb.AppendLine(" ,S006.CAR_SUB_ID_HANYO_KEY1 '仕入番号' ");
                    sb.AppendLine(" ,A002.DISP_NENSHIKI ");
                    sb.AppendLine(" ,A002.CONV_ORIGINAL_MAKER_NAME ");
                    sb.AppendLine(" ,A002.CONV_ORIGINAL_CAR_NAME ");
                    sb.AppendLine(" ,A002.CONV_ORIGINAL_GRADE_NAME ");
                    sb.AppendLine(" ,A002.KATASHIKI ");
                    sb.AppendLine(" ,A002.CC ");
                    sb.AppendLine(" ,A002.JOSHA_TEIIN_NUM ");
                    sb.AppendLine(" ,CASE A002.KEI_CAR_TYPE WHEN '101' THEN '1' WHEN '901' THEN '0' ELSE NULL END AS '軽自動車フラグ' ");
                    sb.AppendLine(" ,X020.LICENSE_PLATE_CHIMEI + A004.LICENSE_PLATE_GROUP_NO + A004.LICENSE_PLATE_HIRAGANA + A004.LICENSE_PLATE_ICHIREN_SHITEI_NO AS '登録NO' ");
                    sb.AppendLine(" ,A004.SHAKEN_LIMIT_DATE ");
                    sb.AppendLine(" ,A004.SHORUI_LIMIT_DATE ");
                    sb.AppendLine(" ,CASE A004.TOROKU_MASSHO_TYPE WHEN  '101' THEN '0' WHEN '201' THEN '1' ELSE NULL END AS '抹消フラグ' ");
                    sb.AppendLine(" ,M006.CAR_ID ");
                    sb.AppendLine(" ,M006.CAR_SUB_ID ");
                    sb.AppendLine(" ,M006.SEIYAKU_TORIKESHI_FLG 'キャンセルフラグ' ");
                    sb.AppendLine(" FROM M006DN_SHUPPIN M006 (NOLOCK) ");
                    sb.AppendLine(" INNER JOIN A002CAR_BASE A002 (NOLOCK) ");
                    sb.AppendLine(" ON A002.CAR_ID=M006.CAR_ID ");
                    sb.AppendLine(" AND A002.CAR_SUB_ID=M006. CAR_SUB_ID ");
                    sb.AppendLine(" AND A002.SHOP_CD=M006. SHOP_CD ");
                    sb.AppendLine(" AND A002.DELETE_FLG='0' ");
                    sb.AppendLine(" INNER JOIN A003CAR_EXTERNAL_KEY A003 (NOLOCK) ");
                    sb.AppendLine(" ON A003.CAR_ID=M006.CAR_ID ");
                    sb.AppendLine(" AND A003.CAR_SUB_ID=M006. CAR_SUB_ID ");
                    sb.AppendLine(" AND A003.SHOP_CD=M006. SHOP_CD ");
                    sb.AppendLine(" AND A003.DELETE_FLG='0' ");
                    sb.AppendLine(" INNER JOIN A004CAR_DETAIL A004 (NOLOCK) ");
                    sb.AppendLine(" ON A004.CAR_ID=M006.CAR_ID ");
                    sb.AppendLine(" AND A004.CAR_SUB_ID=M006. CAR_SUB_ID ");
                    sb.AppendLine(" AND A004.SHOP_CD=M006. SHOP_CD ");
                    sb.AppendLine(" AND A004.DELETE_FLG='0' ");
                    sb.AppendLine(" LEFT JOIN X020LICENSE_PLATE_CHIMEI X020 (NOLOCK) ");
                    sb.AppendLine(" ON X020.LICENSE_PLATE_CHIMEI_CD= A004.LICENSE_PLATE_CHIMEI_CD ");
                    sb.AppendLine(" AND X020.DELETE_FLG='0' ");
                    sb.AppendLine(" LEFT JOIN S006CAR_SUB_ID_NUMBERING_CONTROL S006(NOLOCK) ");
                    sb.AppendLine(" ON S006.CAR_ID=M006.CAR_ID ");
                    sb.AppendLine(" AND S006.CAR_SUB_ID=M006. CAR_SUB_ID ");
                    sb.AppendLine(" AND S006.SHOP_CD=M006. SHOP_CD ");
                    sb.AppendLine(" AND S006.DELETE_FLG='0' ");
                    sb.AppendLine(" AND S006.SYSTEM_TYPE='005' ");
                    sb.AppendLine(" WHERE M006.TOROKU_STATUS='103' ");
                    sb.AppendLine(" AND M006.DELETE_FLG='0' ");
                    sb.AppendLine(" AND M006.UPDATE_DATE>@INPUT ");
                    #endregion
                }
                using (SqlDataAdapter sqlDa = new SqlDataAdapter())
                {
                    sqlDa.SelectCommand = new SqlCommand(sb.ToString(), sqlCn);
                    sqlDa.SelectCommand.Parameters.AddWithValue("@INPUT", input);
                    sqlDa.SelectCommand.CommandTimeout = timeout;
                    sqlDa.Fill(dt);
                }
                displayMainList = fillData(dt);
            }
            catch (Exception ex)
            {
                if (ItemIdCS.E002.Equals(ex.Message))
                {
                    throw ex;
                }
                else
                {
                    throw new SqlExceptionAPI(ItemIdCS.E999);
                }
            }
            finally
            {
                if (sqlCn != null)
                {
                    sqlCn.Close();
                }
            }
            return displayMainList;
        }

        /// <summary>
        /// Fill data from DataTable to Object
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<DisplayMainDTO> fillData(DataTable dt)
        {
            List<DisplayMainDTO> displayMainList = new List<DisplayMainDTO>();
            DisplayMainDTO displayMain;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    displayMain = new DisplayMainDTO();
                        #region fillData
                            displayMain.O_SHUPPINN_TOROKU_NO = dr["SHUPPIN_TOROKU_NO"].ToString();
                        if (!Convert.IsDBNull(dr["CHASSIS_NO"]))
                            displayMain.O_CHASSIS_NO = dr["CHASSIS_NO"].ToString();
                        if (!Convert.IsDBNull(dr["SHOP_CD"]))
                            displayMain.O_SHOP_CD = dr["SHOP_CD"].ToString();
                        if (!Convert.IsDBNull(dr["RAKUSATSU_SHOP_CD"]))
                            displayMain.O_RAKUSATSU_SHOP_CD = dr["RAKUSATSU_SHOP_CD"].ToString();
                        if (!Convert.IsDBNull(dr["SEIYAKU_DATE"]))
                            displayMain.O_DN_SEIYAKU_DATE = dr["SEIYAKU_DATE"].ToString();
                        if (!Convert.IsDBNull(dr["BBNO"]))
                            displayMain.O_BBNO = dr["BBNO"].ToString();
                        if (!Convert.IsDBNull(dr["仕入番号"]))
                            displayMain.O_SHIIRE_NO = dr["仕入番号"].ToString();
                        if (!Convert.IsDBNull(dr["DISP_NENSHIKI"]))
                            displayMain.O_NENSHIKI = dr["DISP_NENSHIKI"].ToString();
                        if (!Convert.IsDBNull(dr["CONV_ORIGINAL_MAKER_NAME"]))
                            displayMain.O_MAKER_NAME = dr["CONV_ORIGINAL_MAKER_NAME"].ToString();
                        if (!Convert.IsDBNull(dr["CONV_ORIGINAL_CAR_NAME"]))
                            displayMain.O_CAR_NAME = dr["CONV_ORIGINAL_CAR_NAME"].ToString();
                        if (!Convert.IsDBNull(dr["CONV_ORIGINAL_GRADE_NAME"]))
                            displayMain.O_GRADE_NAME = dr["CONV_ORIGINAL_GRADE_NAME"].ToString();
                        if (!Convert.IsDBNull(dr["KATASHIKI"]))
                            displayMain.O_KATASHIKI = dr["KATASHIKI"].ToString();
                        if (!Convert.IsDBNull(dr["CC"]))
                            displayMain.O_CC = (Single?)dr["CC"];
                        if (!Convert.IsDBNull(dr["JOSHA_TEIIN_NUM"]))
                            displayMain.O_JOSHA_TEIIN_NUM = (byte?)dr["JOSHA_TEIIN_NUM"];
                        if (!Convert.IsDBNull(dr["軽自動車フラグ"]))
                            displayMain.O_KEI_CAR_FLG = dr["軽自動車フラグ"].ToString();
                        if (!Convert.IsDBNull(dr["登録NO"]))
                            displayMain.O_TOROKU_NO = dr["登録NO"].ToString();
                        if (!Convert.IsDBNull(dr["SHAKEN_LIMIT_DATE"]))
                            displayMain.O_SHAKEN_LIMIT_DATE = dr["SHAKEN_LIMIT_DATE"].ToString();
                        if (!Convert.IsDBNull(dr["SHORUI_LIMIT_DATE"]))
                            displayMain.O_SHORUI_LIMIT_DATE = dr["SHORUI_LIMIT_DATE"].ToString();
                        if (!Convert.IsDBNull(dr["抹消フラグ"]))
                            displayMain.O_MASSHO_FLG = dr["抹消フラグ"].ToString();
                        if (!Convert.IsDBNull(dr["CAR_ID"]))
                            displayMain.O_CAR_ID = dr["CAR_ID"].ToString();
                        if (!Convert.IsDBNull(dr["CAR_SUB_ID"]))
                            displayMain.O_CAR_SUB_ID = dr["CAR_SUB_ID"].ToString();
                        if (!Convert.IsDBNull(dr["キャンセルフラグ"]))
                            displayMain.O_CANCEL_FLG = dr["キャンセルフラグ"].ToString(); 
                        #endregion
                    displayMainList.Add(displayMain);
                }
            }
            return displayMainList;
        }
    }
}

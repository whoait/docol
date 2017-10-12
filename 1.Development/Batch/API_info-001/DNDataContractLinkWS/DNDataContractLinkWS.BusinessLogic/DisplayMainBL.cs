using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNDataContractLinkWS.DTO;
using DNDataContractLinkWS.DataAccess;
using DNDataContractLinkWS.Common;
using System.Globalization;

namespace DNDataContractLinkWS.BusinessLogic
{
    public class DisplayMainBL
    {
        /// <summary>
        /// Get response data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sqlConn"></param>
        /// <returns></returns>
        public object GetResponse(RequestDTO request, string sqlConn, string maxDays, string commandTimeout)
        {
            ResponseDTO response = new ResponseDTO();
            ResponseFailDTO responseFail = new ResponseFailDTO();
            List<DisplayMainDTO> displayMainList = new List<DisplayMainDTO>();
            DisplayMainDA displayMainDA = new DisplayMainDA();
            string input = "";
            try
            {
                int mode = setMode(request, out input, maxDays);
                if (mode != 0)
                {
                    displayMainList = displayMainDA.GetResponse(input, mode, sqlConn, commandTimeout);
                    if (displayMainList.Count > 0)
                    {
                        response.O_DN_SEIYAKU_LIST = displayMainList;
                        response.O_RETURN_CD = ItemIdCS.O_RETURN_CD_SUCCESS;
                    }
                    else
                    {
                        //Data error
                        throw new SqlExceptionAPI(ItemIdCS.E004);
                    }
                }
                else
                {
                    //Parameter error
                    throw new SqlExceptionAPI(ItemIdCS.E003);
                }
            }
            catch (Exception ex)
            {
                //Timeout Error
                if (ItemIdCS.E001.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E001;
                    responseFail.O_ERROR_INFO = MsgCS.E001;
                    return responseFail;
                }
                //DB Connection Error
                else if (ItemIdCS.E002.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E002;
                    responseFail.O_ERROR_INFO = MsgCS.E002;
                    return responseFail;
                }
                //Parameter error
                else if (ItemIdCS.E003.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E003;
                    responseFail.O_ERROR_INFO = MsgCS.E003;
                    return responseFail;
                }
                //Data error
                else if (ItemIdCS.E004.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E004;
                    responseFail.O_ERROR_INFO = MsgCS.E004;
                    return responseFail;
                }
                //Format error
                else if (ItemIdCS.E005.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E005;
                    responseFail.O_ERROR_INFO = MsgCS.E005;
                    return responseFail;
                }
                //Over error
                else if (ItemIdCS.E006.Equals(ex.Message))
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E006;
                    responseFail.O_ERROR_INFO = String.Format(MsgCS.E006, maxDays);
                    return responseFail;
                }
                //System error
                else
                {
                    responseFail.O_RETURN_CD = ItemIdCS.O_RETURN_CD_FAIL;
                    responseFail.O_ERROR_CD = ItemIdCS.E999;
                    responseFail.O_ERROR_INFO = MsgCS.E999;
                    return responseFail;
                }
            }
            return response;
        }

        /// <summary>
        /// Check mode
        /// </summary>
        /// <param name="request"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private int setMode(RequestDTO request, out string input, string maxDays)
        {
            int modeNum = 0;
            input = "";
            if (!string.IsNullOrEmpty(request.I_LINK_CD))
            {
                //Check mode Update
                if (request.I_LINK_CD.Equals(ItemIdCS.I_LINK_CD_UPDATE_DATE))
                {
                    if (string.IsNullOrEmpty(request.I_UPDATE_DATE))
                    {
                        //Parameter error
                        throw new SqlExceptionAPI(ItemIdCS.E003);
                    }
                    else
                    {
                        string[] format = { "yyyy/MM/dd HH:mm:ss" };
                        DateTime input_date;
                        if (DateTime.TryParseExact(request.I_UPDATE_DATE, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out input_date))
                        {
                            DateTime maxDate = DateTime.Now.AddDays(-Convert.ToInt16(maxDays));
                            DateTime check = new DateTime(maxDate.Year, maxDate.Month, maxDate.Day, 0, 0, 0);
                            int result = DateTime.Compare(input_date, check);
                            if (result < 0)
                            {
                                //Over error
                                throw new SqlExceptionAPI(ItemIdCS.E006);
                            }
                            else
                            {
                                modeNum = 1;
                                input = request.I_UPDATE_DATE;
                            }
                        }
                        else
                        {
                            //Format error
                            throw new SqlExceptionAPI(ItemIdCS.E005);
                        }
                    }
                }
                
            }

            return modeNum;
        }
    }
}

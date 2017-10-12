//---------------------------------------------------------------------------
// System			: Gulliver
// Designer			: DatNT
// Programmer		: DatNT
// Created Date		: 2013/08/01
// Comment			:
#region ----------< History >------------------------------------------------
// ID				: 00x
// Designer			:
// Programmer		:
// Updated Date		:
// Comment			:
#endregion

using Gulliver.DoCol.Constants;
using Gulliver.DoCol.DataAccess.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Gulliver.DoCol.DataAccess
{
    public static partial class UtilityDa
    {
        public static void LogSave(string logDiv,
                                    string method,
                                    string loginUser,
                                    string loginStore,
                                    string ipAddress,
                                    string logMessage,
                                    string detail)
        {
            try
            {
                using (DBManager dbManager = new DBManager("stp_Log_Save"))
                {
                    dbManager.Add("@LogDiv", logDiv);
                    dbManager.Add("@Method", method);
                    dbManager.Add("@LoginUser", loginUser);
                    dbManager.Add("@LoginStore", loginStore);
                    dbManager.Add("@IPAddress", ipAddress);
                    dbManager.Add("@LogMessage", logMessage);
                    dbManager.Add("@Detail", detail);
                    dbManager.ExecuteNonQuery();
                    // safe
                    DBManager.CommitTransaction();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

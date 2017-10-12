using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNDataContractLinkWS.Common
{
    public class ItemIdCS
    {
        /// <summary>Timeout Error</summary>
        public const string E001 = "E001";
        /// <summary>DB Connection Error</summary>
        public const string E002 = "E002";
        /// <summary>Parameter error</summary>
        public const string E003 = "E003";
        /// <summary>Data error</summary>
        public const string E004 = "E004";
        /// <summary>Format error</summary>
        public const string E005 = "E005";
        /// <summary>Over error</summary>
        public const string E006 = "E006";
        /// <summary>System error</summary>
        public const string E999 = "E999";

        /// <summary>INPUT : Mode Link</summary>
        public const string I_LINK_CD_UPDATE_DATE = "101";
        /// <summary>OUTPUT : Success</summary>
        public const string O_RETURN_CD_SUCCESS = "001";
        /// <summary>OUTPUT : error</summary>
        public const string O_RETURN_CD_FAIL = "999";
    }
}

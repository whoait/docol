using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNDataContractLinkWS.Common
{
    public class MsgCS
    {
        /// <summary>Timeout Error</summary>
        public const string E001 = "クエリ実行でのタイムアウト";
        /// <summary>DB Connection Error</summary>
        public const string E002 = "DBへの接続失敗";
        /// <summary>Parameter error</summary>
        public const string E003 = "実行パラメータが不正";
        /// <summary>Data error</summary>
        public const string E004 = "指定した車輌データは存在しません";
        /// <summary>Format error</summary>
        public const string E005 = "正しい日時を指定して下さい";
        /// <summary>Over error</summary>
        public const string E006 = "指定した更新日時が古過ぎます。本日より{0}日以内の日時を指定して下さい。";
        /// <summary>System error</summary>
        public const string E999 = "検知できない異常終了";
    }
}

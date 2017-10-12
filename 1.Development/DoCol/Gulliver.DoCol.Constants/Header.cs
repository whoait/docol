using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Constants
{
    //Format CSV for mode 6
    public static class Import1
    {
        public const string FileName = "FROM_HD_UKE";
        public const string Col0 = "ファイルNO";
        public const string Col1 = "車両区分";
        public const string Col2 = "自動車登録番号又は車両番号";
        public const string Col3 = "標板の枚数及び大きさ";
        public const string Col4 = "車台番号";
        public const string Col5 = "原動模型式";
        public const string Col6 = "帳票種別";
    }
    //Format CSV for mode 7
    public static class Import2
    {
        public const string FileName = "FROM_HD_SHAKEN";
        public const string Col0 = "車両区分";
        public const string Col1 = "自動車登録番号又は車両番号";
        public const string Col2 = "標板の枚数及び大きさ";
        public const string Col3 = "車台番号";
        public const string Col4 = "原動模型式";
        public const string Col5 = "帳票種別";
    }
    //Format CSV for mode 8
    public static class Import3
    {
        public const string FileName = "FROM_HD_RFID";
        public const string Col0 = "RFIDキー";
    }
    //Format CSV for mode 9
    public static class Import4
    {
        public const string Col0 = "依頼日";
        public const string Col1 = "店舗コード";
        public const string Col2 = "現車場所";
        public const string Col3 = "車名";
        public const string Col4 = "車台番号";
        public const string Col5 = "自社名/抹消";
        public const string Col6 = "商品部備考";
        public const string Type1 = "抹消依頼";
        public const string Type2 = "自社名依頼";
    }
}

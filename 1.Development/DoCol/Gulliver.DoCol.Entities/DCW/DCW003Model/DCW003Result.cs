using Gulliver.DoCol.Constants;
using Gulliver.DoCol.DataValidation;
using Gulliver.DoCol.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW003Model
{
    public class DCW003Result
	{
		/// <summary>
		/// 書類管理番号
		/// </summary>
		public string DocControlNo { get; set; }

		/// <summary>
		/// 仕入DN出品番号
		/// </summary>
		public string ShiireShuppinnTorokuNo { get; set; }

		/// <summary>
		/// 売上DN出品番号
		/// </summary>
        [EXNumberic(MessageCd.W0001)]
		public string UriageShuppinnTorokuNo { get; set; }

		/// <summary>
		/// 車台番号
		/// </summary>
		public string ChassisNo { get; set; }

		/// <summary>
		/// 出品店情報
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 落札店情報
		/// </summary>
		public string RakusatsuShopName { get; set; }

		/// <summary>
		/// 仕入AA会場
		/// </summary>
		public string ShiireAaKaijo { get; set; }

		/// <summary>
		/// 売上AA会場
		/// </summary>
		public string UriageAaKaijo { get; set; }

		/// <summary>
		/// 年式
		/// </summary>
		public string Nenshiki { get; set; }

		/// <summary>
		/// 車輌区分
		/// </summary>
		public string KeiCarFlg { get; set; }

		/// <summary>
		/// AA開催日
		/// </summary>
		public DateTime? AaKaisaiDate { get; set; }

		/// <summary>
		/// メーカー
		/// </summary>
		public string MakerName { get; set; }

		/// <summary>
		/// 車名
		/// </summary>
		public string CarName { get; set; }

		/// <summary>
		/// グレード
		/// </summary>
		public string GradeName { get; set; }

		/// <summary>
		/// AA番号
		/// </summary>
		public string AaShuppinNo { get; set; }

		/// <summary>
		/// DN成約日
		/// </summary>
		public DateTime? DnSeiyakuDate { get; set; }

		/// <summary>
		/// 型式
		/// </summary>
		public string Katashiki { get; set; }

		/// <summary>
		/// 書類区分
		/// </summary>
		public string MasshoFlg { get; set; }

		/// <summary>
		/// 自社名区分
		/// </summary>
		public string JishameiFlg { get; set; }

		/// <summary>
		/// 書類ステータス
		/// </summary>
		public string DocStatus { get; set; }

		/// <summary>
		/// 登録ナンバー
		/// </summary>
		public string TorokuNo { get; set; }

		/// <summary>
		/// 書類有効期限
		/// </summary>
		public DateTime? ShoruiLimitDate { get; set; }

		/// <summary>
		/// ファイル番号
		/// </summary>
		public string FileNo { get; set; }

		/// <summary>
		/// 仕入番号
		/// </summary>
		public string ShiireNo { get; set; }

		/// <summary>
		/// 車検満了日
		/// </summary>
		public DateTime? ShakenLimitDate { get; set; }

		/// <summary>
		/// 書類入庫日
		/// </summary>
		public DateTime? DocNyukoDate { get; set; }

		/// <summary>
		/// 自社名依頼日
		/// </summary>
		public DateTime? JishameiIraiShukkoDate { get; set; }

		/// <summary>
		/// 抹消依頼日
		/// </summary>
		public DateTime? MasshoIraiShukkoDate { get; set; }

		/// <summary>
		/// 書類出庫日
		/// </summary>
		public DateTime? DocShukkoDate { get; set; }

		/// <summary>
		/// 自社名完了日
		/// </summary>
		public DateTime? JishameiKanryoNyukoDate { get; set; }

		/// <summary>
		/// 抹消完了日
		/// </summary>
		public DateTime? MasshoKanryoNyukoDate { get; set; }

		/// <summary>
		/// メモ
		/// </summary>
		public string Memo { get; set; }

		public DateTime? MeihenShakenTorokuDate { get; set; }

		public string RacNo { get; set; }

		public string ReportType { get; set; }

		public long RowVersion { get; set; }
        public string UriageCancelFlg { get; set; }
        public string ShiireCancelFlg { get; set; }
        public int RowCount { get; set; }
	}
}

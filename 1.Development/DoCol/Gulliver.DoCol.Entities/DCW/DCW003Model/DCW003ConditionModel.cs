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
    public class DCW003ConditionModel : CmnPagingModel
	{
		/// <summary>
		/// 出品登録番号
		/// </summary>
		public string ShuppinnTorokuNo { get; set; }

		/// <summary>
		/// 車台番号
		/// </summary>
		public string ChassisNo { get; set; }

		/// <summary>
		/// DN - AA
		/// </summary>
		public string ShohinType { get; set; }

		/// <summary>
		/// 出品店舗
		/// </summary>
		public string ShopCd { get; set; }

		/// <summary>
		/// 出品店舗名称
		/// </summary>
		public string ShopName { get; set; }

		/// <summary>
		/// 落札店舗
		/// </summary>
		public string RakusatsuShopCd { get; set; }

		/// <summary>
		/// 落札店舗名称
		/// </summary>
		public string RakusatsuShopName { get; set; }

		/// <summary>
		/// 書類ステータス - 保管中
		/// </summary>
		public int DocStatus102 { get; set; }

		/// <summary>
		/// 書類ステータス - 自社名中
		/// </summary>
		public int DocStatus103 { get; set; }

		/// <summary>
		/// 書類ステータス - 抹消中
		/// </summary>
		public int DocStatus104 { get; set; }

		/// <summary>
		/// 書類ステータス - 保管なし(発送済)
		/// </summary>
		public int DocStatus105 { get; set; }

		/// <summary>
		/// ファイル番号
		/// </summary>
		public string FileNo { get; set; }

		/// <summary>
		/// 車輌区分 - 普通車
		/// </summary>
		public string KeiCarFlg0 { get; set; }

		/// <summary>
		/// 車輌区分 - 軽
		/// </summary>
		public string KeiCarFlg1 { get; set; }

		/// <summary>
		/// 書類区分 - 自社名フラグ
		/// </summary>
		public string JishameiFlg { get; set; }

		/// <summary>
		/// 書類区分 - 抹消フラグ
		/// </summary>
		public string MasshoFlg { get; set; }

		/// <summary>
		/// 成約日/落札開催日 - START
		/// </summary>
        
		public DateTime? AaDnSeiyakuDateStart { get; set; }

		/// <summary>
		/// 成約日/落札開催日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("AaDnSeiyakuDateStart", MessageCd.E0007, "成約日/落札開催日_TO\t成約日/落札開催日_FROM")]
		public DateTime? AaDnSeiyakuDateEnd { get; set; }

		/// <summary>
		/// 書類有効期限 - START
		/// </summary>
		public DateTime? ShoruiLimitDateStart { get; set; }

		/// <summary>
		/// 書類有効期限 - END
		/// </summary>
        [EXCompareDateForm("ShoruiLimitDateStart", MessageCd.E0007, "書類有効期限_TO\t書類有効期限_FROM")]
		public DateTime? ShoruiLimitDateEnd { get; set; }

		/// <summary>
		/// 書類入庫日 - START
		/// </summary>
		public DateTime? DocNyukoDateStart { get; set; }

		/// <summary>
		/// 書類入庫日 - END
		/// </summary>
        [EXCompareDateForm("DocNyukoDateStart", MessageCd.E0007, "書類入庫日_TO\t書類入庫日_FROM")]
		public DateTime? DocNyukoDateEnd { get; set; }

		/// <summary>
		/// 書類出庫日 - START
		/// </summary>
		public DateTime? DocShukkoDateStart { get; set; }

		/// <summary>
		/// 書類出庫日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("DocShukkoDateStart", MessageCd.E0007, "書類出庫日_TO\t書類出庫日_FROM")]
		public DateTime? DocShukkoDateEnd { get; set; }

		/// <summary>
		/// 自社名依頼出庫日 - START
		/// </summary>
		public DateTime? JishameiIraiShukkoDateStart { get; set; }

		/// <summary>
		/// 自社名依頼出庫日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("JishameiIraiShukkoDateStart", MessageCd.E0007, "自社名依頼出庫日_TO\t自社名依頼出庫日_FROM")]
		public DateTime? JishameiIraiShukkoDateEnd { get; set; }

		/// <summary>
		/// 自社名完了出庫日 - START
		/// </summary>
		public DateTime? JishameiKanryoNyukoDateStart { get; set; }

		/// <summary>
		/// 自社名完了出庫日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("JishameiKanryoNyukoDateStart", MessageCd.E0007, "自社名完了出庫日_TO\t自社名完了出庫日_FROM")]
		public DateTime? JishameiKanryoNyukoDateEnd { get; set; }

		/// <summary>
		/// 抹消依頼出庫日 - START
		/// </summary>
		public DateTime? MasshoIraiShukkoDateStart { get; set; }

		/// <summary>
		/// 抹消依頼出庫日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("MasshoIraiShukkoDateStart", MessageCd.E0007, "抹消依頼出庫日_TO\t抹消依頼出庫日_FROM")]
		public DateTime? MasshoIraiShukkoDateEnd { get; set; }

		/// <summary>
		/// 抹消完了出庫日 - START
		/// </summary>
		public DateTime? MasshoKanryoNyukoDateStart { get; set; }

		/// <summary>
		/// 抹消完了出庫日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("MasshoKanryoNyukoDateStart", MessageCd.E0007, "抹消完了出庫日_TO\t抹消完了出庫日_FROM")]
		public DateTime? MasshoKanryoNyukoDateEnd { get; set; }

		/// <summary>
		/// 車検満了日 - START
		/// </summary>
		public DateTime? ShakenLimitDateStart { get; set; }

		/// <summary>
		/// 車検満了日 - END
		/// </summary>
        /// 
        [EXCompareDateForm("ShakenLimitDateStart", MessageCd.E0007, "車検満了日_TO\t車検満了日_FROM")]
		public DateTime? ShakenLimitDateEnd { get; set; }

		/// <summary>
		/// Type search with shadai no
		/// </summary>
		public string RadioType { get; set; }

		public int ModeImport { get; set; }

        public int DocumentNomalCar { get; set; }
        public int DocumentNotNomalCar { get; set; }
        public int DocumentStocktaking { get; set; }
        public int RowCount { get; set; }
	}
}

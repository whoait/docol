using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW003Model
{
	public class DCW003Update
	{
		/// <summary>
		/// 書類管理番号
		/// </summary>
		public string DocControlNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UriageShuppinTorokuNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ChassisNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string RackNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FileNo { get; set; }

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
	}
}

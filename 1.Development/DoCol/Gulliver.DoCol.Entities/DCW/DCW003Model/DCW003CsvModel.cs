using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW003Model
{
	public class DCW003CsvModel
	{
		public int ID { get; set; }
		/// <summary>
		/// ラックファイルNO
		/// </summary>
		public string RacFileNo { get; set; }
		/// <summary>
		/// 軽自動車フラグ
		/// </summary>
		public string KeiCarFlg { get; set; }
		/// <summary>
		/// 自動車登録番号又は車両番号
		/// </summary>
		public string TorokuNo { get; set; }
		/// <summary>
		/// 標板の枚数及び大きさ
		/// </summary>
		public string HyobanType { get; set; }
		/// <summary>
		/// 車台番号
		/// </summary>	
		public string ChassisNo { get; set; }
		/// <summary>
		/// 原動機型式
		/// </summary>	
		public string GendokiKatashiki { get; set; }
		/// <summary>
		/// 帳票種別
		/// </summary>	
		public string ReportType { get; set; }

		public string DocControlNo { get; set; }

		public string IraiDate { get; set; }

		public string ShopCd { get; set; }

		public string GenshaLocation { get; set; }

		public string CarName { get; set; }

		public string JMType { get; set; }

		public string Note { get; set; }
	}
}

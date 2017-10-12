using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.Common
{
	public class StatusLinkPriceModel
	{
		// condition
		public string EstimationDenpyoNo { get; set; }
		public bool? FlgInputWorkContent { get; set; }
		public string StatusID { get; set; }
		public bool? FlgCancelSeibi { get; set; }
		public bool? FlgFactotyActive { get; set; }
		public bool? FlgUseSystem { get; set; }
		public string ModeLogin { get; set; }
		public string ShopLoginID { get; set; }
		public string ShopOrderID { get; set; }
		public string ShopPayID { get; set; }
		public string CarType { get; set; }
		public bool? InputEstimationFlg { get; set; }
		public bool? InputOrderFlg { get; set; }
		public bool? FlgCancelRequest { get; set; }
		public bool? FlgTax { get; set; }
		public string BillingDenpyoNo { get; set; }

		// set paramter for link (updating...)
		public string OrderNo { get; set; }
		public int? HonbuJuhachuCarId { get; set; }
		public string CarID { get; set; }
		public string CarSubId { get; set; }
		public string JuchuNo { get; set; }
		public string BBNO { get; set; }
		public string TorihikiKbn { get; set; }
		public string SagyoIraiKbn { get; set; }
		public string ShopOwnerID { get; set; }
		public string KaiShaCode { get; set; }
		public string BushoCode { get; set; }

		public DateTime? EstimationRequestDate { get; set; }
		public DateTime? EstimationHakkoDate { get; set; }
		public DateTime? OrderDate { get; set; }
		public DateTime? NoshaDate { get; set; }
		public DateTime? BillingHonbuSyoninDate { get; set; }

		// left join T_ESTIMATION get status save daff in H2050
		public short? ShoninStatus { get; set; }

		// input work content H2010
		public DateTime? InputDate { get; set; }

		// Date type
		public List<string> ListDateType { get; set; }
	}
}

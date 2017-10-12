using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.Common
{
	public class IconModel
	{
		/// <summary>
		/// get from [T_TORIHIKI].order_no
		/// </summary>
		public string OrderNo { get; set; }

		/// <summary>
		/// if exists order get from [T_TORIHIKI].order_status else get by condition
		/// </summary>
		public string StatusID { get; set; }

		/// <summary>
		/// get from [T_CAR_INFO].input_sagyo_naiyo_flg
		/// </summary>
		public bool? FlgInputWorkContent { get; set; }

		/// <summary>
		/// if exists order get from [T_TORIHIKI].seibi_cancel_flg else get from [T_CAR_INFO].cancel_flg
		/// </summary>
		public bool? FlgCancelSeibi { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].order_cancel_flag
		/// </summary>
		public bool? FlgCancelOrder { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].irai_cancel_flag
		/// </summary>
		public bool? FlgCancelRequestEstimation { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].input_estimation_flg
		/// </summary>
		public bool? InputEstimationFlg { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].input_order_flg
		/// </summary>
		public bool? InputOrderFlg { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].ninteikojo_flg
		/// </summary>
		public bool? FlgFactotyActive { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].system_donyu_flg 
		/// </summary>
		public bool? FlgUseSystem { get; set; }

		/// <summary>
		/// get from the screen
		/// </summary>
		public string Stage { get; set; }

		/// <summary>
		/// if exists order get from [T_TORIHIKI].rikuso_tochaku_plan_date else get from [T_CAR_INFO].rikuso_tochaku_plan_date
		/// </summary>
		public DateTime? RikusoTochakuPlanDate { get; set; }

		/// <summary>
		/// if exists order get from [T_TORIHIKI].rikuso_tochaku_date else get from [T_CAR_INFO].rikuso_tochaku_date
		/// </summary>
		public DateTime? RikusoTochakuDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].estimation_request_date
		///// </summary>
		//public DateTime? EstimationRequestDate { get; set; }

		///// <summary>
		///// get from [T_INPUT_WORK_CONTENT].input_date
		///// </summary>
		//public DateTime? InputDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].order_date
		///// </summary>
		//public DateTime? OrderDate { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].kojo_nyuko_date
		/// </summary>
		public DateTime? KojoNyukoDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].estimation_hakko_date
		///// </summary>
		//public DateTime? EstimationHakkoDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].estimation_inpput_date
		///// </summary>
		//public DateTime? EstimationInpputDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].order_input_date
		///// </summary>
		//public DateTime? OrderInputDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].delivery_input_date
		///// </summary>
		//public DateTime? DeliveryInputDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].delivery_hakko_date
		///// </summary>
		//public DateTime? DeliveryHakkoDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].delivery_tencho_syonin_date
		///// </summary>
		//public DateTime? DeliveryTenchoSyoninDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].delivery_honbu_syonin_date
		///// </summary>
		//public DateTime? DeliveryHonbuSyoninDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].billing_input_date
		///// </summary>
		//public DateTime? BillingInputDate { get; set; }

		///// <summary>
		///// get from [T_TORIHIKI].billing_honbu_syonin_date
		///// </summary>
		//public DateTime? BillingHonbuSyoninDate { get; set; }

		// set paramter for link (updating...)
		/// <summary>
		/// get from [T_CAR_INFO].honbu_juhacchu_car_id
		/// </summary>
		public int? HonbuJuhachuCarId { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].juchu_no
		/// </summary>
		public string JuchuNo { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].taisho_kbn
		/// </summary>
		public string CarType { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].torihiki_kbn
		/// </summary>
		public string TorihikiKbn { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].sagyo_irai_kbn
		/// </summary>
		public string SagyoIraiKbn { get; set; }

		/// <summary>
		/// get from [T_CAR_INFO].tempo_cd
		/// </summary>
		public string ShopOwnerID { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].order_tempo_cd
		/// </summary>
		public string ShopOrderID { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].kaisha_cd
		/// </summary>
		public string KaiShaCode { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].busho_cd
		/// </summary>
		public string BushoCode { get; set; }

		/// <summary>
		/// get from [T_TORIHIKI].billing_denpyo_no
		/// </summary>
		public string BillingDenpyoNo { get; set; }

		/// <summary>
		/// IconModel
		/// </summary>
		/// <param name="orderNo"></param>
		/// <param name="statusID"></param>
		/// <param name="flgInputWorkContent"></param>
		/// <param name="flgCancelSeibi"></param>
		/// <param name="flgCancelOrder"></param>
		/// <param name="flgCancelRequestEstimation"></param>
		/// <param name="inputEstimationFlg"></param>
		/// <param name="inputOrderFlg"></param>
		/// <param name="flgFactotyActive"></param>
		/// <param name="flgUseSystem"></param>
		/// <param name="rikusoTochakuPlanDate"></param>
		/// <param name="rikusoTochakuDate"></param>
		/// <param name="kojoNyukoDate"></param>
		/// <param name="honbuJuhachuCarId"></param>
		/// <param name="juchuNo"></param>
		/// <param name="carType"></param>
		/// <param name="torihikiKbn"></param>
		/// <param name="sagyoIraiKbn"></param>
		/// <param name="shopOwnerID"></param>
		/// <param name="shopOrderID"></param>
		/// <param name="kaiShaCode"></param>
		/// <param name="bushoCode"></param>
		/// <param name="billingDenpyoNo"></param>
		public IconModel( string orderNo, string statusID, bool? flgInputWorkContent, bool? flgCancelSeibi, bool? flgCancelOrder,
			bool? flgCancelRequestEstimation, bool? inputEstimationFlg, bool? inputOrderFlg, bool? flgFactotyActive, bool? flgUseSystem,
			DateTime? rikusoTochakuPlanDate, DateTime? rikusoTochakuDate, DateTime? kojoNyukoDate, int? honbuJuhachuCarId, string juchuNo,
			string carType, string torihikiKbn, string sagyoIraiKbn, string shopOwnerID, string shopOrderID, string kaiShaCode, string bushoCode, string billingDenpyoNo )
		{
			this.OrderNo = orderNo;
			this.StatusID = statusID;
			this.FlgInputWorkContent = flgInputWorkContent;
			this.FlgCancelSeibi = flgCancelSeibi;
			this.FlgCancelOrder = flgCancelOrder;
			this.FlgCancelRequestEstimation = flgCancelRequestEstimation;
			this.InputEstimationFlg = inputEstimationFlg;
			this.InputOrderFlg = inputOrderFlg;
			this.FlgFactotyActive = flgFactotyActive;
			this.FlgUseSystem = flgUseSystem;
			this.RikusoTochakuPlanDate = rikusoTochakuPlanDate;
			this.RikusoTochakuDate = rikusoTochakuDate;
			this.KojoNyukoDate = kojoNyukoDate;
			this.HonbuJuhachuCarId = honbuJuhachuCarId;
			this.JuchuNo = juchuNo;
			this.CarType = carType;
			this.TorihikiKbn = torihikiKbn;
			this.SagyoIraiKbn = sagyoIraiKbn;
			this.ShopOrderID = shopOrderID;
			this.KaiShaCode = kaiShaCode;
			this.BushoCode = bushoCode;
			this.BillingDenpyoNo = billingDenpyoNo;
		}
	}
}

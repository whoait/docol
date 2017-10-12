using Gulliver.DoCol.Constants;
using Gulliver.DoCol.DataAccess.DCW;
using Gulliver.DoCol.Entities.DCW.DCW003Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gulliver.DoCol.BusinessServices.DCW
{
	public class DCW003Services : BaseServices
	{
		//Initial data access
		private DCW003Da da = new DCW003Da();

		#region .GET

		/// <summary>
		/// Get master fuzokuhin
		/// </summary>
		/// <returns></returns>
		public List<DCW003FuzokuhinMaster> DCW003GetFuzokuhinMaster()
		{
			return da.DCW003GetMasterFuzokuhin();
		}

		/// <summary>
		/// Search with condition
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="lstFuzokuhinMaster"></param>
		/// <param name="lstResult"></param>
		/// <param name="lstUketori"></param>
		public void DCW003Search( DCW003ConditionModel condition, List<DCW003FuzokuhinMaster> lstFuzokuhinMaster,
									out List<DCW003Result> lstResult, out List<DCW003Uketori> lstUketori, out int rowCount )
		{
			if (condition.DocumentStocktaking.Equals( 1 ))
			{
				condition.KeiCarFlg0 = NUMBER.NUM_0;
				condition.KeiCarFlg1 = NUMBER.NUM_0;
				condition.ShohinType = null;
			}
			if (condition.DocumentNomalCar.Equals( 1 ) || condition.DocumentNotNomalCar.Equals( 1 ))
			{
				condition.ShohinType = DCW003Constant.SHOHIN_TYPE_DN;
				condition.RadioType = NUMBER.NUM_1;
				condition.DocStatus102 = 1;
				if (condition.DocumentNomalCar.Equals( 1 ))
				{
					condition.KeiCarFlg0 = NUMBER.NUM_1;
				}
				if (condition.DocumentNotNomalCar.Equals( 1 ))
				{
					condition.KeiCarFlg1 = NUMBER.NUM_1;
				}
			}

			lstUketori = new List<DCW003Uketori>();
			if (!string.IsNullOrWhiteSpace( condition.ShuppinnTorokuNo ))
			{
				condition.ShuppinnTorokuNo = condition.ShuppinnTorokuNo.Replace( Environment.NewLine, "," );
			}

			lstResult = da.DCW003SearchCondition( condition, out rowCount );

			if (lstResult.Count > 0)
			{
				List<string> lstDocControlNo = new List<string>();
				foreach (var item in lstResult)
				{
					lstDocControlNo.Add( item.DocControlNo );
				}

				List<DCW003UketoriDetail> lstUketoriDetail = new List<DCW003UketoriDetail>();
				lstUketoriDetail = da.DCW003GetUketoriDetail( lstDocControlNo );

				List<DCW003UketoriDetail> lstUketoriDetailReset = new List<DCW003UketoriDetail>();
				lstUketoriDetailReset = ResetUketoriDetail( lstFuzokuhinMaster, lstDocControlNo );
				foreach (var item in lstUketoriDetailReset)
				{
					foreach (var uketori in lstUketoriDetail)
					{
						if (uketori.DocControlNo == item.DocControlNo && uketori.DocFuzoKuhinCd == item.DocFuzoKuhinCd)
						{
							item.IsChecked = 1;
							item.Note = uketori.Note;
						}
					}
				}

				List<DCW003Uketori> lstUketoriTemp = new List<DCW003Uketori>();
				List<DCW003UketoriDetail> lstUketoriDetailTemp;
				foreach (var item in lstResult)
				{
					lstUketoriDetailTemp = new List<DCW003UketoriDetail>();
					lstUketoriDetailTemp = lstUketoriDetailReset.Where( x => x.DocControlNo == item.DocControlNo ).ToList();
					lstUketoriTemp.Add( new DCW003Uketori
					{
						DocControlNo = item.DocControlNo,
						AaKaisaiDate = item.AaKaisaiDate,
						ChassisNo = item.ChassisNo,
						DnSeiyakuDate = item.DnSeiyakuDate,
						JishameiFlg = item.JishameiFlg,
						KeiCarFlg = item.KeiCarFlg,
						ShakenLimitDate = item.ShakenLimitDate,
						ShiireShuppinnTorokuNo = item.ShiireShuppinnTorokuNo,
						ShoruiLimitDate = item.ShoruiLimitDate,
						TorokuNo = item.TorokuNo,
						UriageShuppinnTorokuNo = item.UriageShuppinnTorokuNo,
						MeihenShakenTorokuDate = item.MeihenShakenTorokuDate,
						UketoriDetail = lstUketoriDetailTemp
					} );
				}

				lstUketori = lstUketoriTemp;
			}
		}

		/// <summary>
		/// Get list import
		/// </summary>
		/// <param name="lstDocControlNo"></param>
		/// <param name="lstFuzokuhinMaster"></param>
		/// <param name="lstResult"></param>
		/// <param name="lstUketori"></param>
		public void DCW003GetListImport( string lstDocControlNo, List<DCW003FuzokuhinMaster> lstFuzokuhinMaster, int pageIndex, int pageSize,
										out List<DCW003Result> lstResult, out List<DCW003Uketori> lstUketori, out int rowCount )
		{
			List<string> lst = new List<string>();

			if (!string.IsNullOrEmpty( lstDocControlNo ))
			{
				string[] docControlNo = lstDocControlNo.Split( ',' );
				foreach (var item in docControlNo)
				{
					lst.Add( item );
				}
			}
			lstResult = da.DCW003GetListImport( lst, pageIndex, pageSize, out rowCount );
			lstUketori = GetUketoriDetail( lstFuzokuhinMaster, lst, lstResult );
		}

		/// <summary>
		/// Get Doc Control exist
		/// </summary>
		/// <param name="lstCsv"></param>
		/// <param name="lstFuzokuhinMaster"></param>
		/// <param name="modeSearch"></param>
		/// <param name="lstResult"></param>
		/// <param name="lstUketori"></param>
		public void DCW003GetDocControlExist( List<DCW003CsvModel> lstCsv, List<DCW003FuzokuhinMaster> lstFuzokuhinMaster,
												int pageIndex, int pageSize, int modeSearch,
												out List<DCW003Result> lstResult, out List<DCW003Uketori> lstUketori,
												out string lstImport, out int rowCount )
		{
			string errMsg = string.Empty;
			lstImport = string.Empty;
			List<DCW003CsvModel> lstCsvTemp = new List<DCW003CsvModel>();

			lstResult = da.DCW003GetDocControlExist( lstCsv, pageIndex, pageSize, modeSearch, out rowCount );

			List<string> lst = new List<string>();
			foreach (var item in lstResult)
			{
				lst.Add( item.DocControlNo );
				foreach (var csv in lstCsv)
				{
					if (csv.ChassisNo == item.ChassisNo)
					{
						lstCsvTemp.Add( new DCW003CsvModel
						{
							ChassisNo = csv.ChassisNo,
							GendokiKatashiki = csv.GendokiKatashiki,
							HyobanType = csv.HyobanType,
							KeiCarFlg = csv.KeiCarFlg,
							RacFileNo = csv.RacFileNo,
							ReportType = csv.ReportType,
							TorokuNo = csv.TorokuNo
						} );
						lstImport = lstImport + "," + csv.ChassisNo;

						if (modeSearch == 1)
						{
							if (csv.ReportType == "1")
							{
								item.JishameiFlg = "1";
								item.JishameiKanryoNyukoDate = DateTime.Now;
							}
							else if (csv.ReportType == "2" || csv.ReportType == "4")
							{
								item.MasshoFlg = "1";
								item.MasshoKanryoNyukoDate = DateTime.Now;
							}
							item.ReportType = csv.ReportType;
							item.TorokuNo = csv.TorokuNo;
							item.DocStatus = "102";
						}
					}
				}
			}
			lstUketori = GetUketoriDetail( lstFuzokuhinMaster, lst, lstResult );
			if (lstCsvTemp.Count > 0)
			{
				da.DCW003InsertDocUketoriIf( lstCsvTemp, out errMsg );
			}
		}

		/// <summary>
		/// Get master doc status
		/// </summary>
		/// <returns></returns>
		public List<DCW003DropDownModel> DCW003GetMasterDocStatus()
		{
			return da.DCW003GetMasterDocStatus();
		}

		/// <summary>
		/// Get master 
		/// </summary>
		/// <returns></returns>
		public List<DCW003DropDownModel> DCW003GetMasterYear()
		{
			List<DCW003DropDownModel> lstYear = new List<DCW003DropDownModel>();
			lstYear = da.DCW003GetMasterYear();
			lstYear.Insert( 0, new DCW003DropDownModel
			{
				Value = string.Empty,
				Text = string.Empty
			} );
			return lstYear;
		}
		#endregion

		#region .IMPORT

		/// <summary>
		/// Import CSV
		/// </summary>
		/// <param name="lstCsv"></param>
		/// <param name="lstError"></param>
		/// <param name="lstNoMap"></param>
		/// <param name="lstImport"></param>
		public void DCW003ImportCsv( List<DCW003CsvModel> lstCsv, out string lstError, out string lstNoMap, out string lstImport, out string lstDocControlNo )
		{
			string errMsg = string.Empty;
			lstError = string.Empty;
			lstNoMap = string.Empty;
			lstImport = string.Empty;
			lstDocControlNo = string.Empty;
			da.DCW003InsertDocUketoriIf( lstCsv, out errMsg );

			if (!string.IsNullOrEmpty( errMsg ))
			{
				base.CmnEntityModel.ErrorMsgCd = errMsg;
				return;
			}

			da.DCW003ImportCsv( lstCsv, out lstError, out lstNoMap, out lstImport, out lstDocControlNo );
		}

		/// <summary>
		/// Get doc control master
		/// </summary>
		/// <param name="lstRfid"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="rowCount"></param>
		/// <returns></returns>
		public List<DCW003Result> DCW003GetDocControlMaster( List<DCW003RFID> lstRfid, int pageIndex, int pageSize, out int rowCount )
		{
			return da.DCW003GetDocControlMaster( lstRfid, pageIndex, pageSize, out rowCount );
		}

		public void DCWOO3InserJishameiMassho( List<DCW003Result> resultModel, List<DCW003CsvModel> lstCsv )
		{
			List<DCW003CsvModel> lstTemp = new List<DCW003CsvModel>();
			foreach (var item in resultModel)
			{
				foreach (var csv in lstCsv)
				{
					if (item.ChassisNo == csv.ChassisNo)
					{
						lstTemp.Add( new DCW003CsvModel
						{
							DocControlNo = item.DocControlNo,
							IraiDate = csv.IraiDate,
							ShopCd = csv.ShopCd,
							GenshaLocation = csv.GenshaLocation,
							CarName = csv.CarName,
							ChassisNo = csv.ChassisNo,
							JMType = csv.JMType,
							Note = csv.Note
						} );

						if (csv.JMType == "101")
						{
							item.JishameiKanryoNyukoDate = DateTime.Now;
							item.ReportType = "1";
						}

						if (csv.JMType == "201")
						{
							item.MasshoKanryoNyukoDate = DateTime.Now;
							item.ReportType = "2";
						}
					}
				}
			}
			da.DCW003InsertJishameiMassho( lstTemp );
		}

		#endregion

		private List<DCW003UketoriDetail> ResetUketoriDetail( List<DCW003FuzokuhinMaster> lstFuzokuhinMaster, List<string> lstDocControlNo )
		{
			List<DCW003UketoriDetail> lstUketoriDetail = new List<DCW003UketoriDetail>();
			foreach (var item in lstDocControlNo)
			{
				foreach (var master in lstFuzokuhinMaster)
				{
					lstUketoriDetail.Add( new DCW003UketoriDetail
					{
						DocControlNo = item,
						DocFuzoKuhinCd = master.DocFuzokuhinCd,
						IsChecked = 0
					} );
				}
			}

			return lstUketoriDetail;
		}

		private List<DCW003Uketori> GetUketoriDetail( List<DCW003FuzokuhinMaster> lstFuzokuhinMaster, List<string> lstDocControlNo, List<DCW003Result> lstResult )
		{
			List<DCW003UketoriDetail> lstUketoriDetail = new List<DCW003UketoriDetail>();
			lstUketoriDetail = da.DCW003GetUketoriDetail( lstDocControlNo );

			List<DCW003UketoriDetail> lstUketoriDetailReset = new List<DCW003UketoriDetail>();
			lstUketoriDetailReset = ResetUketoriDetail( lstFuzokuhinMaster, lstDocControlNo );
			foreach (var item in lstUketoriDetailReset)
			{
				foreach (var uketori in lstUketoriDetail)
				{
					if (uketori.DocControlNo == item.DocControlNo && uketori.DocFuzoKuhinCd == item.DocFuzoKuhinCd)
					{
						item.IsChecked = 1;
					}
				}
			}

			List<DCW003Uketori> lstUketoriTemp = new List<DCW003Uketori>();
			List<DCW003UketoriDetail> lstUketoriDetailTemp;
			foreach (var item in lstResult)
			{
				lstUketoriDetailTemp = new List<DCW003UketoriDetail>();
				lstUketoriDetailTemp = lstUketoriDetailReset.Where( x => x.DocControlNo == item.DocControlNo ).ToList();
				lstUketoriTemp.Add( new DCW003Uketori
				{
					DocControlNo = item.DocControlNo,
					AaKaisaiDate = item.AaKaisaiDate,
					ChassisNo = item.ChassisNo,
					DnSeiyakuDate = item.DnSeiyakuDate,
					JishameiFlg = item.JishameiFlg,
					KeiCarFlg = item.KeiCarFlg,
					ShakenLimitDate = item.ShakenLimitDate,
					ShiireShuppinnTorokuNo = item.ShiireShuppinnTorokuNo,
					ShoruiLimitDate = item.ShoruiLimitDate,
					TorokuNo = item.TorokuNo,
					UriageShuppinnTorokuNo = item.UriageShuppinnTorokuNo,
					MeihenShakenTorokuDate = item.MeihenShakenTorokuDate,
					UketoriDetail = lstUketoriDetailTemp
				} );
			}

			return lstUketoriTemp;
		}


		#region .UPDATE

		/// <summary>
		/// Update one record
		/// </summary>
		/// <param name="result"></param>
		public void DCW003Update( DCW003Result result )
		{
			string error = string.Empty;
			da.DCW003Update( result, out error );
			if (!string.IsNullOrEmpty( error ))
			{
				base.CmnEntityModel.ErrorMsgCd = error;
			}
		}

		/// <summary>
		/// Update all
		/// </summary>
		/// <param name="lstUpdate"></param>
		public void DCW003UpdateAll( List<DCW003Update> lstUpdate )
		{
			string error = string.Empty;
			string lstError = string.Empty;
			da.DCW003UpdateAll( lstUpdate );
		}

		/// <summary>
		/// Send auto search
		/// </summary>
		/// <param name="lstAutoSearch"></param>
		/// <param name="lstError"></param>
		/// <param name="lstSuccess"></param>
		public void DCW003SendAutoSearch( List<DCW003Update> lstAutoSearch, out string lstSuccess, out string lstError )
		{
			da.DCW003InsertAutoSearch( lstAutoSearch, out lstSuccess, out lstError );
		}

		public void DCW003Register( List<DCW003Uketori> lstUketori )
		{
			string errMsg = string.Empty;
			List<DCW003UketoriDetail> lstDetail = new List<DCW003UketoriDetail>();
			List<DCW003UketoriUpdate> lstUpdate = new List<DCW003UketoriUpdate>();
			foreach (var obj in lstUketori)
			{
				foreach (var item in obj.UketoriDetail)
				{
					if (item.IsChecked == 1)
					{
						lstDetail.Add( new DCW003UketoriDetail
						{
							DocControlNo = obj.DocControlNo,
							DocFuzoKuhinCd = item.DocFuzoKuhinCd,
							IsChecked = item.IsChecked,
							Note = item.Note
						} );
					}
				}
				lstUpdate.Add( new DCW003UketoriUpdate
				{
					DocControlNo = obj.DocControlNo,
					ShakenLimitDate = obj.ShakenLimitDate,
					ShoruiLimitDate = obj.ShoruiLimitDate,
					MeihenShakenTorokuDate = obj.MeihenShakenTorokuDate
				} );
			}

			da.DCW003InsertDocUketoriDetail( lstUpdate, lstDetail, out errMsg );
		}
		#endregion























	}
}
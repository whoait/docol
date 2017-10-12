using Gulliver.DoCol.BusinessServices.DCW;
using Gulliver.DoCol.Controllers;
using Gulliver.DoCol.Entities.DCW.DCW003Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gulliver.DoCol.WebReference;
using Gulliver.DoCol.ReportServices;
using System.Data.SqlClient;
using Gulliver.DoCol.UtilityServices;
using Gulliver.DoCol.Entities.Common;
using Gulliver.DoCol.Constants;
using Gulliver.DoCol.BusinessServices.Common;
namespace Gulliver.DoCol.Areas.DCW.Controllers
{
	public class DCW003Controller : BaseController
	{
		private Dictionary<string, string> _contentType = new Dictionary<string, string>();

		public ActionResult DCW003Index( int mode, int check )
		{
			string lstError = string.Empty;
			string lstNoMap = string.Empty;
			string lstImport = string.Empty;
			string lstDocControlNo = string.Empty;
			string messageContent = string.Empty;
			string messageType = string.Empty;
			int rowCount = 0;

			DCW003Model model = new DCW003Model();
			DCW003ConditionModel conditionModel = new DCW003ConditionModel();
			DCW003DetailModel detailModel = new DCW003DetailModel();
			List<DCW003Uketori> uketoriModel = new List<DCW003Uketori>();
			List<DCW003Result> resultModel = new List<DCW003Result>();
			DCW003ConditionModel cache = base.GetCache<DCW003ConditionModel>( CacheKeys.DCW003_CONDITION );

            if (cache == null)
            {
                conditionModel.PageIndex = DCW003Constant.DEFAULT_PAGE_INDEX;
                conditionModel.PageSize = DCW003Constant.DEFAULT_PAGE_SIZE;
                conditionModel.KeiCarFlg0 = NUMBER.NUM_0;
                conditionModel.KeiCarFlg1 = NUMBER.NUM_0;
                conditionModel.JishameiFlg = NUMBER.NUM_0;
                conditionModel.MasshoFlg = NUMBER.NUM_0;
                conditionModel.RadioType = NUMBER.NUM_1;
            }
            else {
                conditionModel = cache;
                mode = 0;
            }

			List<DCW003FuzokuhinMaster> lstFuzokuhinMaster = new List<DCW003FuzokuhinMaster>();

			using (DCW003Services services = new DCW003Services())
			{
				//Get label master fuzokuhin
				lstFuzokuhinMaster = services.DCW003GetFuzokuhinMaster();
				ViewBag.DocFuzokuhinMaster = lstFuzokuhinMaster;

				switch (mode)
				{

                    case 0:
                        {
                            services.DCW003Search(conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount);
                        }
                        break;
					//詳細条件で書類を検索する
					case 1:
						{
							conditionModel.ShohinType = DCW003Constant.SHOHIN_TYPE_DN;
							conditionModel.DocStatus102 = 1;
							conditionModel.AaDnSeiyakuDateStart = null;
							conditionModel.AaDnSeiyakuDateEnd = DateTime.Now.AddDays( -1 );
                            ViewBag.Mode = 1;
                            services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
						}
						break;

					//本日発送予定の書類を検索する(普通車)
					case 2:
						{
							conditionModel.ShohinType = DCW003Constant.SHOHIN_TYPE_DN;
							conditionModel.DocStatus102 = 1;
							conditionModel.KeiCarFlg0 = NUMBER.NUM_1;
							conditionModel.AaDnSeiyakuDateEnd = DateTime.Now.AddDays( -1 );

							services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
						}
						break;

					//本日発送予定の書類を検索する(軽)
					case 3:
						{
							conditionModel.ShohinType = DCW003Constant.SHOHIN_TYPE_DN;
							conditionModel.DocStatus102 = 1;
							conditionModel.KeiCarFlg1 = NUMBER.NUM_1;
							conditionModel.AaDnSeiyakuDateEnd = DateTime.Now.AddDays( -1 );

							services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
						}
						break;

					//棚卸し
					case 5:
						{
							conditionModel.DocStatus102 = 1;

							services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
						}
						break;
					//Import 1
					case 6:
						{
							List<DCW003CsvModel> lstCsv = new List<DCW003CsvModel>();
							DataTable tblCsv = (DataTable)TempData["tblCsv"];
							if (tblCsv != null)
							{
								if (tblCsv.Rows.Count > 0)
								{
									for (int i = 0; i < tblCsv.Rows.Count; i++)
									{
										lstCsv.Add( new DCW003CsvModel
										{
											ID = i,
											RacFileNo = tblCsv.Rows[i][0].ToString(),
											KeiCarFlg = tblCsv.Rows[i][1].ToString(),
											TorokuNo = tblCsv.Rows[i][2].ToString(),
											HyobanType = tblCsv.Rows[i][3].ToString(),
											ChassisNo = tblCsv.Rows[i][4].ToString(),
											GendokiKatashiki = tblCsv.Rows[i][5].ToString(),
											ReportType = tblCsv.Rows[i][6].ToString(),
										} );
									}
									if (check == 0)
									{
										conditionModel.ModeImport = 1;
										services.DCW003ImportCsv( lstCsv, out lstError, out lstNoMap, out lstImport, out lstDocControlNo );
										Utility.GetMessage( "I0010",
                                                            SubStringList(lstImport), SubStringList(lstError), SubStringList(lstNoMap),
															out messageType,
															out messageContent );
										ViewBag.contentMsgI0010 = messageContent;
										ViewBag.typeMsgI0010 = messageType;

										if (!string.IsNullOrEmpty( lstDocControlNo ))
										{
											lstDocControlNo = lstDocControlNo.Substring( 1 );
											services.DCW003GetListImport( lstDocControlNo, lstFuzokuhinMaster, conditionModel.PageIndex, conditionModel.PageSize,
																			out resultModel, out uketoriModel, out rowCount );
										}
									}
									else
									{
										conditionModel.ModeImport = 2;
										services.DCW003GetDocControlExist( lstCsv, lstFuzokuhinMaster, conditionModel.PageIndex, conditionModel.PageSize, 1,
																			out resultModel, out uketoriModel, out lstImport, out rowCount );

										Utility.GetMessage( "I0010",
															lstImport, string.Empty, string.Empty,
															out messageType,
															out messageContent );
										ViewBag.contentMsgI0010 = messageContent;
										ViewBag.typeMsgI0010 = messageType;
									}
								}
								else
								{
									return ReturnResult();
								}
							}
						}
						break;
					case 7:
						{
							conditionModel.ModeImport = 3;
							List<DCW003CsvModel> lstCsv = new List<DCW003CsvModel>();
							DataTable tblCsv = (DataTable)TempData["tblCsv"];
							if (tblCsv != null)
							{
								if (tblCsv.Rows.Count > 0)
								{
									for (int i = 0; i < tblCsv.Rows.Count; i++)
									{
										lstCsv.Add( new DCW003CsvModel
										{
											ID =i,
											KeiCarFlg = tblCsv.Rows[i][0].ToString(),
											TorokuNo = tblCsv.Rows[i][1].ToString(),
											HyobanType = tblCsv.Rows[i][2].ToString(),
											ChassisNo = tblCsv.Rows[i][3].ToString(),
											GendokiKatashiki = tblCsv.Rows[i][4].ToString(),
											ReportType = tblCsv.Rows[i][5].ToString(),
										} );
									}

									services.DCW003GetDocControlExist( lstCsv, lstFuzokuhinMaster, conditionModel.PageIndex, conditionModel.PageSize, 2,
																		out resultModel, out uketoriModel, out lstImport, out rowCount );
								}
							}
						}
						break;
					case 8:
						{
							conditionModel.ModeImport = 3;
							List<DCW003RFID> lstCsv = new List<DCW003RFID>();
							DataTable tblCsv = (DataTable)TempData["tblCsv"];
							if (tblCsv != null)
							{
								if (tblCsv.Rows.Count > 0)
								{
									for (int i = 0; i < tblCsv.Rows.Count; i++)
									{
										lstCsv.Add( new DCW003RFID
										{
											ID = i,
											RFIDKey = tblCsv.Rows[i][0].ToString(),
										} );
									}

									resultModel = services.DCW003GetDocControlMaster( lstCsv, conditionModel.PageIndex, conditionModel.PageSize, out rowCount );
								}
							}
						}
						break;
					case 9:
						{
							conditionModel.ModeImport = 4;
							List<DCW003CsvModel> lstCsv = new List<DCW003CsvModel>();
							DataTable tblCsv = (DataTable)TempData["tblCsv"];
							if (tblCsv != null)
							{
								if (tblCsv.Rows.Count > 0)
								{
									for (int i = 0; i < tblCsv.Rows.Count; i++)
									{
										if (tblCsv.Rows[i][5].ToString() == "自社名依頼")
										{
											tblCsv.Rows[i][5] = "101";
										}
										else if (tblCsv.Rows[i][5].ToString() == "抹消依頼")
										{
											tblCsv.Rows[i][5] = "201";
										}
										lstCsv.Add( new DCW003CsvModel
										{
											ID = i,
											IraiDate = tblCsv.Rows[i][0].ToString(),
											ShopCd = tblCsv.Rows[i][1].ToString(),
											GenshaLocation = tblCsv.Rows[i][2].ToString(),
											CarName = tblCsv.Rows[i][3].ToString(),
											ChassisNo = tblCsv.Rows[i][4].ToString(),
											JMType = tblCsv.Rows[i][5].ToString(),
											Note = tblCsv.Rows[i][6].ToString(),
										} );
									}

									services.DCW003GetDocControlExist( lstCsv, lstFuzokuhinMaster, conditionModel.PageIndex, conditionModel.PageSize, 3,
																		out resultModel, out uketoriModel, out lstImport, out rowCount );

									if (resultModel.Count >0)
									{
										services.DCWOO3InserJishameiMassho( resultModel, lstCsv );
									}		
								}
							}
						}
						break;
				}
			}
            
            conditionModel.TotalRow = resultModel.Count != 0 ? resultModel[0].RowCount: 0;
            if (conditionModel.TotalRow == 0 && mode != 6) {
                Utility.GetMessage(MessageCd.I0003, string.Empty, out messageType, out messageContent);
                ViewBag.typeMsgI0010 = messageType;
                ViewBag.contentMsgI0010 = messageContent;
            }
			detailModel.UketoriModel = uketoriModel;
			detailModel.ResultModel = resultModel;
			model.Condition = conditionModel;
			model.Detail = detailModel;
			base.SaveCache( CacheKeys.DCW003_CONDITION, conditionModel );
			InitialDropDownList();

			ViewBag.SearchCondition = model.Condition;
			return View( model );
		}

        private string SubStringList(string lst)
        {
            if (!string.IsNullOrEmpty(lst))
            {
                lst = lst.Substring(1);
            }   
            return lst;
        }

		private void InitialDropDownList()
		{
			List<DCW003DropDownModel> dropMassho = new List<DCW003DropDownModel>();
			List<DCW003DropDownModel> dropJishamei = new List<DCW003DropDownModel>();
			List<DCW003DropDownModel> dropDocStatus = new List<DCW003DropDownModel>();
			List<DCW003DropDownModel> dropYear = new List<DCW003DropDownModel>();
            DCW003ConditionModel cache = base.GetCache<DCW003ConditionModel>(CacheKeys.DCW003_CONDITION);
			using (DCW003Services services = new DCW003Services())
			{
				dropDocStatus = services.DCW003GetMasterDocStatus();
				dropYear = services.DCW003GetMasterYear();
			}
			//YEAR
			ViewBag.DropYear = dropYear;

			//DOC STATUS
            if (cache.ModeImport == 1 || cache.ModeImport == 2)
            {
                ViewBag.DropDocStatus = dropDocStatus;
            }
            else {
                dropDocStatus.RemoveAt(0);
                ViewBag.DropDocStatus = dropDocStatus;
            }
			//MASSHO
			dropMassho.Insert( 0, new DCW003DropDownModel { Value = "0", Text = "継続" } );
			dropMassho.Insert( 1, new DCW003DropDownModel { Value = "1", Text = "抹消" } );
			ViewBag.DropMassho = dropMassho;

			//JISHAMEI
			dropJishamei.Insert( 0, new DCW003DropDownModel { Value = "0", Text = "" } );
			dropJishamei.Insert( 1, new DCW003DropDownModel { Value = "1", Text = "自社名済" } );
			ViewBag.DropJishamei = dropJishamei;
		}

		public ActionResult DCW003Paging( CmnPagingModel cmnPagingModel )
		{
			DCW003ConditionModel conditionModel = base.GetCache<DCW003ConditionModel>( CacheKeys.DCW003_CONDITION );
			List<DCW003FuzokuhinMaster> lstFuzokuhinMaster = new List<DCW003FuzokuhinMaster>();
			string typeMsg = string.Empty;
			string errorMsg = string.Empty;
			int rowCount = 0;
			conditionModel.PageIndex = cmnPagingModel.PageIndex;
			conditionModel.PageSize = cmnPagingModel.PageSize;
			conditionModel.PageBegin = cmnPagingModel.PageBegin;
			conditionModel.PageEnd = cmnPagingModel.PageEnd;

			DCW003DetailModel detailModel = new DCW003DetailModel();
			List<DCW003Result> resultModel = new List<DCW003Result>();
			List<DCW003Uketori> uketoriModel = new List<DCW003Uketori>();
			using (DCW003Services services = new DCW003Services())
			{
				lstFuzokuhinMaster = services.DCW003GetFuzokuhinMaster();
				ViewBag.DocFuzokuhinMaster = lstFuzokuhinMaster;

				services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
			}

			detailModel.ResultModel = resultModel;
			detailModel.UketoriModel = uketoriModel;
            base.SaveCache(CacheKeys.DCW003_CONDITION,conditionModel);
			InitialDropDownList();
			ViewBag.SearchCondition = conditionModel;
			return View( "_DCW003Result", detailModel );
		}

		public ActionResult DCW003Search( DCW003ConditionModel conditionModel )
		{
			List<DCW003FuzokuhinMaster> lstFuzokuhinMaster = new List<DCW003FuzokuhinMaster>();
           
			string typeMsg = string.Empty;
			string contentMsg = string.Empty;
			int rowCount = 0;
			conditionModel.PageIndex = DCW003Constant.DEFAULT_PAGE_INDEX;
			conditionModel.PageSize = DCW003Constant.DEFAULT_PAGE_SIZE;
			DCW003DetailModel detailModel = new DCW003DetailModel();
			List<DCW003Result> resultModel = new List<DCW003Result>();
			List<DCW003Uketori> uketoriModel = new List<DCW003Uketori>();
			using (DCW003Services services = new DCW003Services())
			{
				lstFuzokuhinMaster = services.DCW003GetFuzokuhinMaster();
				ViewBag.DocFuzokuhinMaster = lstFuzokuhinMaster;

				services.DCW003Search( conditionModel, lstFuzokuhinMaster, out resultModel, out uketoriModel, out rowCount );
				if (resultModel.Count == 0)
				{
					UtilityServices.Utility.GetMessage( "I0003",
												string.Empty,
												out typeMsg,
												out contentMsg );

					this.ViewBag.typeMsgI0001 = typeMsg;
					this.ViewBag.contentMsgI0001 = contentMsg;
				}
			}
			if (conditionModel == null)
			{
				conditionModel.PageIndex = DCW003Constant.DEFAULT_PAGE_INDEX;
				conditionModel.PageSize = DCW003Constant.DEFAULT_PAGE_SIZE;
				conditionModel.TotalRow = rowCount;
			}
            conditionModel.TotalRow = resultModel.Count != 0 ? resultModel[0].RowCount : 0;
			base.SaveCache( CacheKeys.DCW003_CONDITION, conditionModel );
			detailModel.ResultModel = resultModel;
			detailModel.UketoriModel = uketoriModel;
			InitialDropDownList();
			ViewBag.SearchCondition = conditionModel;
			return View( "_DCW003Result", detailModel );
		}

		public ActionResult DCW003Update( DCW003Result resultModel )
		{
			using (DCW003Services services = new DCW003Services())
			{
				services.DCW003Update( resultModel );
			}
			return ReturnResult();
		}

		public ActionResult DCW003UpdateAll( List<DCW003Result> resultModel )
		{
            //DCW003ConditionModel cache = base.GetCache<DCW003ConditionModel>(CacheKeys.DCW003_PAGING);
            //DCW003ConditionModel cache = base.GetCache<DCW003ConditionModel>(CacheKeys.DCW003_CONDITION);
			if (resultModel != null)
			{
				List<DCW003Update> lst = new List<DCW003Update>();
                //List<DCW003DropDownModel> dropDocStatus = new List<DCW003DropDownModel>();
				foreach (var item in resultModel)
				{
                    //item.PageIndex = cache.PageIndex;
                    //item.PageSize = cache.PageSize;
                    //item.PageBegin = cache.PageBegin;
                    //item.PageEnd = cache.PageEnd;
					lst.Add( new DCW003Update
					{
						DocControlNo = item.DocControlNo,
						UriageShuppinTorokuNo = item.UriageShuppinnTorokuNo,
						MasshoFlg = item.MasshoFlg,
						JishameiFlg = item.JishameiFlg,
						DocStatus = item.DocStatus,
						DocNyukoDate = item.DocNyukoDate,
						DocShukkoDate = item.DocShukkoDate,
						JishameiIraiShukkoDate = item.JishameiIraiShukkoDate,
						JishameiKanryoNyukoDate = item.JishameiKanryoNyukoDate,
						MasshoIraiShukkoDate = item.MasshoIraiShukkoDate,
						MasshoKanryoNyukoDate = item.MasshoKanryoNyukoDate,
						Memo = item.Memo
					} );
				}

				using (DCW003Services services = new DCW003Services())
				{
					services.DCW003UpdateAll( lst );
				}
                //if (cache.ModeImport == 0)
                //{
                //    dropDocStatus.RemoveAt(0);
                //    ViewBag.DropDocStatus = dropDocStatus;
                //}
                //else
                //{
                //    ViewBag.DropDocStatus = dropDocStatus;
                //}
                //base.SaveCache(CacheKeys.DCW003_CONDITION,resultModel);
				return ReturnResult();
			}
			else
			{
				base.CmnEntityModel.ErrorMsgCd = "W0001";
				return ReturnResult();
			}
		}

		public ActionResult DCW003SendAutoSearch( List<DCW003Result> resultModel )
		{
			string lstError = string.Empty;
			string lstSuccess = string.Empty;
			if (resultModel != null)
			{
				List<DCW003Update> lst = new List<DCW003Update>();

				foreach (var item in resultModel)
				{
					lst.Add( new DCW003Update
					{
						RackNo = item.RacNo,
						FileNo = item.FileNo,
						ChassisNo = item.ChassisNo
					} );
				}

				using (DCW003Services services = new DCW003Services())
				{
					services.DCW003SendAutoSearch( lst, out lstSuccess, out lstError );
				}

				return ReturnListResult( SubStringList(lstSuccess), SubStringList(lstError) );
			}
			else
			{
				base.CmnEntityModel.ErrorMsgCd = "W0001";
				return ReturnResult();
			}
		}

		public ActionResult DCW003Register( List<DCW003Uketori> uketoriModel )
		{
			using (DCW003Services services = new DCW003Services())
			{
				services.DCW003Register( uketoriModel );
			}
			return ReturnResult();
		}

		public ActionResult PrintPage( List<DCW003Result> resultModel, string ReportID = "RD0020" )
		{
			if (resultModel != null)
			{
				DataTable table = new DataTable();
				MemoryStream stream = new MemoryStream();
				#region getFileReport
				// コンテントタイプの辞書を作成
				_contentType.Clear();
				_contentType.Add( ".fcp", "application/x-fmfcp" );
				_contentType.Add( ".fcx", "application/x-fmfcx" );

				_contentType.Add( ".dat", "text/plain" );
				_contentType.Add( ".xml", "application/x-fmdat+xml" );
				_contentType.Add( ".csv", "text/csv" );
				_contentType.Add( ".tsv", "text/tab-separated-value" );
				_contentType.Add( ".fcq", "application/x-fmfcq" );

				_contentType.Add( ".bmp", "image/x-bmp" );
				_contentType.Add( ".emf", "image/x-emf" );
				_contentType.Add( ".wmf", "image/x-wmf" );
				_contentType.Add( ".tif", "image/tiff" );
				_contentType.Add( ".jpg", "image/jpeg" );
				_contentType.Add( ".gif", "image/gif" );
				_contentType.Add( ".png", "image/png" );

				_contentType.Add( ".dev", "application/XmlReadMode-fmfcp" );

				_contentType.Add( ".fmcsm", "Application/x-fmcsm" );
				_contentType.Add( ".fmcsmd", "Application/x-fmcsmd" );

				FMWebService service = new FMWebService();
				service.Timeout = 1200 * 1000;
				service.Url = ConfigurationManager.AppSettings["WebReference.FMWebService"];

				FMWSRequest request = new FMWSRequest();
				FMWSKeyValue[] headers = new FMWSKeyValue[0];
				request.headers = headers;

				//リクエストパラメータの生成と、リクエストへの登録
				List<FMWSKeyValue> parameters = new List<FMWSKeyValue>();
				FMWSKeyValue param = new FMWSKeyValue();

				List<FMWSData> datas = new List<FMWSData>();

				System.Text.Encoding enc = System.Text.Encoding.GetEncoding( "Shift_JIS" );
				string fpath = Server.MapPath( @"~/iwfm/" ) + "iwfm_" + DateTime.Now.ToString( "yMMdd_HHmmss" ) + ".dat";
				System.IO.StreamWriter sr = new System.IO.StreamWriter( fpath, false, enc );

				if (ReportID == "RD0020") param.value = DCW003Constant.DEF_RD0010_FILE;
				///// 開始
				sr.WriteLine( @"[Control Section]" );
				sr.WriteLine( @"VERSION=7.2" );
				sr.WriteLine( @"OPTION=FIELDATTR" );
				sr.WriteLine( @";" );

				// Get data for report
				DataTable dataReport = new DataTable();
				DataReportsClients getDataReportClient = new DataReportsClients();
				//DataTable table = new DataTable();
				table.Columns.Add( "Item", typeof( string ) );
                table.Columns.Add("ID", typeof(string));
				//for (int i = 0; i < DocControlNo.Length; i++)
				//	table.Rows.Add( DocControlNo[i].ToString() );
                for (int i = 0; i < resultModel.Count; i++) {
                    table.Rows.Add(resultModel[i].DocControlNo,i);
                }

                //foreach (var item in resultModel)
                //{
                //    table.Rows.Add( item.DocControlNo );
                //}

				var pList = new SqlParameter( "@list", SqlDbType.Structured );
				pList.TypeName = "dbo.StringList";
				pList.Value = table;
				if (ReportID == "RD0020")
                {
					dataReport = getDataReportClient.ExportPDFRD0020( table );
				}
				if (dataReport.Rows.Count > 0)
				{
					param.key = "fm-formfilename";
					parameters.Add( param );
					param = new FMWSKeyValue();
					param.key = "fm-outputtype";
					param.value = "pdf";
					parameters.Add( param );
					param = new FMWSKeyValue();
					param.key = "fm-action";
					param.value = "view";
					parameters.Add( param );
					param = new FMWSKeyValue();
					param.key = "fm-target";
					param.value = "client";
					parameters.Add( param );
					request.parameters = parameters.ToArray();

					foreach (DataRow dr in dataReport.Rows)
					{
						//if (dr["AA会場"].ToString().Contains("JU"))
						//{

						#region create data file RD0020.DAT
						sr.WriteLine( @"[Body Data Section]" );
						sr.WriteLine( string.Format( @"<line1>={0}", dr["SHOP_CD"].ToString() ) );
						sr.WriteLine( string.Format( @"<line1_1>={0}", dr["TEMPO_NAME"].ToString() ) );
						sr.WriteLine( string.Format( @"<line1_2>={0}", "在庫" ) );
						sr.WriteLine( string.Format( @"<line2>={0}", "車庫証明・自社名依頼申請書" ) );
						sr.WriteLine( string.Format( @"<line3>={0}", "店舗コード" ) );
						sr.WriteLine( string.Format( @"<line3_1>={0}", dr["DJ_SHOPCD"].ToString() ) );
						sr.WriteLine( string.Format( @"<line4>={0}", "店舗名" ) );
						sr.WriteLine( string.Format( @"<line4_1>={0}", dr["TEMPO_NAME"].ToString() ) );
						sr.WriteLine( string.Format( @"<line5>={0}", "車名" ) );
						sr.WriteLine( string.Format( @"<line5_1>={0}", dr["CAR_NAME"].ToString() ) );
						sr.WriteLine( string.Format( @"<line6>={0}", "車台番号" ) );
						sr.WriteLine( string.Format( @"<line6_1>={0}", dr["CHASSIS_NO"].ToString() ) );
						sr.WriteLine( string.Format( @"<line7>={0}", "仕入番号" ) );
						sr.WriteLine( string.Format( @"<line7_1>={0}", dr["SHIIRE_NO"].ToString() ) );
						sr.WriteLine( string.Format( @"<line8>={0}", "車庫証明を取得し、自社名変をお願い致します" ) );
						sr.WriteLine( string.Format( @"<line9>={0}", "自社名終了後は車検原本をこの申請書と一緒に幕張オフィスDNチームにお送り下さい。" ) );
						sr.WriteLine( string.Format( @"<line10>={0}", dr["DN"].ToString() ) );
						sr.WriteLine( string.Format( @"<line11>={0}", "J-NET TEL" ) );
						sr.WriteLine( string.Format( @"<line11_1>={0}", dr["TEL"].ToString() ) );
						sr.WriteLine( string.Format( @"<line12>={0}", "          FAX" ) );
						sr.WriteLine( string.Format( @"<line12_1>={0}", dr["TEL"].ToString() ) );
						sr.WriteLine( string.Format( @"<line13>={0}", "管理番号: " ) );
						sr.WriteLine( string.Format( @"<line13_1>={0}", dr["CONTROL_NUMBER"].ToString() ) );
						sr.WriteLine( string.Format( @"<symbol>={0}", "※" ) );
						sr.WriteLine( string.Format( @"<symbol_1>={0}", "※" ) );

						sr.WriteLine( string.Format( @"[Form Section]" ) );
						sr.WriteLine( string.Format( @"NEXTPAGE" ) );
						sr.WriteLine( string.Format( @";" ) );
						#endregion
					}
				}

				sr.Close();

				//DATファイルの登録
				FMWSData datdata = new FMWSData();
				datdata.content = System.IO.File.ReadAllBytes( fpath );
				datdata.contentName = Path.GetFileName( fpath );
				datdata.contentType = _contentType[Path.GetExtension( fpath ).ToLower()];
				datas.Add( datdata );

				request.attachments = datas.ToArray();

				FMWSResponse response = service.overlay( request );
				Dictionary<string, List<String>> headerMap = new Dictionary<string, List<string>>();
				foreach (FMWSKeyValue header in response.headers)
				{
					List<string> list;
					if (!headerMap.ContainsKey( header.key ))
					{
						list = new List<string>();
						headerMap.Add( header.key, list );
					}
					else
					{
						list = headerMap[header.key];
					}
					list.Add( header.value );
				}
				string serviceStatus = headerMap["x-service-status"][0];//.ElementAt(0);
				//ステータスの確認
				if (serviceStatus != "200")
				{
					//string serviceMessage = headerMap["x-service-message"][0];//.ElementAt(0);
					//throw new Exception("overlay failure: " + serviceMessage);
					//Connect service khong thanh cong
					base.CmnEntityModel.ErrorMsgCd = "E0004";
					return ReturnResult();
				}

				Response.ClearContent();
				Response.Buffer = true;

				foreach (FMWSData attachment in response.attachments)
				{

					//IEの場合はファイル名をURLエンコードする
					String file = attachment.contentName;
					if (Request.Browser.Browser == "IE")
					{
						file = HttpUtility.UrlEncode( file );
					}
					Response.AddHeader( "Content-Disposition", "inline;filename=Report" + DateTime.Now.ToString( "yyyyMMdd" ) + ".pdf" );
					Response.ContentType = "application/pdf";
					Response.BinaryWrite( attachment.content );
					break;
				}

				Response.Flush();
				Response.End();
				return View( "_DCW003ViewPrintPage" );
				#endregion
			}
			else
			{
				base.CmnEntityModel.ErrorMsgCd = "W0001";
				return ReturnResult();
			}
		}

		public ActionResult OutputCsv( List<DCW003Result> resultModel )
		{
			if (resultModel != null)
			{
				DataReportsClients getDataReportClient = new DataReportsClients();
				MemoryStream stream = new MemoryStream();
				DataTable table = new DataTable();
				table.Columns.Add( "ファイルNO", typeof( string ) );
				//table.Rows.Add( "1", "123" );
				//table.Rows.Add( "2", "123" );
				foreach (var item in resultModel)
				{
                    table.Rows.Add(item.RacNo+item.FileNo);
				}
				stream = getDataReportClient.ExportCSV( table );


				string attachment = "attachment; filename= " + DateTime.Now.ToString( "yyyyMMddhhmmss" ) + "_TO_HD_CHECK.csv";
				Response.AddHeader( "content-disposition", attachment );
				Response.ContentType = "text/csv";
				//Response.AddHeader("ファイルNO");
				Response.BinaryWrite( stream.ToArray() );
				Response.Flush();
				stream.Close();

				return new EmptyResult();
			}
			else
			{
				base.CmnEntityModel.ErrorMsgCd = "W0001";
				return ReturnResult();
			}
		}

		private JsonResult ReturnResult()
		{
			string messageContent = string.Empty;
			string messageClass = string.Empty;
			string messageCd = "I0001";
			string messageReplace = string.Empty;
			bool isSuccess = true;
			if (!String.IsNullOrEmpty( base.CmnEntityModel.ErrorMsgCd ))
			{
				messageCd = base.CmnEntityModel.ErrorMsgCd;
				messageReplace = base.CmnEntityModel.ErrorMsgReplaceString;
				isSuccess = false;
			}

			UtilityServices.Utility.GetMessage( messageCd,
				string.Empty,
				out messageClass,
				out messageContent );
			return this.Json( new { Success = isSuccess, MessageClass = messageClass, Message = messageContent }, JsonRequestBehavior.AllowGet );
		}

		private JsonResult ReturnListResult( string lstSuccess, string lstError )
		{
			string messageContent = string.Empty;
			string messageClass = string.Empty;
			string messageCd = "I0011";
			string messageReplace = string.Empty;
			bool isSuccess = true;
			if (!String.IsNullOrEmpty( base.CmnEntityModel.ErrorMsgCd ))
			{
				messageCd = base.CmnEntityModel.ErrorMsgCd;
				messageReplace = base.CmnEntityModel.ErrorMsgReplaceString;
				isSuccess = false;
			}

			UtilityServices.Utility.GetMessage( messageCd,
				lstSuccess, lstError,
				out messageClass,
				out messageContent );

			return this.Json( new { Success = isSuccess, MessageClass = messageClass, Message = messageContent }, JsonRequestBehavior.AllowGet );
		}
	}
}

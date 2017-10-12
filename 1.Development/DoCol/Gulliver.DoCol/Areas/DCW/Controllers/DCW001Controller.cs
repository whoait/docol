
//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001Controller
// Overview		: Check user login information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gulliver.DoCol.Entities.DCW.DCW001Model;
using Gulliver.DoCol.BusinessServices.DCW;
using Gulliver.DoCol.Controllers;
using Gulliver.DoCol.UtilityServices;
using Gulliver.DoCol.Constants;



namespace Gulliver.DoCol.Areas.DCW.Controllers
{
    public class DCW001Controller : BaseController
    {
       
        [HttpGet]
        public ActionResult DCW001Index()
        {
            CacheUtil.RemoveAllCache();
            //  Return DCW001 view with default value.
            return this.View("DCW001Index");

        }

        [HttpPost]
        public ActionResult DCW001Index(string TANTOSHA_NAME, string PASSWORD, string domainLogin)
        {
            // remove all cache
            CacheUtil.RemoveAllCache();
            DCW001Login model = new DCW001Login();
            if (TANTOSHA_NAME != null) 
            {
                model.TANTOSHA_NAME = TANTOSHA_NAME;
                model.PASSWORD = PASSWORD;
            }

            // 1. Input check.
            if (ModelState.IsValid)
            {
                //Create new object get status login.
                DCW001Login objResultLogin = new DCW001Login();

                // Create new Service object to check login status.
                using (DCW001Services service = new DCW001Services())
                {
                    objResultLogin = service.DCW001CheckLoginByMultiStatus(model);
                    if (service.DCW001CheckDomainLogin(domainLogin))
                    {
                        return RedirectToAction("LoginFail", "Error", new { area = "Common" });  
                    }
                    /// 2. Check login by user.
                    if (objResultLogin == null)
                    {
                        // Return Errorpage
                        return RedirectToAction("LoginFail", "Error",new {area="Common"});                       
                    }
                    // 3. Save user login information into session common.
                    base.CmnEntityModel.UserName = objResultLogin.TANTOSHA_NAME;
                    base.CmnEntityModel.Password = objResultLogin.PASSWORD;
                    base.CmnEntityModel.ShainNo = objResultLogin.RACK_SEACH_KANO_FLG;

                    CacheUtil.SaveCache(CacheKeys.CmnEntityModel, base.CmnEntityModel);

                    return base.Redirect("DCW002Menu", "DCW002", new { area = "DCW"});
                }
            }
          
            return this.View(model);
        }
       
         ///<summary>
         //Logout
        /// </summary>
       ///<returns></returns>
        public ActionResult Logout()
        {
            CacheUtil.RemoveAllCache();
            return RedirectToAction("LogOut", "Error", new { area = "Common" });
        }    
     }
 }


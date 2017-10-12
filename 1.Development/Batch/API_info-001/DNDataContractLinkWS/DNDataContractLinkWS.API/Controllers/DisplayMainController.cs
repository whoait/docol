using DNDataContractLinkWS.BusinessLogic;
using DNDataContractLinkWS.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DNDataContractLinkWS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DisplayMainController : ApiController
    {
        public string Get()
        {
            return JsonConvert.SerializeObject("");
        }

        [System.Web.Http.HttpPost]
        public object Post(RequestDTO request)
        {
            string cn = ConfigurationManager.AppSettings.Get("ConnectionString");
            string maxDays = ConfigurationManager.AppSettings.Get("MaxDays");
            string commandTimeout = ConfigurationManager.AppSettings.Get("CommandTimeout");
            DisplayMainBL displayMainBL = new DisplayMainBL();

            return displayMainBL.GetResponse(request, cn, maxDays, commandTimeout);
        }
    }
}

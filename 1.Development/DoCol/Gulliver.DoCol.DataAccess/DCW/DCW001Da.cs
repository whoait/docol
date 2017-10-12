//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001Da
// Overview		: Check & Get user login information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------

using Gulliver.DoCol.DataAccess.Framework;
using Gulliver.DoCol.Entities;
using Gulliver.DoCol.Entities.DCW.DCW001Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.DataAccess.DCW
{
    public class DCW001Da : BaseDa
    {
        /// <summary>
        /// Check user login in-case auto and direct login mode.
        /// </summary>
        /// <param name="model">The user login information.</param>
        /// <returns>The login status.</returns>
        public DCW001Login DCW001CheckLoginByMultiStatus(DCW001Login model)
        {
              DCW001Login results;

            // Create new DBManager object for check user login into system.
              using (DBManager manager = new DBManager("stp_DCW001UserLogin"))
            {
                // Declare parameter input for stp_DCW001UserLogin.
                manager.Add("@userName", model.TANTOSHA_NAME);
                manager.Add("@password", model.PASSWORD);
                // Get the user login information from database.
                DataTable dataTable = manager.GetDataTable();

                // Check data login from database.
                if (dataTable.Rows.Count == 0)
                {
                    results = null;
                }
                else
                {                  
                  results = EntityHelper<DCW001Login>.GetListObject(dataTable)[0];                 
                }
            }

            // Return status login to service.
            return results;
        }
        public bool DCW001CheckDomainLogin(string domainLogin)
        {
            using (DBManager manager = new DBManager("stp_DCW001CheckDomainLogin"))
            {
                manager.Add("@domain_Login", domainLogin);
                DataTable dataTable = manager.GetDataTable();

                return (dataTable.Rows.Count == 0);
            }
        }
    }
}

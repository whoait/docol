//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001Services.
// Overview		: Check & Get user login information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------
using Gulliver.DoCol.DataAccess.DCW;
using Gulliver.DoCol.Entities.DCW.DCW001Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.BusinessServices.DCW
{
    public class DCW001Services : BaseServices
    {


        public DCW001Login DCW001CheckLoginByMultiStatus(DCW001Login model)
        {
            // Create new object get status login.
            DCW001Login objResultLogin = new DCW001Login();

            // Create new DataAccess object for get active status.
            DCW001Da dataAccess = new DCW001Da();


            // Check login status.
            // return dataAccess.LI0001CheckLoginByMultiStatus(model);
            return dataAccess.DCW001CheckLoginByMultiStatus(model);
        }
        public bool DCW001CheckDomainLogin(string domainLogin)
        {
            DCW001Da dataAccess = new DCW001Da();
            return dataAccess.DCW001CheckDomainLogin(domainLogin);
        }

    }
}

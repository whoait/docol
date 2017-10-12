//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001CheckLogin.
// Overview		: Check and get user login information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW001Model
{
    public class DCW001CheckLogin
    {
        /// <summary>
        /// ID from database.
        /// </summary>
        public string Id { get; set; }
       
        /// <summary>
        /// username  from database.
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Password from from database.
        /// </summary>        
        public string password { get; set; }

        /// <sumary>
        /// Flag from database
        /// </sumary>
        public string flag { get; set; }
        
    }
}

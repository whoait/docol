//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001InterfaceLogin
// Overview		: Get user login information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------

using System;
using Gulliver.DoCol.Constants;
using Gulliver.DoCol.DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW001Model
{
    public class DCW001InterfaceLogin
    {
        /// <summary>
        /// ID from database.
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// username  from database.
        /// </summary>  
        [EXAlphaNumberic(MessageCd.W0002, typeof(DCW001CheckLogin), "userNameAlphanumericRequired")]
        public string userName { get; set; }

        /// <summary>
        /// Password from from database.
        /// </summary> 
        [EXAlphaNumberic( MessageCd.W0002, typeof( DCW001CheckLogin ), "passwordAlphanumericRequired" )]
        public string password { get; set; }

        /// <sumary>
        /// Flag from database
        /// </sumary>
        public int flag { get; set; }
        
    }
}

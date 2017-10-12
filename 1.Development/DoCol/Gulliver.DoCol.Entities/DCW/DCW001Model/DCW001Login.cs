//---------------------------------------------------------------------------
// Version		: 1.0
// Class Name	: DCW001LoginModel
// Overview		: Get login auto mode information.
// Designer		: Dhanya.Ratheesh
// Programmer	: Dhanya.Ratheesh
// Created Date	: 2015/11/24
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW001Model
{
   public class DCW001Login
    {
        /// <summary>
        /// Get ID .
        /// </summary>
        public string Id { get; set; }
        
       /// <summary>
        ///Get username  .
        /// </summary>
        [Required(ErrorMessage = "Please Enter UserName")]
        public string TANTOSHA_NAME { get; set; }

        /// <summary>
        /// Get Password .
        /// </summary>     
        [Required(ErrorMessage = "Please Enter Password")]
        public string PASSWORD { get; set; }
       
       /// <sumary>
       /// Flag 
        /// </sumary>
        public int flag { get; set; }


        /// <sumary>
        /// RACK_SEACH_KANO_FLG 
        /// </sumary>
        public string RACK_SEACH_KANO_FLG { get; set; }
        
    }
}

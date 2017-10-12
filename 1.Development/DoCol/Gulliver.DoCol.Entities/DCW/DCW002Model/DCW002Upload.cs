using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW002Model
{
    public class DCW002Upload
    {

        /// <summary>
        /// get ID 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// get fileno
        /// </summary>        
       public string fileNo { get; set; }

        /// <summary>
        /// get vechicleClassification
        /// </summary> 
        public string vechicleClassification { get; set; }

        /// <sumary>
        /// get vechicleRegistrationNo
        /// </sumary>
        public string vechicleRegistrationNo { get; set; }

        /// <sumary>
        /// get numberPlate
        /// </sumary>
        public string numberPlate { get; set; }

        /// <sumary>
        /// get chassisNo
        /// </sumary>
        public string chassisNo { get; set; }

        /// <sumary>
        /// get motorModel
        /// </sumary>
        public string motorModel { get; set; }

        /// <sumary>
        /// get formType
        /// </sumary>
        public string formType { get; set; }
        /// <sumary>
        /// get current user
        /// </sumary>
        public string currentUser { get; set; }
        
       

    }
}

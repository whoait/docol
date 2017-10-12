using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDataContractLinkWS.DTO
{
    public class ResponseDTO
    {
        //declare values response when API fail
        public List<DisplayMainDTO> O_DN_SEIYAKU_LIST { get; set; }
        public string O_RETURN_CD { get; set; }
    }

    public class ResponseFailDTO
    {
        //declare values response when API fail
        public string O_RETURN_CD { get; set; }
        public string O_ERROR_CD { get; set; }
        public string O_ERROR_INFO { get; set; }
    }
}

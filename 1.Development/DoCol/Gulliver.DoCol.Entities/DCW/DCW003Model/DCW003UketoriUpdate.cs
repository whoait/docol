using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.DCW.DCW003Model
{
	public class DCW003UketoriUpdate
	{
		public string DocControlNo { get; set; }

		public DateTime? ShakenLimitDate { get; set; }

		public DateTime? ShoruiLimitDate { get; set; }

		public DateTime? MeihenShakenTorokuDate { get; set; }
	}
}

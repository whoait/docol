using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gulliver.DoCol.Entities.Common
{
	public class LinkTypeModel
	{
		public string Type { get; set; }
		public DateTime? Name { get; set; }
		public string Link { get; set; }
		public string Code { get; set; }
		public string UrlIcon { get; set; }

		public LinkTypeModel( string type, DateTime? name, string code )
		{
			this.Type = type;
			this.Name = name;
			this.Code = code;
		}

		public LinkTypeModel( string type, DateTime? name, string link, string code )
		{
			this.Type = type;
			this.Name = name;
			this.Link = link;
			this.Code = code;
		}

		public LinkTypeModel()
		{ }
	}
}

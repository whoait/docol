//---------------------------------------------------------------------------
// Version			: 001
// Designer			: QuanNH7-FPT
// Programmer		: QuanNH7-FPT
// Date				: 2015/03/31
// Comment			: Create new
//---------------------------------------------------------------------------

using System.Xml;

//using System.Xml.Xsl;
//using System.Xml.XPath;

namespace Gulliver.DoCol.UtilityServices
{
	/// <summary>
	/// Helper Class for XML and XSL
	/// </summary>
	public class XMLHelper
	{
		#region CreateXMLAttribute

		/// <summary>
		/// Creates and returns an XML attribute
		/// </summary>
		/// <param name="sourceDoc">XMLDocument for which attribute is created</param>
		/// <param name="name">Attribute Name</param>
		/// <param name="value">Attribute Value</param>
		/// <returns>XML Attribute</returns>
		public static XmlAttribute CreateXMLAttribute( XmlDocument sourceDoc, string name, string value )
		{
			XmlAttribute attrib = sourceDoc.CreateAttribute( name );
			attrib.Value = value;
			return attrib;
		}

		#endregion CreateXMLAttribute

		#region Public Static Methods

		/// <summary>
		/// Get InnerText of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static string GetInnerText( System.Xml.XmlNode Node )
		{
			if (Node != null)
			{
				return Node.InnerText;
			}
			return string.Empty;
		}

		/// <summary>
		/// Get InnerText of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static string GetInnerXml( System.Xml.XmlNode Node )
		{
			if (Node != null)
			{
				return Node.InnerXml;
			}
			return string.Empty;
		}

		/// <summary>
		/// Get InnerXml of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static string GetInnerXml( XmlDocument DocXML, string XPATH )
		{
			if (DocXML != null)
			{
				return GetInnerXml( DocXML.SelectSingleNode( XPATH ) );
			}
			return string.Empty;
		}

		/// <summary>
		/// Get InnerText of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static string GetInnerText( XmlDocument DocXML, string XPATH )
		{
			if (DocXML != null)
			{
				return GetInnerText( DocXML.SelectSingleNode( XPATH ) );
			}
			return string.Empty;
		}

		/// <summary>
		/// Get InnerText as a int of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static int GetInnerTextInt( System.Xml.XmlNode Node )
		{
			int data = 0;
			int.TryParse( GetInnerText( Node ), out data );
			return data;
		}

		/// <summary>
		/// Get InnerText as a int of a Xml Node
		/// </summary>
		/// <param name="Node"></param>
		/// <returns></returns>
		public static int GetInnerTextInt( XmlDocument DocXML, string XPATH )
		{
			int data = 0;
			int.TryParse( GetInnerText( DocXML, XPATH ), out data );
			return data;
		}

		/// <summary>
		/// Get InnerText as a long of a Xml Node
		/// </summary>
		/// <param name="Node">XML Node</param>
		/// <returns>long</returns>
		public static long GetInnerTextLong( System.Xml.XmlNode Node )
		{
			long data = 0;
			long.TryParse( GetInnerText( Node ), out data );
			return data;
		}

		/// <summary>
		/// Get InnerText as a long of a Xml Node
		/// </summary>
		/// <param name="Node">XML Node</param>
		/// <returns>long</returns>
		public static long GetInnerTextLong( XmlDocument DocXML, string XPATH )
		{
			long data = 0;
			long.TryParse( GetInnerText( DocXML, XPATH ), out data );
			return data;
		}

		#endregion Public Static Methods
	}
}
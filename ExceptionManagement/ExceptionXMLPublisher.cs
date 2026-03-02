using System;
using System.Text;
using System.IO;
using System.Xml;	
using System.Collections;
using System.Collections.Specialized;
using ExceptionManagement;

namespace ExceptionManagement
{
	public class ExceptionXMLPublisher : IExceptionXmlPublisher
	{
		void IExceptionXmlPublisher.Publish(XmlDocument ExceptionInfo, NameValueCollection ConfigSettings)
		{
			string filename;
			if (ConfigSettings != null)
			{
				filename = ConfigSettings["fileName"];
			}
			else
			{
				filename = @"C:\ErrorLog.xml";
			}
			using ( FileStream fs =  File.Open(filename, FileMode.Create,FileAccess.ReadWrite) )
			{
				using ( StreamWriter writer = new StreamWriter(fs) )
				{
					writer.Write(ExceptionInfo.OuterXml);
				}
			}
		}
	}
}

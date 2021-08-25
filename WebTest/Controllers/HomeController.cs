using System.Xml;
using System.Xml.Xsl;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Xml.XPath;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string id)
        {
            StringWriter strWriter = new StringWriter();
            XmlDocument sqlSelect = new XmlDocument();
            if(id != null)
                sqlSelect.Load(Server.MapPath("~/data/data-ajax.xml"));
            else
                sqlSelect.Load(Server.MapPath("~/data/data.xml"));

            string select = sqlSelect.SelectSingleNode("sql").InnerText.Replace("{id}", id);



            XmlDocument docXML = GetXMLData(select);

            XmlDocument xsltXML = new XmlDocument();
            xsltXML.Load(Server.MapPath("~/xslt/html.xslt"));

            XsltArgumentList xsltParameters = new XsltArgumentList();
            if(id != null)
                xsltParameters.AddParam("id", "", id);

            XslCompiledTransform xslt = new XslCompiledTransform();

            try
            {
                xslt.Load(xsltXML, new XsltSettings(false, true), null);
                xslt.Transform(docXML, xsltParameters, strWriter);
            }
            catch (Exception ex)
            {
                strWriter.Write(ex.Message + "<br /><br />" + ex.StackTrace);
            }

            return Content(FinalReplace(strWriter.ToString()));
        }

        private XmlDocument GetXMLData(string Select) {
            if (string.IsNullOrEmpty(Select))
                return null;

            string connStr = ConfigurationManager.ConnectionStrings["WebTest"].ConnectionString;

            XmlDocument result = new XmlDocument();
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                
                using (SqlCommand sqlComm = new SqlCommand(Select, sqlConn))
                {
                    sqlComm.CommandType = System.Data.CommandType.Text;
                    try {
                        sqlConn.Open();

                        using (XmlReader xmlReader = sqlComm.ExecuteXmlReader())
                        {
                            var xp = new XPathDocument(xmlReader);
                            var xn = xp.CreateNavigator();
                            XmlNode root = result.CreateElement("Items");
                            root.InnerXml = xn.OuterXml;
                            result.AppendChild(root);
                        }
                    }
                    catch (Exception ex) {
                        result.AppendChild(result.CreateElement("Errors"));
                        result.DocumentElement.AppendChild(result.CreateElement("Error"));
                        result.DocumentElement["Error"].InnerText = ex.Message + "{br}{br}" + ex.StackTrace;
                    }
                }
            }

            return result;
        }

        private string FinalReplace(string Html) {
            Html = Html.Replace("{br}", "<br />");
            Html = Html.Replace("{empty}", "");

            return Html;
        }
    }
}
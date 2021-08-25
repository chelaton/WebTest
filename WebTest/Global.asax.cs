using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace WebTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/AppStart.txt"), true))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine("***********************");
            }
        }
    }
}

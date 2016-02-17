
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProcessClaimService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var xmlFormatter = new NamespacedXmlMediaTypeFormatter() { UseXmlSerializer = true };
            xmlFormatter.Namespaces.Add("cla", "http://www.test.com/examples/claim");
            GlobalConfiguration.Configuration.Formatters.Insert(0, xmlFormatter);
           
            //GlobalConfiguration.Configuration.Formatters.Clear();
            //GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
           // GlobalConfiguration.Configuration.Formatters.Insert(0, new ClaimMediaTypeFormatter());
           // GlobalConfiguration.Configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}

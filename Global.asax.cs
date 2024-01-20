using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoogFleet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;

           

           
        }
        //void Application_BeginRequest(Object source, EventArgs e)
        //{
        //    var app = (HttpApplication)source;
        //    var uriObject = app.Context.Request.Url;
        //    //app.Context.Request.Url.OriginalString

        //    if (uriObject.AbsoluteUri.Contains("app.googfleet.com"))
        //    {
        //        HttpContext.Current.Session["curdomain"] = "googfleet";
        //    }
        //    else if (uriObject.AbsoluteUri.Contains("www.fleetmanager.us"))
        //    {
        //        HttpContext.Current.Session["curdomain"] = "fleetmanager";
        //    }
        //}
    }
}

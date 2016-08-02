using IBC_Forms.Model;
using IBC_Forms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace IBC_Forms
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Export",
                routeTemplate: "api/{controller}/{id}/{fields}/{type}"
            );

            /*Encoding latinEncoding = Encoding.GetEncoding("UTF-16");
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Add(latinEncoding);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.RemoveAt(0);*/

            //Startup daten neue einpfelgen ???
            /*Database.getInstance().DeleteData();
            foreach (Form f in TestData.getForms())
                Database.getInstance().insertForm(f);*/
        }
    }
}

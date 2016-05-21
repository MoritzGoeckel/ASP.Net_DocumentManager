using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBC_Forms.Utils
{
    public class LocalPath
    {
        public static string getTmpPath()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("~/tmp/");
        }

        public static string getTemplatePath()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("~/templates/");
        }

        public static string getDatabasePath()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("~/Database/");
        }
    }
}
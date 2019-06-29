using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SNB.Common
{
    public static class ExtensionMethod
    {
        public static string CheckDirectory(this string url)
        {
            var fullUrl = HttpContext.Current.Server.MapPath(url);
            if (!Directory.Exists(fullUrl))
                Directory.CreateDirectory(fullUrl);
            return url;
        }
    }
}

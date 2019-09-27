using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PayTabs.Web.Models.Helpers
{
    public static class ServerClient
    {
        public static string GetServerIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public static string GetClientIP()
        {
          return  HttpContext.Current.Request.UserHostAddress;

        }
    }
}
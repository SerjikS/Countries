using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countries_WebServer
{
    /// <summary>
    /// Сводное описание для CheckConnection
    /// </summary>
    public class CheckConnection : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
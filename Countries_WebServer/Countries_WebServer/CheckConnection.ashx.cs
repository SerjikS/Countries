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
        public void ProcessRequest(HttpContext Context)
        {
            ExecuteCheckConnection(Context);
        }

        private void ExecuteCheckConnection(HttpContext Context)
        {
            Context.Response.ContentType = "text/plain";
            Context.Response.Write("1");
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
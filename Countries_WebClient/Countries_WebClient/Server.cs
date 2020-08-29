using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Windows.Media;

namespace Countries_WebClient
{
    static class Server
    {
        public static string Link = "http://localhost:8081/";
        public static int ServerConnection = 0;

        private static string HttpRequest(string HttpRequest)
        {
            string Result = null;
            WebRequest request = WebRequest.Create(HttpRequest);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Result = reader.ReadLine();
                }
            }
            response.Close();
            return Result;
        }
        private static bool HTTPIsNull(string HTTPRequest)
        {
            if (string.IsNullOrEmpty(HTTPRequest))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Check()
        {
            string Result = null;
            try
            {
                Result = HttpRequest($"{Server.Link}CheckConnection.ashx");
            }
            catch (WebException Exception)
            {
                if (Exception.Status == WebExceptionStatus.ConnectFailure)
                {
                    ServerConnection = 0;
                }
                if (Exception.Status == WebExceptionStatus.ProtocolError)
                {
                    ServerConnection = 2;
                }
            }
            if (Result == "1")
            {
                ServerConnection = 1;
            }
        }
        public static bool HTTPRequestAllow()
        {
            Server.Check();
            ServerReactionCheck.ReactionCheck();
            if (Server.ServerConnection == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

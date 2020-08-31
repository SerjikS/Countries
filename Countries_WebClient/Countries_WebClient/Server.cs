using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

namespace Countries_WebClient
{
    static class Server
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public static string Link = "http://localhost:8081/";
        /// <summary>
        /// Состояние сервера.
        /// 0 - Сервер не существует
        /// 1 - Сервер существует. Запрос успешно обработан
        /// 2 - Сервер существует, но по неизвестной причине запрос обработан неуспешно
        /// </summary>
        public static int ServerConnection = 0;

        /// <summary>
        /// Проверка доступности сервера
        /// </summary>
        public static void Check()
        {
            string Result = null;

            try
            {
                Result = HTTPClient.HttpRequest($"{Server.Link}CheckConnection.ashx");
            }
            catch (WebException Exception)
            {
                switch (Exception.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                        ServerConnection = 0;
                        break;
                    case WebExceptionStatus.ProtocolError:
                        ServerConnection = 2;
                        break;
                    default:
                        break;
                }
            }
            if (Result == "1")
            {
                ServerConnection = 1;
            }
        }
    }
}

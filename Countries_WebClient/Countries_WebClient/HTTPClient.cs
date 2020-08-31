using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Text.Json;
using System.IO;

namespace Countries_WebClient
{
    class HTTPClient
    {
        /// <summary>
        /// Выполнение HTTP запроса
        /// </summary>
        /// <param name="HttpRequest">HTTP Запрос</param>
        public static string HttpRequest(string HttpRequest)
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

        /// <summary>
        /// Проверка HTTP запроса
        /// </summary>
        /// <param name="HTTPRequest">HTTP Запрос</param>
        public static bool HTTPIsNull(string HTTPRequest)
        {
            if (string.IsNullOrEmpty(HTTPRequest) || HTTPRequest == "[]")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Доступность сервера для HTTP запросов
        /// </summary>
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

        /// <summary>
        /// Преобразование HTTP ответа в JSON
        /// </summary>
        /// <param name="HTTPRequest">HTTP Запрос</param>
        public static List<string> HTTPResponseToData(string HTTPRequest)
        {
            List<string> ListData = new List<string>();
            List<Country> ListCountries = JsonSerializer.Deserialize<List<Country>>(HTTPRequest);

            for (int i = 0; i != ListCountries.Count; i++)
            {
                string Convert = $@"
Название: { ListCountries[i].Name}
Код: { ListCountries[i].Code}
Столица: { ListCountries[i].Capital}
Площадь: { (ListCountries[i].Area).ToString("G20")}
Население: { ListCountries[i].Population}
Регион: { ListCountries[i].Region}
                ";
                ListData.Add(Convert);
            }
            return ListData;
        }
    }
}

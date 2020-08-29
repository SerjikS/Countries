using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using System.Text.Json;

namespace Countries_WebClient
{
    class HTTP
    {
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

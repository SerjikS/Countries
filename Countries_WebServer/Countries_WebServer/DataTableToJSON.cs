using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Countries_WebServer
{
    public static class DataTableToJSON
    {
        /// <summary>
        /// Преобразование DataTable в JSON
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        public static string Convert(DataTable dataTable)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dataTable);
            return JSONString;
        }
    }
}
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
        public static string Convert(DataTable DataTable)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(DataTable);
            return JSONString;
        }
    }
}
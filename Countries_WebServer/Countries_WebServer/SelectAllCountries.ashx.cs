using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Countries_WebServer
{
    /// <summary>
    /// Сводное описание для SelectAllCountries
    /// </summary>
    public class SelectAllCountries : IHttpHandler
    {

        public void ProcessRequest(HttpContext Context)
        {
            string Command = ($@"
                SELECT Countries.Name, Countries.Code, Cities.Name as 'Capital', Countries.Area, Countries.Population, Regions.Name as 'Region' 
                FROM Countries, Cities, Regions 
                WHERE (Cities.Id = Countries.Capital) AND (Regions.Id = Countries.Region);
            ;");
            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);

            DataTable dataTable = SqlTransact.ExecuteReader();
            string Result = DataTableToJSON.Convert(dataTable);

            Context.Response.ContentType = "text/plain";
            Context.Response.Write(Result);
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
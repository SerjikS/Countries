using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.Text.Json;
using Newtonsoft.Json;

namespace Countries_WebServer
{
    /// <summary>
    /// Сводное описание для SelectConcreteCountry
    /// </summary>
    public class SelectConcreteCountry : IHttpHandler
    {
        public void ProcessRequest(HttpContext Context)
        {
            string Command = ($@"
                    SELECT Countries.Name, Countries.Code, Cities.Name as 'Capital', Countries.Area, Countries.Population, Regions.Name as 'Region' 
                    FROM Countries, Cities, Regions 
                    WHERE (Countries.Name = @Country) AND (Cities.Id = Countries.Capital) AND (Regions.Id = Countries.Region);
                ");

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Country", Context.Request.QueryString["Country"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);

            DataTable dataTable = SqlTransact.ExecuteReader();
            Context.Response.ContentType = "text/plain";

            string Result = DataTableToJSON.Convert(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                Result = DataTableToJSON.Convert(dataTable);
                Context.Response.Write(Result);
            }
            else
            {
                Context.Response.Write("");
            }

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
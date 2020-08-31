using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;

namespace Countries_WebServer
{
    /// <summary>
    /// Сводное описание для SelectAllCountries
    /// </summary>
    public class SelectAllCountries : IHttpHandler
    {
        public void ProcessRequest(HttpContext Context)
        {
            ExecuteSelectAllCountries(Context);
        }

        private void ExecuteSelectAllCountries(HttpContext Context)
        {
            DataTable dataTable = GetAllCountries();
            string Result = DataTableToJSON.Convert(dataTable);

            Context.Response.ContentType = "text/plain";

            if (dataTable.Rows.Count > 0)
            {
                Context.Response.Write(Result);
            }
            else
            {
                Context.Response.Write("");
            }
        }
        private DataTable GetAllCountries()
        {
            string Command = $@"
                SELECT Countries.Name, Countries.Code, Cities.Name as 'Capital', Countries.Area, Countries.Population, Regions.Name as 'Region' 
                FROM Countries, Cities, Regions 
                WHERE (Cities.Id = Countries.Capital) 
                    AND (Regions.Id = Countries.Region)
            ";

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            DataTable dataTable = SqlTransact.ExecuteReader();
            return dataTable;
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
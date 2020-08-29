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
    /// Сводное описание для UpdateConcreteCountry
    /// </summary>
    public class UpdateConcreteCountry : IHttpHandler
    {
        public void ProcessRequest(HttpContext Context)
        {
            bool CapitalExists = false;
            bool RegionExists = false;
            bool CountryExists = false;

            int CountCountries = 0;
            int IdCapital = 0;
            int IdRegion = 0;
            int IdCountry = 0;

            string Result = string.Empty;

            CountCountries = GetCountCountries(Context);
            if (CountCountries != 1)
            {
                IdCapital = GetIdCity(Context);

                if (IdCapital != 0)
                {
                    CapitalExists = true;
                }

                if (CapitalExists == false)
                {
                    InsertCity(Context);
                    IdCapital = GetIdCity(Context);

                    if (IdCapital != 0)
                    {
                        CapitalExists = true;
                    }
                }

                IdRegion = GetIdRegion(Context);
                if (IdRegion != 0)
                {
                    RegionExists = true;
                }

                if (RegionExists == false)
                {
                    InsertRegion(Context);
                    IdRegion = GetIdRegion(Context);

                    if (IdRegion != 0)
                    {
                        RegionExists = true;
                    }
                }

                IdCountry = GetIdCountry(Context);

                if (IdCountry != 0)
                {
                    CountryExists = true;
                }
                if (CountryExists == false)
                {
                    InsertCountry(Context, IdCapital, IdRegion);
                }
                else
                {
                    UpdateCountry(Context, IdCapital, IdRegion);
                }
                
                if (IdCountry != 0)
                {
                    CountryExists = true;
                    DataTable dataTable = GetCountry(Context);
                    Result = DataTableToJSON.Convert(dataTable);
                }
            }
            else
            {
                Result = null;
            }

            Context.Response.ContentType = "text/plain";

            if (Result == null && CountCountries == 1)
            {
                Context.Response.Write(2);
            }
            else if (Result == null && CountCountries != 1)
            {
                Context.Response.Write("");
            }
            else
            {
                Context.Response.Write(1);
            }
        }

        private int GetCountCountries(HttpContext Context)
        {
            string Command = ($@"
                SELECT COUNT(*) 
                FROM Countries 
                WHERE Name = @Name 
                    AND Code != @Code
            ");

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Name", Context.Request.QueryString["Name"]),
                new Params("Code", Context.Request.QueryString["Code"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);

            DataTable dataTable = SqlTransact.ExecuteReader();

            if (dataTable.Rows.Count > 0)
            {
                int CountCountries = Convert.ToInt32(dataTable.Rows[0][0]);
                return CountCountries;
            }
            return 0;
        }
        private int GetIdCity(HttpContext Context)
        {
            string Command = ($@"
                SELECT Id 
                FROM Cities 
                WHERE Name = @Capital
            ");

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Capital", Context.Request.QueryString["Capital"])
            };

           SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);

            DataTable dataTable = SqlTransact.ExecuteReader();

            if (dataTable.Rows.Count > 0)
            {
                int IdCapital = Convert.ToInt32(dataTable.Rows[0][0]);
                return IdCapital;
            }
            return 0;
        }
        private void InsertCity(HttpContext Context)
        {
            string Command = $@"
                INSERT INTO Cities 
                VALUES (@Capital)
            ;";

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Capital", Context.Request.QueryString["Capital"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
            SqlTransact.ExecuteNonQuery();
        }
        private int GetIdRegion(HttpContext Context)
        {
            string Command = ($@"
                SELECT Id 
                FROM Regions 
                WHERE Name = @Region
            ");

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Region", Context.Request.QueryString["Region"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);

            DataTable dataTable = SqlTransact.ExecuteReader();

            if (dataTable.Rows.Count > 0)
            {
                int IdRegion = Convert.ToInt32(dataTable.Rows[0][0]);
                return IdRegion;
            }
            return 0;
        }
        private void InsertRegion(HttpContext Context)
        {
            string Command = $@"
                INSERT INTO Regions 
                VALUES (@Region)
            ;";

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Region", Context.Request.QueryString["Region"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
            SqlTransact.ExecuteNonQuery();
        }
        private int GetIdCountry(HttpContext Context)
        {
            string Command = ($@"
                SELECT Id 
                FROM Countries 
                WHERE Code = @Code
            ");

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Code", Context.Request.QueryString["Code"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
            DataTable dataTable = SqlTransact.ExecuteReader();

            if (dataTable.Rows.Count > 0)
            {
                int IdCountry = Convert.ToInt32(dataTable.Rows[0][0]);
                return IdCountry;
            }
            return 0;
        }
        private void InsertCountry(HttpContext Context, int IdCapital, int IdRegion)
        {
            string Command = $@"
                INSERT INTO Countries 
                VALUES (@Name, @Code, @Capital, @Area, @Population, @Region)
            ;";

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Name", Context.Request.QueryString["Name"]),
                new Params("Code", Context.Request.QueryString["Code"]),
                new Params("Capital", IdCapital.ToString()),
                new Params("Area", Context.Request.QueryString["Area"]),
                new Params("Population", Context.Request.QueryString["Population"]),
                new Params("Region", IdRegion.ToString())
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
            SqlTransact.ExecuteNonQuery();
        }
        private void UpdateCountry(HttpContext Context, int IdCapital, int IdRegion)
        {
            string Command = $@"
                UPDATE Countries 
                SET Name = @Name, Code = @Code, Capital = @Capital, Area = @Area, Population = @Population, Region = @Region 
                FROM Countries 
                WHERE Code = @Code
            ;";

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Name", Context.Request.QueryString["Name"]),
                new Params("Code", Context.Request.QueryString["Code"]),
                new Params("Capital", IdCapital.ToString()),
                new Params("Area", Context.Request.QueryString["Area"]),
                new Params("Population", Context.Request.QueryString["Population"]),
                new Params("Region", ToString())
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
            SqlTransact.ExecuteNonQuery();
        }
        private DataTable GetCountry(HttpContext Context)
        {
            string Command = $@"
                SELECT Countries.Name, Countries.Code, Cities.Name as 'Capital', Countries.Area, Countries.Population, Regions.Name as 'Region' 
                FROM Countries, Cities, Regions 
                WHERE (Countries.Code = @Code) 
                    AND (Cities.Id = Countries.Capital) 
                    AND (Regions.Id = Countries.Region);
            ;";

            List<Params> ParamsList = new List<Params>()
            {
                new Params("Code", Context.Request.QueryString["Code"])
            };

            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString, ParamsList);
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
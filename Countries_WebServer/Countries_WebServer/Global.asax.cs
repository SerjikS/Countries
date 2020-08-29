using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace Countries_WebServer
{
    public class Global : System.Web.HttpApplication
    {   
        /*
        private static string PathNoAPP = $@"{AppDomain.CurrentDomain.BaseDirectory}";
        private static string PathApp = $@"App_Data\";
        private static string Document = "Countries.mdf";
        */
        protected void Application_Start(object sender, EventArgs e)
        {
            /*
            try
            {
                if (File.Exists(PathNoAPP + PathApp + Document) == true)
                {

                }
                else
                {
                    byte[] a = File.ReadAllBytes(PathNoAPP + Document);
                    File.WriteAllBytes(PathNoAPP + PathApp + Document, a);
                }
            }
            finally
            {

            }
            */
            string Command = $@"
			    CREATE TABLE [Countries] (
                    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
                    [Name]  NVARCHAR(50) UNIQUE NOT NULL,
                    [Code]  NVARCHAR(50) UNIQUE NOT NULL,
                    [Capital] INT NOT NULL,
	                [Area] FLOAT(53) NOT NULL,
	                [Population] INT NOT NULL,
	                [Region] INT NOT NULL
            )";
            SQL SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();

            Command = $@"
                CREATE TABLE [Cities] (
                    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
                    [Name]  NVARCHAR(50) UNIQUE NOT NULL
            )";

            SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();

            Command = $@"
                CREATE TABLE [Regions] (
                    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
                    [Name]  NVARCHAR(50) UNIQUE NOT NULL
            )";

            SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();

            Command = $@"
                INSERT INTO [Regions]  
                VALUES 
                    (N'Евразия'),
                    (N'Европа');
            ";

            SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();

            Command = $@"
                INSERT INTO [Cities]  
                VALUES 
                    (N'Москва'),
                    (N'Киев'),
                    (N'Минск');
            ";

            SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();

            Command = $@"
                INSERT INTO [Countries] VALUES 
                    (N'Россия', '7', 1, 17098246.1, 146748590, 1),
                    (N'Украина', '380', 2, 603628, 41806221, 2),
                    (N'Беларусь', '375', 3, 207600, 9408400, 2)
            ;";

            SqlTransact = new SQL(Command, ConfigurationManager.ConnectionStrings["CountriesDBConnection"].ConnectionString);
            SqlTransact.ExecuteNonQuery();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
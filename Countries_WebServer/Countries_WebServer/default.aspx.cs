using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;

using System.IO;

namespace Countries_WebServer
{
    // https://metanit.com/sharp/net/2.1.php
    // Countries.cs удалить
    // Переимменовать Database.mdf
    // https://localhost:44347/Default.aspx?Country=All
    // Переделать в JSON/XML
    /*
         CREATE TABLE [dbo].[Countries]
    (
	    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	    [Name]  NVARCHAR(50) UNIQUE NOT NULL,
	    [Code] INT UNIQUE NOT NULL,
	    [Capital] NVARCHAR(50) UNIQUE NOT NULL,
	    [Area] FLOAT NOT NULL,
	    [Population] INT NOT NULL,
	    [Region] NVARCHAR(50) NOT NULL
    )

    INSERT INTO [dbo].[Countries] ([Name], [Code], [Capital], [Area], [Population], [Region]) VALUES (N'Россия', 1, N'Москва', 17098246, 146748590, N'Евразия')
    INSERT INTO [dbo].[Countries] ([Name], [Code], [Capital], [Area], [Population], [Region]) VALUES (N'Украина', 2, N'Киев', 603628, 41806221,  N'Европа')
    INSERT INTO [dbo].[Countries] ([Name], [Code], [Capital], [Area], [Population], [Region]) VALUES (N'Беларусь', 3, N'Минск', 207600, 9408400, N'Европа')

    SELECT [Name], [Code], [Capital], [Area], [Population], [Region] FROM [Countries] WHERE [Name] = "Россия"

    Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True
    */
    /*
    str = @"CREATE DATABASE [123]
CONTAINMENT = NONE
ON PRIMARY
(NAME = N'123', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\123.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
LOG ON
(NAME = N'123_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\123_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )";
*/
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Unload(object sender, EventArgs e)
        {

        }
    }
}
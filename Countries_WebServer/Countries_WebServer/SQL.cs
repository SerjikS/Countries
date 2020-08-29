using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.Json;

namespace Countries_WebServer
{
    public class Params
    {
        public string NameInSql { get; set; }
        public string NameInLink { get; set; }
        public Params(string NameInSql, string NameInLink)
        {
            this.NameInLink = NameInLink;
            this.NameInSql = NameInSql;
        }
    }

    public class SQL
    {
        public string Command;
        public SqlConnection Connection;
        public List<int> IngonreErrors = new List<int>()
        {
            2714, // Table already exists 
            2627 // UNIQE var
        };
        public List<Params> Parameters = new List<Params>();

        public SQL(string command, string connection)
        {
            this.Command = command;
            this.Connection = new SqlConnection(connection);
        }

        public SQL(string command, string connection, List<Params> Params)
        {
            this.Command = command;
            this.Connection = new SqlConnection(connection);
            this.Parameters.AddRange(Params);
        }

        public void ExecuteNonQuery()
        {
            SqlCommand sqlCommand = new SqlCommand(Command, Connection);
            try
            {
                Connection.Open();

                for (int i = 0; i != Parameters.Count; i++)
                {
                    sqlCommand.Parameters.AddWithValue(Parameters[0].NameInSql, Parameters[0].NameInLink);
                }

                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (IngonreErrors.Contains(ex.Number))
                {

                }
                else
                {

                }
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        public DataTable ExecuteReader()
        {
            SqlCommand sqlCommand = new SqlCommand(Command, Connection);
            SqlDataReader sqlDataReader = null;
            DataTable dataTable = new DataTable();

            for (int i = 0; i != Parameters.Count; i++)
            {
                sqlCommand.Parameters.AddWithValue(Parameters[i].NameInSql, Parameters[i].NameInLink);
            }

            try
            {
                Connection.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                sqlDataReader.Close();
                Connection.Close();
            }
            finally
            {

            }

            return dataTable;
        }
    }
}

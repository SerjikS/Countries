using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data;

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
        /// <summary>
        /// <param name="Command">SQL Команда</param>
        /// </summary>
        public string Command;
        /// <summary>
        /// SQL Библиотека
        /// <param name="Connection">SQL Подключение</param>
        /// </summary>
        public SqlConnection Connection;
        /// <summary>
        /// <param name="Params">HTTP Параметры</param>
        /// </summary>
        public List<Params> Parameters = new List<Params>();

        public List<int> IngonreErrors = new List<int>()
        {
            2714, // Table already exists 
            2627 // Uniqe var
        };

        /// <summary>
        /// SQL Библиотека
        /// <param name="command">SQL Команда</param>
        /// <param name="connection">SQL Подключение</param>
        /// </summary>
        public SQL(string command, string connection)
        {
            this.Command = command;
            this.Connection = new SqlConnection(connection);
        }

        /// <summary>
        /// SQL Библиотека
        /// <param name="command">SQL Команда</param>
        /// <param name="connection">SQL Подключение</param>
        /// <param name="Params">HTTP Параметры</param>
        /// </summary>
        public SQL(string command, string connection, List<Params> Params)
        {
            this.Command = command;
            this.Connection = new SqlConnection(connection);
            this.Parameters.AddRange(Params);
        }

        /// <summary>
        /// Выполнить SQL запрос без ответа
        /// </summary>
        public void ExecuteNonQuery()
        {
            SqlCommand sqlCommand = new SqlCommand(Command, Connection);

            try
            {
                Connection.Open();

                for (int i = 0; i != Parameters.Count; i++)
                {
                    sqlCommand.Parameters.AddWithValue(Parameters[i].NameInSql, Parameters[i].NameInLink);
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

        /// <summary>
        /// Выполнить SQL запрос с возвратом данных
        /// </summary>
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

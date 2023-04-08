using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace BL_Web_Server
{
    public class DBAccess
    {
        private static MySqlConnection connection = new MySqlConnection();
        private static MySqlCommand command = new MySqlCommand();
        private static MySqlDataReader DbReader;
        private static MySqlDataAdapter adapter = new MySqlDataAdapter();
        public MySqlTransaction DbTran;


        String server = "";
        String database = "";
        String uid = "";
        String password = "";
        String strConnString;


        // private static string strConnString = "Data Source=localhost;Initial Catalog=studentdb;uid=root;password=my@pass ";



        public void createConn()
        {

            server = "localhost";
            database = "blacklotus";
            uid = "root";
            password = "root";

            strConnString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "user id=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(strConnString);

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void closeConn()
        {
            connection.Close();
        }


        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                MySqlCommandBuilder DbCommandBuilder = new MySqlCommandBuilder(adapter);


                string insert = DbCommandBuilder.GetInsertCommand().CommandText.ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();


                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new MySqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public MySqlDataReader readDatathroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            MySqlDataReader reader;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int executeQuery(MySqlCommand dbCommand)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;


                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
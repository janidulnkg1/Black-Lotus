using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BL_Web_Server
{
    /// <summary>
    /// Summary description for BLWeb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BLWeb : System.Web.Services.WebService
    {
        
        private MySqlConnection connection = new MySqlConnection();
        private MySqlCommand command = new MySqlCommand();
        private MySqlDataReader DbReader;
        private MySqlDataAdapter adapter = new MySqlDataAdapter();
        public MySqlTransaction DbTran;

        String server = "";
        String database = "";
        String uid = "";
        String password = "";
        String strConnString;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool createConn()
        {
            server = "localhost";
            database = "blacklotus";
            uid = "root";
            password = "root";
            string strConnString;
            strConnString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "user id=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(strConnString);

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool insertuserdata(string userFirstname, string userLastname, string userLoginname, string userLoginPassword,string userType )
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string insertCommand = "INSERT INTO userdetails (userFname,userLname, userLoginName,userLoginPassword,userType) VALUES('" + userFirstname + "','" + userLastname + "','" + userLoginname + "','" + userLoginPassword + "','" + userType + "')";

                MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool updateuserdata(string userFirstname, string userLastname, string userLoginname, string userLoginPassword, string userType, string userid)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string updateCommand = "UPDATE userdetails SET userFname = '"+userFirstname+"' , userLname='"+userLastname+"', userLoginName = '"+userLoginname+"',userLoginPassword = '"+userLoginPassword+"',userType = '"+userType+ "') WHERE userID='" + userid + "')";

                MySqlCommand cmd = new MySqlCommand(updateCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool deleteuserdata(string userid)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string deleteCommand = "DELETE FROM userdetails WHERE userID='" + userid + "')";

                MySqlCommand cmd = new MySqlCommand(deleteCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

       DBAccess getInfo = new DBAccess();
        DataTable tblgetUsers = new DataTable();
        DataTable tblgetorders = new DataTable();
        DataTable tblgetcategory = new DataTable();
        DataTable tblgetflowerdetails = new DataTable();
        DataTable tblgetlogin = new DataTable();

        [WebMethod]
        public string retrieveuserdata(string userid)
        {
            string searchSQL = "SELECT * FROM userdetails WHERE userID='" + userid + "'";
            getInfo.readDatathroughAdapter(searchSQL, tblgetUsers);

            string result = JsonConvert.SerializeObject(tblgetUsers);
            return result;
        }

        [WebMethod]
        public string retrieveAlluserdata()
        {
            string searchSQL = "SELECT * FROM userdetails";
            getInfo.readDatathroughAdapter(searchSQL, tblgetUsers);

            string result = JsonConvert.SerializeObject(tblgetUsers);
            return result;
        }

        [WebMethod]
        public bool insertcategorydata(string categoryname,string categorycost)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string insertCommand = "INSERT INTO category (categoryName,categoryCost) VALUES('" + categoryname + "','" + categorycost + "')";

                MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool updatecategorydata(string categoryid, string categoryname, string categorycost )
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string updateCommand = "UPDATE category SET categoryName ='" + categoryname + "',categoryCost ='" + categorycost + "'  WHERE categoryID='" + categoryid + "'";

                MySqlCommand cmd = new MySqlCommand(updateCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool deletecategorydata( string categoryid)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string deleteCommand = "DELETE FROM category  WHERE categoryID='" + categoryid + "'";

                MySqlCommand cmd = new MySqlCommand(deleteCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public string retrievecategorydata(string categoryid)
        {
            string searchSQL = "SELECT * FROM category WHERE categoryID='" + categoryid + "'";
            getInfo.readDatathroughAdapter(searchSQL, tblgetcategory);

            string result = JsonConvert.SerializeObject(tblgetcategory);
            return result;
        }

        [WebMethod]
        public string retrieveAllcategorydata()
        {
            string searchSQL = "SELECT * FROM category";
            getInfo.readDatathroughAdapter(searchSQL, tblgetcategory);

            string result = JsonConvert.SerializeObject(tblgetcategory);
            return result;
        }

        [WebMethod]
        public bool insertorderdata(string orderDate, string orderCategory, string orderItemName,string orderQuantity, string orderAmount, string customerTitle,string customerFname, string customerLname,string customerContactno, string orderStatus)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string insertCommand = "INSERT INTO orderdetails (orderDate,orderCategory,orderItemName,orderQuantity,orderAmount,customerTitle,customerFname,customerLname,customerContactno,orderStatus) VALUES('" + orderDate + "','" + orderCategory + "','" + orderItemName + "','" + orderQuantity + "','" + orderAmount + "','" + customerTitle + "','" + customerFname + "','" + customerLname + "','" + customerContactno + "','" + orderStatus + "')";

                MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool updateorderdata(string orderID, string orderStatus )
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string updateCommand = "UPDATE orderdetails SET orderStatus = '"+orderStatus+ "' WHERE orderID='" + orderID + "')";

                MySqlCommand cmd = new MySqlCommand(updateCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool deleteorderdata( string orderid)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string deleteCommand = "DELETE FROM orderdetails  WHERE orderID='" + orderid + "')";

                MySqlCommand cmd = new MySqlCommand(deleteCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public string retrieveorderdata(string orderid)
        {
            string searchSQL = "SELECT * FROM orderdetails WHERE orderID='" + orderid + "'";
            getInfo.readDatathroughAdapter(searchSQL, tblgetorders);

            string result = JsonConvert.SerializeObject(tblgetorders);
            return result;
        }

        [WebMethod]
        public string retrieveAllorderdata()
        {
            string searchSQL = "SELECT * FROM orderdetails";
            getInfo.readDatathroughAdapter(searchSQL, tblgetorders);

            string result = JsonConvert.SerializeObject(tblgetorders);
            return result;
        }

        [WebMethod]
        public bool insertflowerdata( string flowername, string flowerprice, string flowerstock)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string insertCommand = "INSERT INTO flowerdetails (flowerName,flowerPrice, flowerStock) VALUES('" + flowername + "','" + flowerprice + "','" + flowerstock + "')";

                MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool updateflowerdata(string flowerid, string flowername, string flowerprice, string flowerstock)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string updateCommand = "UPDATE flowerdetails SET flowerName ='" + flowername + "',flowerPrice='" + flowerprice + "',flowerStock='" + flowerstock + "' WHERE flowerID='" + flowerid + "'";

                MySqlCommand cmd = new MySqlCommand(updateCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public bool deleteflowerdata(string flowerid)
        {
            try
            {
                this.createConn();

                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }

                string deleteCommand = "DELETE FROM flowerdetails WHERE flowerID='" + flowerid + "'";

                MySqlCommand cmd = new MySqlCommand(deleteCommand, connection);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {

                return false;
            }
        }

        [WebMethod]
        public string retrieveflowerdata(string flowerid)
        {
            string searchSQL = "SELECT * FROM flowerdetails WHERE flowerID='" + flowerid + "'";
            getInfo.readDatathroughAdapter(searchSQL, tblgetflowerdetails);

            string result = JsonConvert.SerializeObject(tblgetflowerdetails);
            return result;
        }

        [WebMethod]
        public string retrieveAllflowerdata()
        {
            string searchSQL = "SELECT * FROM flowerdetails";
            getInfo.readDatathroughAdapter(searchSQL, tblgetflowerdetails);

            string result = JsonConvert.SerializeObject(tblgetflowerdetails);
            return result;
        }


        [WebMethod]
        public string getLogin()
        {
            string searchSQL = "SELECT * FROM userdetails";
            getInfo.readDatathroughAdapter(searchSQL, tblgetlogin);

            string result = JsonConvert.SerializeObject(tblgetlogin);
            return result;
        }
    }
}

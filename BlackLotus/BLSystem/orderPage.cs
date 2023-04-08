using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace BLSystem
{
    public partial class orderPage : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        string oldOrderID;
        string newOrderID;
        public orderPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            userMainPanel mainpanel = new userMainPanel();
            mainpanel.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void orderPage_Load(object sender, EventArgs e)
        {
            webSetting();
            this.selectAllOrders();
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            String orderid = txtOrderID.Text;
            String orderstatus = cmbOrderStatus.Text;
            //String orderstatus = this.cmbOrderStatus.GetItemText(this.cmbOrderStatus.SelectedItem);

            try
            {
                if (getInfofromService.updateorderdata( orderstatus, orderid))
                {
                    MessageBox.Show("Order has been updated successfully..!");

                }
                else
                {
                    MessageBox.Show("Order has not been updated..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemoveOrder_Click(object sender, EventArgs e)
        {
            String orderid = txtOrderID.Text;

            try
            {
                if (getInfofromService.deleteorderdata(orderid))
                {
                    MessageBox.Show("Order has been cancelled successfully..!");

                }
                else
                {
                    MessageBox.Show("Order has not been cancelled..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private DataTable stringSplit(string userJason)
        {
            string[] json = userJason.Split('>');
            string[] finalJason = json[2].Split('<');
            //string[] finalJason1 = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJason[0]);
            return dt;

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.ToString();
            HttpResponseMessage msg = client.GetAsync("retrieveorderdata?orderid=" + search + "").Result;
            string uj = msg.Content.ReadAsStringAsync().Result;
            tblOrders.DataSource = stringSplit(uj);
            tblOrders.Columns["orderID"].HeaderText = "Order ID";
            tblOrders.Columns["orderDate"].HeaderText = "Order Date";
            tblOrders.Columns["orderCategory"].HeaderText = "Order Category";
            tblOrders.Columns["orderItemName"].HeaderText = "Item Name";
            tblOrders.Columns["orderQuantity"].HeaderText = "Quantity";
            tblOrders.Columns["orderAmount"].HeaderText = "Total Amount";
            tblOrders.Columns["customerTitle"].HeaderText = "Customer Title";
            tblOrders.Columns["customerFname"].HeaderText = "Customer First Name";
            tblOrders.Columns["customerLname"].HeaderText = "Customer Last Name";
            tblOrders.Columns["orderStatus"].HeaderText = "Order Status";
            tblOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void selectAllOrders()
        {
            HttpResponseMessage msg = client.GetAsync("retrieveAllorderdata").Result;
            string uj = msg.Content.ReadAsStringAsync().Result;
            tblOrders.DataSource = stringSplit(uj);

            tblOrders.Columns["orderID"].HeaderText = "Order ID";
            tblOrders.Columns["orderDate"].HeaderText = "Order Date";
            tblOrders.Columns["orderCategory"].HeaderText = "Order Category";
            tblOrders.Columns["orderItemName"].HeaderText = "Item Name";
            tblOrders.Columns["orderQuantity"].HeaderText = "Quantity";
            tblOrders.Columns["orderAmount"].HeaderText = "Total Amount";
            tblOrders.Columns["customerTitle"].HeaderText = "Customer Title";
            tblOrders.Columns["customerFname"].HeaderText = "Customer First Name";
            tblOrders.Columns["customerLname"].HeaderText = "Customer Last Name";
            tblOrders.Columns["customerContactno"].HeaderText = "Customer Contact No";
            tblOrders.Columns["orderStatus"].HeaderText = "Order Status";
            tblOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void tblOrders_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtOrderID.ReadOnly = true;
            int row = e.RowIndex;
            txtOrderID.Text = Convert.ToString(tblOrders[0, row].Value);
            oldOrderID = txtOrderID.Text;        
            cmbOrderStatus.Text = Convert.ToString(tblOrders[10, row].Value);
        }

        private void btnAllOrders_Click(object sender, EventArgs e)
        {
            this.selectAllOrders();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtOrderID.ReadOnly = false;
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace BlackLotusCLIENT
{
    public partial class placeOrders : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        public placeOrders()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            customerMain logout = new customerMain();
            logout.Show();
            this.Hide();
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            String orderDate = dtpOrderDate.Text;
            String orderCategory = txtOrderCategory.Text;
            String orderItemname = txtOrderItemName.Text;
            String orderQuantity = txtOrderQuantity.Text;
            String orderAmount = txtOrderAmount.Text;
            String customertitle = cmbCustomerTitle.Text;
            String customerfname = txtCustomerFirstname.Text;
            String customerlname = txtCustomerLastName.Text;
            String customercontactno = txtCustomerContactNo.Text;
            String orderStatus = "Order Pending";

            dtpOrderDate.Value = DateTime.Today.Add(dtpOrderDate.Value.TimeOfDay);

            try
            {
                if (getInfofromService.insertorderdata(orderDate, orderCategory, orderItemname, orderQuantity, orderAmount,customertitle,customerfname,customerlname,customercontactno,orderStatus))
                {
                    MessageBox.Show("Order has been added..!");
                    

                }
                else
                {
                    MessageBox.Show("Order has not been added..!");
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

        public void selectAllFlowers()
        {
            try
            {
                HttpResponseMessage msg = client.GetAsync("retrieveAllflowerdata").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;

                tblFlowers.DataSource = stringSplit(uj);
                tblFlowers.Columns["flowerID"].HeaderText = "Flower ID";
                tblFlowers.Columns["flowerName"].HeaderText = "Flower Name";
                tblFlowers.Columns["flowerPrice"].HeaderText = "Flower Price Name";
                tblFlowers.Columns["flowerStock"].HeaderText = "Flower Stock";
                tblFlowers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! :" + ex.Message);
            }
        }



        private void tblFlowers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           // txtflowerID.ReadOnly = true;
            int row = e.RowIndex;
            //txtflowerID.Text = Convert.ToString(tblFlowers[0, row].Value);
            //oldflowerID = txtflowerID.Text;
            txtOrderItemName.Text = Convert.ToString(tblFlowers[1, row].Value);
            txtItemPrice.Text = Convert.ToString(tblFlowers[2, row].Value);
            //txtStockAmount.Text = Convert.ToString(tblFlowers[3, row].Value);
        }

        private void placeOrders_Load(object sender, EventArgs e)
        {
            webSetting();
            this.selectAllFlowers();
            this.selectAllCategories();
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }     

        public void selectAllCategories()
        {
            try
            {
                HttpResponseMessage msg = client.GetAsync("retrieveAllcategorydata").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;

                tblCategory.DataSource = stringSplit(uj);
                //tblCategory.Columns["categoryID"].HeaderText = "Category ID";
                tblCategory.Columns["categoryName"].HeaderText = "Category Name";
                tblCategory.Columns["categoryCost"].HeaderText = "Category Price";

                tblCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! :" + ex.Message);
            }
        }

        private void tblCategory_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            //txtCategoryID.ReadOnly = true;
            int row = e.RowIndex;
            //txtCategoryID.Text = Convert.ToString(tblCategory[0, row].Value);
            //oldcategoryID = txtCategoryID.Text;
            txtOrderCategory.Text = Convert.ToString(tblCategory[1, row].Value);
            txtCategoryPrice.Text = Convert.ToString(tblCategory[2, row].Value);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            float CategoryPrice, OrderQuantity, PriceperItem, OrderAmount;

           CategoryPrice = float.Parse(txtCategoryPrice.Text);
            PriceperItem = float.Parse(txtItemPrice.Text);
            OrderQuantity = float.Parse(txtOrderQuantity.Text);

            OrderAmount = CategoryPrice + (PriceperItem*OrderQuantity);
          txtOrderAmount.Text = OrderAmount.ToString();
        }
    }
}

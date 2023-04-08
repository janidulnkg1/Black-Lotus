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

namespace BLSystem
{
    public partial class flowerStockPage : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        string oldflowerID;
        string newflowerID;
        public flowerStockPage()
        {
            InitializeComponent();
        }     

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult Clear = MessageBox.Show(" Are you sure to \n clear all!", "Clear All", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Clear == DialogResult.Yes)
            {
                txtflowerID.Text = "";
                txtFlowerName.Text = "";
                txtPricePerFlower.Text = "";
                txtStockAmount.Text = "";
                txtSearch.Text = "";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            userMainPanel mainpanel = new userMainPanel();
            mainpanel.Show();
            this.Hide();
        }

        private void btnAddFlowers_Click(object sender, EventArgs e)
        {
           String flowerName = txtFlowerName.Text;
            String flowerPrice = txtPricePerFlower.Text;
            String flowerStockAmount = txtStockAmount.Text;

            try
            {
                if (getInfofromService.insertflowerdata(flowerName,flowerPrice,flowerStockAmount))
                {
                    MessageBox.Show("Flower Stock data have been added successfully..!");
                    txtflowerID.Text = "";
                    txtFlowerName.Text = "";
                    txtPricePerFlower.Text = "";
                    txtStockAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("Flower Stock data have not been added..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnUpdateFlowers_Click(object sender, EventArgs e)
        {
            String flowerName = txtFlowerName.Text;
            String flowerPrice = txtPricePerFlower.Text;
            String flowerStockAmount = txtStockAmount.Text;
            String flowerid = txtflowerID.Text;

            try
            {
                if (getInfofromService.updateflowerdata(flowerName, flowerPrice, flowerStockAmount,flowerid))
                {
                    MessageBox.Show("Flower Stock data has been updated successfully..!");
                    txtflowerID.Text = "";
                    txtFlowerName.Text = "";
                    txtPricePerFlower.Text = "";
                    txtStockAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("Flower Stock data has not been updated..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemoveFlower_Click(object sender, EventArgs e)
        {         
            
            String flowerid = txtflowerID.Text;

            try
            {
                if (getInfofromService.deleteflowerdata(flowerid))
                {
                    MessageBox.Show("Flower Stock data has been deleted successfully..!");
                    txtflowerID.Text = "";
                    txtFlowerName.Text = "";
                    txtPricePerFlower.Text = "";
                    txtStockAmount.Text = "";
                }
                else
                {
                    MessageBox.Show("Flower Stock data has not been deleted..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void flowerStockPage_Load(object sender, EventArgs e)
        {
            webSetting();
            this.selectAllFlowers();
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void btnAllUsers_Click(object sender, EventArgs e)
        {
            this.selectAllFlowers();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.ToString();
            HttpResponseMessage msg = client.GetAsync("retrieveflowerdata?flowerid=" + search + "").Result;
            string uj = msg.Content.ReadAsStringAsync().Result;
            tblFlowers.DataSource = stringSplit(uj);
            tblFlowers.Columns["flowerID"].HeaderText = "Flower ID";
            tblFlowers.Columns["flowerName"].HeaderText = "Flower Name";
            tblFlowers.Columns["flowerPrice"].HeaderText = "Flower Price Name";
            tblFlowers.Columns["flowerStock"].HeaderText = "Flower Stock";
            tblFlowers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.ReadOnly = false;
        }

        private void tblFlowers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtflowerID.ReadOnly = true;
            int row = e.RowIndex;
            txtflowerID.Text = Convert.ToString(tblFlowers[0, row].Value);
            oldflowerID = txtflowerID.Text;
            txtFlowerName.Text = Convert.ToString(tblFlowers[1, row].Value);
           txtPricePerFlower.Text = Convert.ToString(tblFlowers[2, row].Value);
            txtStockAmount.Text = Convert.ToString(tblFlowers[3, row].Value);
          
        }
    }
    }


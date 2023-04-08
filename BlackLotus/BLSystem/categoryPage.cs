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
    public partial class categoryPage : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        string oldcategoryID;
        string newcategoryID;
        public categoryPage()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCategoryType.Text = "";
            txtCategoryCost.Text = "";
            txtCategoryID.Text = "";
            txtSearch.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            userMainPanel mainpanel = new userMainPanel();
            mainpanel.Show();
            this.Hide();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {

            String categoryname = txtCategoryType.Text;
            String categorycost = txtCategoryCost.Text;

      

            try
            {
                if (getInfofromService.insertcategorydata(categoryname, categorycost))
                {
                    MessageBox.Show("Category data have been added successfully..!");
                    txtCategoryType.Text = "";
                    txtCategoryCost.Text = "";
                    txtCategoryType.Text = "";
                }
                else
                {
                    MessageBox.Show("Category data have not been added..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            String categoryid = txtCategoryID.Text;
            String categoryname = txtCategoryType.Text;
            String categorycost = txtCategoryCost.Text;

            try
            {
                if (getInfofromService.updatecategorydata(categoryname, categorycost, categoryid))
                {
                    MessageBox.Show("Category data have been updated successfully..!");
                    txtCategoryType.Text = "";
                    txtCategoryCost.Text = "";
                    txtCategoryType.Text = "";
                }
                else
                {
                    MessageBox.Show("Category data have not been updated ..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            String categoryid = txtCategoryID.Text;


            try
            {
                if (getInfofromService.deletecategorydata(categoryid))
                {
                    MessageBox.Show("Category data has been deleted successfully..!");
                    txtCategoryType.Text = "";
                    txtCategoryCost.Text = "";
                    txtCategoryType.Text = "";
                }
                else
                {
                    MessageBox.Show("Category data has not been deleted  ..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void categoryPage_Load(object sender, EventArgs e)
        {
            webSetting();
            this.selectAllCategories();
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }

        private DataTable stringSplit(string userJason)
        {
            string[] json = userJason.Split('>');
            string[] finalJason = json[2].Split('<');
            //string[] finalJason1 = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJason[0]);
            return dt;

        }
        public void selectAllCategories()
        {
            try
            {
                HttpResponseMessage msg = client.GetAsync("retrieveAllcategorydata").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;

                tblCategory.DataSource = stringSplit(uj);
                tblCategory.Columns["categoryID"].HeaderText = "Category ID";
                tblCategory.Columns["categoryName"].HeaderText = "Category Name";
                tblCategory.Columns["categoryCost"].HeaderText = "Flower Price Name";
               
                tblCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! :" + ex.Message);
            }
        }

        private void tblCategory_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCategoryID.ReadOnly = true;
            int row = e.RowIndex;
            txtCategoryID.Text = Convert.ToString(tblCategory[0, row].Value);
            oldcategoryID = txtCategoryID.Text;
            txtCategoryType.Text = Convert.ToString(tblCategory[1, row].Value);
           txtCategoryCost.Text = Convert.ToString(tblCategory[2, row].Value);
         
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.ToString();
            HttpResponseMessage msg = client.GetAsync("retrievecategorydata?categoryid=" + search + "").Result;
            string uj = msg.Content.ReadAsStringAsync().Result;
            tblCategory.DataSource = stringSplit(uj);
            tblCategory.DataSource = stringSplit(uj);
            tblCategory.Columns["categoryID"].HeaderText = "Category ID";
            tblCategory.Columns["categoryName"].HeaderText = "Category Name";
            tblCategory.Columns["categoryCost"].HeaderText = "Flower Price Name";
            tblCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAllCategories_Click(object sender, EventArgs e)
        {
            this.selectAllCategories();
        }
    }
    }


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
    public partial class adminPage : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        string oldUserID;
        string newUserID;
        public adminPage()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult Clear = MessageBox.Show(" Are you sure to \n clear all!", "Clear All", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Clear == DialogResult.Yes)
            {
                txtFname.Text = "";
                txtLname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtcPassword.Text = "";
                txtSearch.Text = "";
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            DialogResult password = MessageBox.Show(" Are you sure to use\n this password!", "Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);


            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password \n must be \n6 Characters or more", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtPassword.Focus();

            }


            if (password == DialogResult.No)
            {
                txtPassword.Text = "";
            }
        }

        private void txtcPassword_Leave(object sender, EventArgs e)
        {
            if (txtcPassword.Text.Contains(txtPassword.Text))
            {
                MessageBox.Show("Passwords\n are matching!", "Password ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Passwords\n do no match!", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcPassword.Text = "";
                txtcPassword.Focus();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginPage login = new loginPage();
            login.Show();
            this.Hide();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            loginPage login = new loginPage();
            login.Show();
            this.Close();
        }

        private void adminPage_Load(object sender, EventArgs e)
        {
            webSetting();
            this.selectAllUsers();
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            String userID = txtUserID.Text;
            String Fname = txtFname.Text;
            String Lname = txtLname.Text;
            String username = txtUsername.Text;
            String password = txtPassword.Text;
            String usertype = txtUserType.Text;


            try
            {
                if (getInfofromService.updateuserdata(Fname, Lname, username, password, usertype, userID))
                {
                    MessageBox.Show("User account has been updated successfully..!");

                }
                else
                {
                    MessageBox.Show("User account has not been updated..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            String userID = txtUserID.Text;



            try
            {
                if (getInfofromService.deleteuserdata(userID))
                {
                    MessageBox.Show("User account has been deleted successfully..!");

                }
                else
                {
                    MessageBox.Show("User account has not been deleted..!");
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
          
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJason[0]);
            return dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String search = txtSearch.Text.ToString();
            //try
            //{ 
                HttpResponseMessage msg = client.GetAsync("retrieveuserdata?userid=" + search + "").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;
            
                tblUsers.DataSource = stringSplit(uj);
                tblUsers.Columns["userID"].HeaderText = "User ID";
                tblUsers.Columns["userFname"].HeaderText = "User First Name";
                tblUsers.Columns["userLname"].HeaderText = "User Last Name";
                tblUsers.Columns["userLoginName"].HeaderText = "User Login Name";
                tblUsers.Columns["userLoginPassword"].HeaderText = "User Login Password";
                tblUsers.Columns["userType"].HeaderText = "User Type";          
                tblUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //}
            //catch(Exception ex)
            //{  
            //    MessageBox.Show("Error! :" + ex.Message);
            //}
        }

        public void selectAllUsers()
        {
            try { 
                HttpResponseMessage msg = client.GetAsync("retrieveAlluserdata").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;
                
                tblUsers.DataSource = stringSplit(uj);
                tblUsers.Columns["userID"].HeaderText = "User ID";
                tblUsers.Columns["userFname"].HeaderText = "User First Name";
                tblUsers.Columns["userLname"].HeaderText = "User Last Name";
                tblUsers.Columns["userLoginName"].HeaderText = "User Login Name";
                tblUsers.Columns["userLoginPassword"].HeaderText = "User Login Password";
                tblUsers.Columns["userType"].HeaderText = "User Type";
                tblUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! :" + ex.Message);
            }
        }

        private void btnAllUsers_Click(object sender, EventArgs e)
        {
            this.selectAllUsers();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtUserID.ReadOnly = false;
        }

        private void tblUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtUserID.ReadOnly = true;
            int row = e.RowIndex;
            txtUserID.Text = Convert.ToString(tblUsers[0, row].Value);
            oldUserID = txtUserID.Text;
            txtFname.Text = Convert.ToString(tblUsers[1, row].Value);
            txtLname.Text = Convert.ToString(tblUsers[2, row].Value);
           txtUsername.Text = Convert.ToString(tblUsers[3, row].Value);
            txtPassword.Text = Convert.ToString(tblUsers[4, row].Value);
            txtUserType.Text = Convert.ToString(tblUsers[5, row].Value);
        }
    }
}

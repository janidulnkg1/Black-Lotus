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
    public partial class createEmployeeAccount : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        public createEmployeeAccount()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            String firstname = txtFname.Text;
            String lastname = txtLname.Text;
            String username = txtUsername.Text;
            String password = txtPassword.Text;
            String confirmpassword = txtcPassword.Text;
            String usertype = "Employee";


            try
            {
                if (getInfofromService.insertuserdata(firstname, lastname, username, password, usertype))
                {
                    MessageBox.Show("User has been added..!");
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtcPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("User has not been added..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFname.Text = "";
            txtLname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtcPassword.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginPage login = new loginPage();
            login.Show();
            this.Hide();
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
    }
}

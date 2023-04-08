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

namespace BLSystem
{
    public partial class loginPage : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        public loginPage()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(75, Color.Black);
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked == true)
                txtLoginPassword.UseSystemPasswordChar = false;
            else
                txtLoginPassword.UseSystemPasswordChar = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLoginUsername.Text = "";
            txtLoginPassword.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBypass_Click(object sender, EventArgs e)
        {
            userMainPanel mainpanel = new userMainPanel();
            mainpanel.Show();
            this.Hide();
        }

        private void loginPage_Load(object sender, EventArgs e)
        {
            if (getInfofromService.createConn())
            {
                lblDBConnection.Text = "Database connected..!!";
            }
            else
            {
                lblDBConnection.Text = "Database connection error..!!";
            }

            this.webSetting();
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            createEmployeeAccount newacc = new createEmployeeAccount();
            newacc.Show();
            this.Hide();
        }

        private void btnBypassToAdmin_Click(object sender, EventArgs e)
        {
            adminPage admin = new adminPage();
            admin.Show();
            this.Hide();
        }

        private DataTable stringSplit(string userJason)
        {
            string[] json = userJason.Split('>');
            string[] finalJason = json[2].Split('<');
            //string[] finalJason1 = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJason[0]);
            return dt;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtLoginUsername.Text;
            String password = txtLoginPassword.Text;

            if(username.Equals(""))
            {
                MessageBox.Show("Username cannot be empty!", "Username ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginUsername.Focus();
            }
            else if(password.Equals(""))
            {
                MessageBox.Show("Password cannot be empty!", "Username ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginPassword.Focus();
            }

            try
            {
                HttpResponseMessage msg = client.GetAsync("getLogin").Result;
                string uj = msg.Content.ReadAsStringAsync().Result;
                
                int rCount = stringSplit(uj).Rows.Count;
                if (rCount > 0)
                {
                    for (int count = 0; count < rCount; count++)
                    {
                        String user = stringSplit(uj).Rows[count][3].ToString();
                        String pass = stringSplit(uj).Rows[count][4].ToString();

                        if (username.Equals("Admin") && password.Equals("admin@BLS"))
                        {
                            MessageBox.Show("ADMIN login Successful!");
                            adminPage admin = new adminPage();
                            admin.Show();
                            this.Hide();
                            txtLoginPassword.Text = "";
                            txtLoginPassword.Text = "";
                            break;
                        }
                        else if (username.Equals(user) && password.Equals(pass))
                        {
                            if (stringSplit(uj).Rows[count][5].ToString().Equals("Employee"))
                            {
                                MessageBox.Show("Employee login Successful!");
                                txtLoginPassword.Text = "";
                                txtLoginPassword.Text = "";
                                userMainPanel mainpanel = new userMainPanel();
                                mainpanel.Show();
                                this.Hide();
                                break;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Invalid Login Credentials..!");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            }

        private void txtLoginUsername_Leave(object sender, EventArgs e)
        {
            String username = txtLoginUsername.Text;

            if (username.Equals(""))
            {
                MessageBox.Show("Username cannot be empty!", "Username ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginUsername.Focus();
            }
        }

        private void txtLoginPassword_Leave(object sender, EventArgs e)
        {
            String password = txtLoginPassword.Text;

            if (password.Equals(""))
            {
                MessageBox.Show("Password cannot be empty!", "Username ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoginPassword.Focus();
            }
        }

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }


    }
}


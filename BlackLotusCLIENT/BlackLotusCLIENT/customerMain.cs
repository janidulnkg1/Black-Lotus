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


namespace BlackLotusCLIENT
{
    public partial class customerMain : Form
    {
        BLWeb.BLWeb getInfofromService = new BLWeb.BLWeb();
        HttpClient client = new HttpClient();
        public customerMain()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(75, Color.Black);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        private void btnBypass_Click(object sender, EventArgs e)
        {
            placeOrders orders = new placeOrders();
            orders.Show();
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

       

        private void webSetting()
        {
            client.BaseAddress = new Uri("http://localhost:51272/BLWeb.asmx/");
        }

        private void loginPage_Load(object sender, EventArgs e)
        {
            webSetting();
        }
    }
    }


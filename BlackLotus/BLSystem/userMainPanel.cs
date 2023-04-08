using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLSystem
{
    public partial class userMainPanel : Form
    {
        public userMainPanel()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loginPage login = new loginPage();
            login.Show();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            categoryPage categories = new categoryPage();
            categories.Show();
            this.Hide();
        }

        private void btnFlowerStock_Click(object sender, EventArgs e)
        {
            flowerStockPage flowers = new flowerStockPage();
           flowers.Show();
            this.Hide();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            orderPage orders = new orderPage();
            orders.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

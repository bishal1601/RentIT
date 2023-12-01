using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rent
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var createshop = new CreateStore();
            this.Hide();
            createshop.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Electricity = new Electricity();
            this.Hide();
            Electricity.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var Payment = new Payment();
            this.Hide();
            Payment.ShowDialog();
            this.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Text = new Text();
            this.Hide();
            Text.ShowDialog();
            this.Show();
        }
    }
}

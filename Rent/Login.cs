 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using MySql.Data.MySqlClient;
using Rent.Model;

namespace Rent
{
    public partial class Login : Form
    {
        private string connectionString;
        public Login()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = Usertxt.Text;
            var password = Passwordtxt.Text;
            if (username=="" || password=="")
            {
                MessageBox.Show("Enter Username and Password");
                Usertxt.Text = "";
                Passwordtxt.Text = "";
            }
            else
            {
                using (IDbConnection db = new MySqlConnection(connectionString))
                {
                    db.Open();
                    var Data = db.Query<user>("Select * from user");
                    Console.WriteLine();

                    if (Data.Where(a => a.Username == username && a.Password == password).FirstOrDefault() != null)
                    {
                        Usertxt.Text = "";
                        Passwordtxt.Text = "";
                        
                        // MessageBox.Show("ok");
                        var dashboard = new Dashboard();
                        this.Hide();
                        dashboard.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username & Password");
                        Usertxt.Text = "";
                        Passwordtxt.Text = "";

                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var Reset = new Reset();
            this.Hide();
            Reset.ShowDialog();
            this.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var Register = new Register();
            this.Hide();
            Register.ShowDialog();
            this.Show();
        }
    }
}

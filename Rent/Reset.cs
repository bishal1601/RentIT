using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Reset : Form
    {
        private string connectionString;
        public Reset()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var newpassword = Newpassword.Text;
            var usernanme = Username.Text;
            var email = Email.Text;
            
            //MessageBox.Show(Repassword);
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                var Data = db.Query<user>("Select * from user");
                if (Data.Where(a => a.Username == usernanme && a.Email == email).FirstOrDefault() != null)
                {
                    var UpdateQuery = "UPDATE user SET password = @repassword WHERE username = @username";
                    var affectedRows = db.Execute(UpdateQuery, new
                    {
                        repassword = newpassword,
                        username = usernanme
                    });

                    MessageBox.Show("Password Reset Success");
                    var login = new Login();
                    this.Close();
                    login.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("User Not Found");
                }
                
            }

        }
    }
}

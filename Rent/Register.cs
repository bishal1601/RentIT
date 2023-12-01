using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using MySql.Data.MySqlClient;
using Rent.Model;

namespace Rent
{
    public partial class Register : Form
    {
        private string connectionString;

        public Register()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fname = Fname.Text;
            var lname = Lname.Text;
            var address = Address.Text;
            var contact = Contact.Text;
            var email = Email.Text;
            var username = Username.Text;
            var password = Password.Text;
            var repassword = Repassword.Text;
            if (fname=="")
            {
                MessageBox.Show("Enter First Name");
            }
            else if(lname=="")
            {
                MessageBox.Show("Enter Last Name");
            }
            else if(address=="")
            {
                MessageBox.Show("Enter Address");
            }
            else if (contact == "")
            {
                MessageBox.Show("Enter Contact");
            }
            else if (email == "")
            {
                MessageBox.Show("Enter Email");
            }
            else if (username == "")
            {
                MessageBox.Show("Enter Username");
            }
            else if (password == "")
            {
                MessageBox.Show("Enter Password");
            }
            else if (repassword == "")
            {
                MessageBox.Show("Enter Re-Type Password");
            }
            else
            {
                using (IDbConnection db = new MySqlConnection(connectionString))
                {
                    db.Open();
                    var Data = db.Query<user>("SELECT * FROM user");

                    if (Data.Any(a => a.Username == username))
                    {
                        MessageBox.Show("Username already exists");
                        Username.Text = "";
                    }
                    else
                    {
                        if (password != repassword)
                        {
                            MessageBox.Show("Password and Repassword did not match");
                            Password.Text = "";
                            Repassword.Text = "";
                        }
                        else
                        {

                            var insertQuery = "INSERT INTO user (firstname, lastname, address, contactno, email, username, password) " +
                                              "VALUES (@FirstName, @LastName, @Address, @ContactNo, @Email, @Username, @Password)";

                            var affectedRows = db.Execute(insertQuery, new
                            {
                                FirstName = fname,
                                LastName = lname,
                                Address = address,
                                ContactNo = contact,
                                Email = email,
                                Username = username,
                                Password = password
                            });

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Account created");
                                var login = new Login();
                                this.Close();
                                login.ShowDialog();
                                this.Show();

                            }
                        }
                    }
                }
            }
        }

        private void Repassword_TextChanged(object sender, EventArgs e)
        {
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}

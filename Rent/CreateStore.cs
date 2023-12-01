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
    public partial class CreateStore : Form
    {
        private string connectionString;
        public CreateStore()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CreateStore_Load(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sname = Shopname.Text;
            string vat = Vat.Text;
            string oname = OwnerName.Text;
            string contact = Contact.Text;
            string address = Address.Text;
            string date = OpeningDate.Text;
            if (int.TryParse(Rent.Text, out int rent) && int.TryParse(Advance.Text, out int advance))
            {
                using (IDbConnection db = new MySqlConnection(connectionString))
                {
                    db.Open();

                    // Check if the shop name already exists
                    var existingShop = db.QueryFirstOrDefault<Shop>("SELECT * FROM shopdetails WHERE Sname = @Shopname", new { Shopname = sname });

                    if (existingShop != null)
                    {
                        MessageBox.Show("Shop Name is already in use: ");
                        Shopname.Text = string.Empty;
                    }
                    else
                    {
                        // Insert new data into the database using parameterized query
                        var affectedRows = db.Execute(
                            "INSERT INTO shopdetails (Sname, Vat, Oname, Contact, Address, Odate, Rent, Advance) " +
                            "VALUES (@Sname, @Vat, @OwnerName, @Contact, @Address, @OpeningDate, @Rent, @Advance)",
                            new
                            {
                                Sname = sname,
                                Vat = vat,
                                OwnerName = oname,
                                Contact = contact,
                                Address = address,
                                OpeningDate = date,
                                Rent = rent,
                                Advance = advance
                            });

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Store created successfully.");
                            Shopname.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("Failed to create the store.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Rent and Advance must be valid amounts.");
                Rent.Text = "";
                Advance.Text = string.Empty;
            }
        }
    }
}

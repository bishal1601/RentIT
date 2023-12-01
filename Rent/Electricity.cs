using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Dapper;
using Rent.Model;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;

namespace Rent
{
    public partial class Electricity : Form
    {
        private string connectionString;

        public Electricity()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Connection string is null or empty.");
                

                // You may want to handle this situation appropriately
            }
        }

        private void CreateElectricityBill()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS electricitybill (
                                BillNo INT AUTO_INCREMENT PRIMARY KEY,
                                Sname VARCHAR(50),
                                Billdate VARCHAR(50),
                                Year VARCHAR(5),
                                Month VARCHAR(15),
                                Consumedunit INT,
                                Amount DECIMAL
                            )";

                        command.ExecuteNonQuery();
                    }
                }

                //MessageBox.Show("Electricity bill table created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Electricity_Load(object sender, EventArgs e)
        {
            CreateElectricityBill();

            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                var data = db.Query<Shop>("SELECT * FROM shopdetails");

                comboBox1.DataSource = data;
                comboBox1.ValueMember = "SID";
                comboBox1.DisplayMember = "Sname";
            }
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /* Shop demo = comboBox1.SelectedItem as Shop;
            if (demo != null)
            {
                MessageBox.Show(string.Format("Id = {0} , Name= {1}", demo.SID, demo.Sname), nameof(Message), MessageBoxButtons.OK);
            }*/
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void Consumed_TextChanged(object sender, EventArgs e)
        {
            var consumedText = Consumed.Text;

            if (int.TryParse(consumedText, out int consumed))
            {
                int amount = consumed * 25;
                Amount.Text = amount.ToString();  // Convert the result to string before assigning to Amount.Text
            }
            /*else
            {
                // Handle the case where the input is not a valid integer
                MessageBox.Show("Invalid input. Please enter a valid integer.");
            }*/
        }

        private void save_Click(object sender, EventArgs e)
        {

        }

        private void Save_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine();
            string shopName = comboBox1.Text;
            string year = comboBox2.Text;
            string bdate = Billdate.Text;
            string month = comboBox3.Text;
            var consumedText = Consumed.Text;
            
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                db.Open();

                //var existinginvoice = db.QueryFirstOrDefault<Electricitybill>("SELECT * FROM shopdetails WHERE Esname = @dvshopname && Eyear=@dvyear && Emonth=@dvmonth ", new { dvshopname = shopName, dvyear = year, dvmonth = month });
                var existingInvoice = db.QueryFirstOrDefault<Electricitybill>("SELECT * FROM electricitybill WHERE Sname = @dvshopname AND Year = @dvyear AND Month = @dvmonth",
            new { dvshopname = shopName, dvyear = year, dvmonth = month });
                if (existingInvoice != null)
                {
                    MessageBox.Show("Invoice Already Exist");
                    //Shopname.Text = string.Empty;
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                }
                else
                {
                    if (int.TryParse(consumedText, out int consumed))
                    {
                        int amount = consumed * 25;

                        
                            var insertQuery = "INSERT INTO electricitybill (Sname, Billdate, Year, Month, Consumedunit, Amount)" +
                                              "VALUES (@sname, @billdate, @year, @month, @consumed, @amount)";

                            var affectedRows = db.Execute(insertQuery, new
                            {
                                sname = shopName,
                                billdate = bdate,
                                year = year,
                                month = month,
                                consumed = consumed,
                                amount = amount
                            });
                            db.Close();


                            var UpdateQuery = "UPDATE shopdetails SET Electricitydue = @elec WHERE Sname = @newsname";
                            var affectedRow = db.Execute(UpdateQuery, new
                            {
                                elec = amount,
                                newsname = shopName
                            });

                            /*var insertintostoretable = "INSERT INTO shopdetails (Electricitydue) " +
                                    "VALUES (@elec)";
                                    var affectedRow = db.Execute(insertintostoretable, new
                                    {
                                        elec = amount
                                    });*/





                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Record inserted successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to insert record.");
                            }
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid input. Please enter a valid integer for consumed units.");
                    }
                }
            }

            




            //end

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

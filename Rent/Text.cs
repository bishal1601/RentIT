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
using MySqlX.XDevAPI.Relational;
using Rent.Model;


namespace Rent
{
    public partial class Text : Form
        {
        private string connectionString;
        public Text()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        }



        private void CreateEmployeeTable()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS employees (
                                employee_id INT PRIMARY KEY,
                                first_name VARCHAR(50),
                                last_name VARCHAR(50),
                                birth_date DATE,
                                hire_date DATE
                            )";

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Employee table created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }




        private void Text_Load(object sender, EventArgs e)
        {
            CreateEmployeeTable();
            /*string tableName = "test";

            try
            {
                bool tableExists = TableExists(connectionString, tableName);

                if (tableExists)
                {
                    MessageBox.Show($"The '{tableName}' table exists in the database.");
                }
                else
                {
                    MessageBox.Show($"The '{tableName}' table does not exist in the database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }*/
        }

       /* private bool TableExists(string connectionString, string tableName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"SHOW TABLES LIKE '{tableName}'", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }*/
    
    }
}

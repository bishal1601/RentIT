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
    public partial class Payment : Form
    {
        private string connectionString;
        public Payment()
        {
            InitializeComponent();
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Defaultconnection"].ToString();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                db.Open();
                var Data = db.Query<Shop>("Select * from shopdetails");
                Data.ToList();
                comboBox1.DataSource = Data;
                comboBox1.ValueMember = "SID";
                comboBox1.DisplayMember = "Sname";
            }
        }
    }
}

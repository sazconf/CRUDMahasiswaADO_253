using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUDMahasiswaADO
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        string connectionString = "Data Source=SAZZAD_LAPTOP\\SQLSERVERDEV;Initial Catalog=DBAkademikADO;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbJK.Items.Add("L");
            cmbJK.Items.Add("P");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void btnConncet_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                MessageBox.Show("Connected!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

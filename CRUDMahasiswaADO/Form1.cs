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
            // TODO: This line of code loads data into the 'dBAkademikADODataSet.Mahasiswa' table. You can move, or remove it, as needed.
            this.mahasiswaTableAdapter.Fill(this.dBAkademikADODataSet.Mahasiswa);
            cmbJK.Items.Add("L");
            cmbJK.Items.Add("P");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                this.mahasiswaBindingSource.AddNew();

                txtNIM.Text = "";
                txtNama.Text = "";
                cmbJK.Text = "";
                txtAlamat.Text = "";
                txtKodeProdi.Text = "";
                dtpTanggalLahir.Value = DateTime.Now;

                MessageBox.Show("Fill data then click Save/Update.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                this.mahasiswaTableAdapter.Fill(this.dBAkademikADODataSet.Mahasiswa);
                MessageBox.Show("Data loaded!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dtpTanngalLahir_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.mahasiswaBindingSource.EndEdit();
                this.mahasiswaTableAdapter.Update(this.dBAkademikADODataSet.Mahasiswa);

                MessageBox.Show("Saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                mahasiswaBindingSource.RemoveCurrent();
                mahasiswaTableAdapter.Update(dBAkademikADODataSet.Mahasiswa);

                MessageBox.Show("Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}

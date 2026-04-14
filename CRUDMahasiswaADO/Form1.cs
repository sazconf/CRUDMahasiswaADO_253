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
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "INSERT INTO Mahasiswa VALUES (@NIM, @Nama, @JK, @TanggalLahir, @Alamat, @KodeProdi, @TanggalDaftar)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@JK", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);
                cmd.Parameters.AddWithValue("@TanggalDaftar", DateTime.Now);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Inserted!");
                btnLoad.PerformClick(); // refresh
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
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("NIM", "NIM");
                dataGridView1.Columns.Add("Nama", "Nama");
                dataGridView1.Columns.Add("JenisKelamin", "Jenis Kelamin");
                dataGridView1.Columns.Add("TanggalLahir", "Tanggal Lahir");
                dataGridView1.Columns.Add("Alamat", "Alamat");
                dataGridView1.Columns.Add("KodeProdi", "Kode Prodi");

                string query = "SELECT * FROM Mahasiswa";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["NIM"],
                        reader["Nama"],
                        reader["JenisKelamin"],
                        Convert.ToDateTime(reader["TanggalLahir"]).ToShortDateString(),
                        reader["Alamat"],
                        reader["KodeProdi"]
                    );
                }

                reader.Close();
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
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "UPDATE Mahasiswa SET Nama=@Nama, JenisKelamin=@JK, TanggalLahir=@TanggalLahir, Alamat=@Alamat, KodeProdi=@KodeProdi WHERE NIM=@NIM";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@JK", cmbJK.Text);
                cmd.Parameters.AddWithValue("@TanggalLahir", dtpTanggalLahir.Value);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);
                cmd.Parameters.AddWithValue("@KodeProdi", txtKodeProdi.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Updated!");
                btnLoad.PerformClick();
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
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "DELETE FROM Mahasiswa WHERE NIM=@NIM";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Deleted!");
                btnLoad.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtNIM.Text = row.Cells["NIM"].Value.ToString();
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                cmbJK.Text = row.Cells["JenisKelamin"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtKodeProdi.Text = row.Cells["KodeProdi"].Value.ToString();
            }
        }
    }
}

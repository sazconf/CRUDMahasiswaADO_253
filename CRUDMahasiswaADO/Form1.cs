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
        private BindingSource bindingSource = new BindingSource();
        private DataTable dtMahasiswa = new DataTable();
        public Form1()
        {

            InitializeComponent();
            conn = new SqlConnection(connectionString); 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ComboBox JK
            cmbJK.DataSource = new string[] { "L", "P" };

            // DataGridView settings (use YOUR name)
            dgvMahasiswa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMahasiswa.MultiSelect = false;
            dgvMahasiswa.ReadOnly = true;
            dgvMahasiswa.AllowUserToAddRows = false;
            dgvMahasiswa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // BindingNavigator
            bindingNavigator1.BindingSource = bindingSource;

            // Load data (we'll define next)
            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand("sp_GetMahasiswa", conn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        using (SqlDataAdapter da =
                            new SqlDataAdapter(cmd))
                        {
                            dtMahasiswa = new DataTable();

                            da.Fill(dtMahasiswa);

                            bindingSource.DataSource =
                                dtMahasiswa;

                            dgvMahasiswa.DataSource =
                                bindingSource;

                            BindControls();
                            HitungTotal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "Load Error: " + ex.Message
                );
            }
        }

        private void HitungTotal()
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand("sp_CountMahasiswa", conn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        SqlParameter outputParam =
                            new SqlParameter("@Total", SqlDbType.Int);

                        outputParam.Direction =
                            ParameterDirection.Output;

                        cmd.Parameters.Add(outputParam);

                        conn.Open();

                        cmd.ExecuteNonQuery();

                        lblTotal.Text =
                            "Total Mahasiswa: " +
                            outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "Gagal menghitung total: " +
                    ex.Message
                );
            }
        }


        private void BindControls()
        {
            txtNIM.DataBindings.Clear();
            txtNama.DataBindings.Clear();
            cmbJK.DataBindings.Clear();
            dtpTanggalLahir.DataBindings.Clear();
            txtAlamat.DataBindings.Clear();
            txtKodeProdi.DataBindings.Clear();

            txtNIM.DataBindings.Add("Text", bindingSource, "NIM");
            txtNama.DataBindings.Add("Text", bindingSource, "Nama");
            cmbJK.DataBindings.Add("Text", bindingSource, "JenisKelamin");

            // 🔥 FIXED LINE
            dtpTanggalLahir.DataBindings.Add(
                "Value",
                bindingSource,
                "TanggalLahir",
                true,
                DataSourceUpdateMode.OnPropertyChanged,
                DateTime.Now
            );

            txtAlamat.DataBindings.Add("Text", bindingSource, "Alamat");
            txtKodeProdi.DataBindings.Add("Text", bindingSource, "KodeProdi");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand("sp_InsertMahasiswa", conn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@NIM",
                            txtNIM.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@Nama",
                            txtNama.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@JenisKelamin",
                            cmbJK.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@TanggalLahir",
                            dtpTanggalLahir.Value.Date
                        );

                        cmd.Parameters.AddWithValue(
                            "@Alamat",
                            txtAlamat.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@KodeProdi",
                            txtKodeProdi.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@TanggalDaftar",
                            DateTime.Now
                        );

                        conn.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "Data inserted successfully!"
                        );
                    }
                }

                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "SQL Error: " + ex.Message
                );
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "General Error: " + ex.Message
                );
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
                SimpanLog(ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpTanngalLahir_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand("sp_UpdateMahasiswa", conn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@NIM",
                            txtNIM.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@Nama",
                            txtNama.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@JenisKelamin",
                            cmbJK.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@TanggalLahir",
                            dtpTanggalLahir.Value.Date
                        );

                        cmd.Parameters.AddWithValue(
                            "@Alamat",
                            txtAlamat.Text
                        );

                        cmd.Parameters.AddWithValue(
                            "@KodeProdi",
                            txtKodeProdi.Text
                        );

                        conn.Open();

                        int result =
                            cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            result + " row(s) updated"
                        );
                    }
                }

                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "SQL Error: " + ex.Message
                );
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "General Error: " + ex.Message
                );
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNIM.Text))
                {
                    MessageBox.Show("NIM is required!");
                    return;
                }

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this data?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo
                );

                if (confirm == DialogResult.No)
                    return;

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                        new SqlCommand("sp_DeleteMahasiswa", conn))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.Add(
                            "@NIM",
                            SqlDbType.Char,
                            11
                        ).Value = txtNIM.Text;

                        conn.Open();

                        int rowsAffected =
                            cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show(
                                "Data berhasil dihapus"
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                "Data tidak ditemukan"
                            );
                        }
                    }
                }

                LoadData();
            }
            catch (SqlException ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "SQL Error: " + ex.Message
                );
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show(
                    "General Error: " + ex.Message
                );
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = @"
        IF OBJECT_ID('dbo.Mahasiswa_Backup') IS NOT NULL
            DROP TABLE dbo.Mahasiswa_Backup;

        SELECT * INTO dbo.Mahasiswa_Backup
        FROM dbo.Mahasiswa;
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Backup updated (old backup replaced)");
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("Backup gagal: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = @"
        IF OBJECT_ID('dbo.Mahasiswa_Backup') IS NOT NULL
        BEGIN
            DELETE FROM dbo.Mahasiswa;

            INSERT INTO dbo.Mahasiswa
            SELECT * FROM dbo.Mahasiswa_Backup;
        END
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data berhasil direset!");

                LoadData();
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("Reset gagal: " + ex.Message);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        private void btnTestInjection_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query =
                        "UPDATE Mahasiswa SET Nama = '" + txtNama.Text +
                        "' WHERE NIM = '" + txtNIM.Text + "'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Update berhasil dijalankan!");
            }
            catch (Exception ex)
            {
                SimpanLog(ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SimpanLog(string pesan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO LogError (waktu, pesan_error) VALUES (GETDATE(), @pesan)",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@pesan", pesan);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

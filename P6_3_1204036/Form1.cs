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

namespace P6_3_1204036
{
    public partial class Form1 : Form
    {
        string prodi;
        public Form1()
        {
            InitializeComponent();
            rbLaki.Checked = false;
            rbPerempuan.Checked = false;

            //string myConnectionString = "integrated security=true;data source=.;initial catalog=DESKTOP-NAWAF\\P6_1204036";
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NAWAF\P6_1204036;Initial Catalog=P6_1204036;Integrated Security=True");

            conn.Open();

            SqlCommand sc = new SqlCommand("SELECT * FROM msprodi", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_prodi", typeof(string));
            dt.Columns.Add("singkatan", typeof(string));
            dt.Load(reader);

            cbProdi.ValueMember = "id_prodi";
            cbProdi.DisplayMember = "singkatan";
            cbProdi.DataSource = dt;

            conn.Close();
        }

        private void npmTB_TextChanged(object sender, EventArgs e)
        {
            if (npmTB.TextLength < 7)
            {
                epWrong.SetError(npmTB, "Format NPM salah!");
            }
            else if (npmTB.TextLength == 7)
            {
                epWrong.SetError(npmTB, "");
            }
            else if (npmTB.TextLength == 0)
            {
                epWarning.SetError(npmTB, "Tidak Boleh Kosong!");
            }
        }

        private void namaTB_TextChanged(object sender, EventArgs e)
        {
            if (namaTB.TextLength == 0)
            {
                epWarning.SetError(namaTB, "Tidak Boleh Kosong!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (npmTB.Text != "" && npmTB.TextLength == 7)
            {
                if (namaTB.Text != "")
                {
                    if (dtTanggal.Text != "")
                    {
                        if (rbLaki.Checked || rbPerempuan.Checked)
                        {
                            if (tbAlamat.Text != "")
                            {
                                if (nohpTB.Text != "")
                                {
                                    if (cbProdi.Text != "- Pilih Program Studi -")
                                    {
                                        string npm = npmTB.Text;
                                        string nama = namaTB.Text;
                                        string tanggal = dtTanggal.Text;
                                        string kelamin = "";
                                        if (rbLaki.Checked)
                                        {
                                            kelamin = rbLaki.Text;
                                        }
                                        if (rbPerempuan.Checked)
                                        {
                                            kelamin = rbPerempuan.Text;
                                        }
                                        string alamat = tbAlamat.Text;
                                        string nohp = nohpTB.Text;
                                        string prodi = this.prodi;

                                        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NAWAF\P6_1204036;Initial Catalog=P6_1204036;Integrated Security=True");

                                        string sql = "insert into msmhs ([nim], [nama], [tgl_lahir], [jenis_kelamin], [alamat], " +
                                            "[telepon], [id_prodi]) values(@nim,@nama,@tanggal,@kelamin,@alamat,@nohp,@idprodi)";

                                        using (SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-NAWAF\P6_1204036;Initial Catalog=P6_1204036;Integrated Security=True"))
                                        {
                                            try
                                            {
                                                cnn.Open();

                                                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                                                {
                                                    cmd.Parameters.Add("@nim", SqlDbType.NVarChar).Value = npm;
                                                    cmd.Parameters.Add("@nama", SqlDbType.NVarChar).Value = nama;
                                                    cmd.Parameters.Add("@tanggal", SqlDbType.NVarChar).Value = tanggal;
                                                    cmd.Parameters.Add("@kelamin", SqlDbType.NVarChar).Value = kelamin;
                                                    cmd.Parameters.Add("@alamat", SqlDbType.NVarChar).Value = alamat;
                                                    cmd.Parameters.Add("@nohp", SqlDbType.NVarChar).Value = nohp;
                                                    cmd.Parameters.Add("@idprodi", SqlDbType.NVarChar).Value = prodi;

                                                    int rowsAdded = cmd.ExecuteNonQuery();
                                                    if (rowsAdded > 0)
                                                        MessageBox.Show("Data berhasil disimpan");
                                                    else
                                                        MessageBox.Show("Tidak ada data yang disimpan");



                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("ERROR:" + ex.Message);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show
                                                    ("Prodi belum diisi!",
                                                    "Informasi Data Submit",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show
                                                ("No Telp belum diisi!",
                                                "Informasi Data Submit",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show
                                            ("Alamat belum diisi!",
                                            "Informasi Data Submit",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show
                                        ("Jenis Kelamin belum diisi!",
                                        "Informasi Data Submit",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show
                                    ("Tanggal Lahir belum diisi!",
                                    "Informasi Data Submit",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show
                                ("Nama belum diisi!",
                                "Informasi Data Submit",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show
                            ("NPM belum diisi!",
                            "Informasi Data Submit",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.prodi = cbProdi.SelectedValue.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            npmTB.Text = null;
            namaTB.Text = null;
            tbAlamat.Text = null;
            nohpTB.Text = null;
            rbLaki.Checked = false;
            rbPerempuan.Checked = false;
            cbProdi.SelectedIndex = 0;
        }
    }
}

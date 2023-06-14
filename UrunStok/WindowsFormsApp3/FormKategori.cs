using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;

namespace UrunStok
{
    public partial class FormKategori : Form
    {
        VeriTabaniIslemleri veriTabani = new VeriTabaniIslemleri();
        DataTable dtSiniflar = new DataTable();
        MySqlCommand kmt = new MySqlCommand();
        int siraNo = 0;
        public FormKategori()
        {
            InitializeComponent();
        }
        private void DataGridiDoldur()
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            dataGridView1.RowHeadersVisible = true;
            dataGridView1.Font = new Font("Arial", 10);
            dtSiniflar.Columns.Clear();
            dtSiniflar.Clear();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from kategoriler order by kategori", veriTabani.Baglan());
            adtr.Fill(dtSiniflar);
            dataGridView1.DataSource = dtSiniflar;

            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["id"].Width = 60;
            dataGridView1.Columns["kategori"].HeaderText = "KATEGORİ";
            dataGridView1.Columns["kategori"].Width = 120;

            dataGridView1.Rows[siraNo].Selected = true;

            VerileriDoldur();

            veriTabani.BaglantiyiKapat();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

        }
        private void VerileriDoldur()
        {
            if (siraNo >= 0 && siraNo < dtSiniflar.Rows.Count)
            {
                textBox1.Text = dtSiniflar.Rows[siraNo]["id"].ToString();
                textBox2.Text = dtSiniflar.Rows[siraNo]["kategori"].ToString();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                siraNo = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows[siraNo].Selected = true;
                if (siraNo >= 0)
                {
                    VerileriDoldur();
                }
            }
        }

        private void FormKategori_Load(object sender, EventArgs e)
        {
            DataGridiDoldur();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            try
            {
                string sorgu = "INSERT INTO `kategoriler` ( `kategori` ) VALUES ( @kategori ); ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@kategori", textBox2.Text.Trim().ToString());

                kmt.ExecuteNonQuery();
                siraNo = dtSiniflar.Rows.Count;
                DataGridiDoldur();

                MessageBox.Show("Kayıt Başarılı", "Kayıt İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("HATA : " + hata.Message);
            }
            finally
            {
                kmt.Connection.Close();
                veriTabani.BaglantiyiKapat();
            }
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            try
            {
                string sorgu = "UPDATE `kategoriler` SET `kategori` = @kategori WHERE `id` = @id; ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@kategori", textBox2.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@id", textBox1.Text.Trim().ToString());

                kmt.ExecuteNonQuery();
                siraNo = dataGridView1.CurrentRow.Index;
                DataGridiDoldur();

                MessageBox.Show("Güncelleme Başarılı", "Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("HATA : " + hata.Message);
            }
            finally
            {
                kmt.Connection.Close();
                veriTabani.BaglantiyiKapat();
            }
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            try
            {
                string sorgu = "DELETE FROM `kategoriler` WHERE `id` = @id; ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@id", textBox1.Text.Trim().ToString());

                kmt.ExecuteNonQuery();
                siraNo = 0;
                DataGridiDoldur();

                MessageBox.Show("Silme Başarılı", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("HATA : " + hata.Message);
            }
            finally
            {
                kmt.Connection.Close();
                veriTabani.BaglantiyiKapat();
            }
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }
    }
}

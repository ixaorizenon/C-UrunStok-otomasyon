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

namespace UrunStok
{
    public partial class Form3 : Form
    {
        VeriTabaniIslemleri veriTabani = new VeriTabaniIslemleri();
        DataTable dtmusteriler = new DataTable();
        DataTable dturunler = new DataTable();
        MySqlCommand kmt = new MySqlCommand();
        int SiraNo = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataGridiDoldur();
            VerileriDoldur();
            MySqlDataAdapter adtr = new MySqlDataAdapter("SELECT * FROM `urunler`", veriTabani.Baglan());
            adtr.Fill(dturunler);
            for (int i = 0; i < dturunler.Rows.Count; i++)
            {
                txtUrun.Items.Add(dturunler.Rows[i]["urunadi"].ToString());
            }
        }
        private void DataGridiDoldur()
        {
            dtmusteriler.Clear();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from musteriler", veriTabani.Baglan());
            adapter.Fill(dtmusteriler);
            dataGridView1.DataSource = dtmusteriler;
            adapter.Dispose();


            veriTabani.BaglantiyiKapat();

            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["ad"].HeaderText = "İsim";
            dataGridView1.Columns["telefonno"].HeaderText = "Telefon Numarası";
            dataGridView1.Columns["gidecekurun"].HeaderText = "Gidicek Ürün";
            dataGridView1.Columns["adres"].HeaderText = "Adres";

        }
        private void VerileriDoldur()
        {
            if (SiraNo >= 0 && SiraNo < dtmusteriler.Rows.Count)
            {
                txtId.Text = dtmusteriler.Rows[SiraNo]["id"].ToString();
                txtAd.Text = dtmusteriler.Rows[SiraNo]["ad"].ToString();
                txtSoyad.Text = dtmusteriler.Rows[SiraNo]["soyad"].ToString();
                txtNo.Text = dtmusteriler.Rows[SiraNo]["telefonno"].ToString();
                txtUrun.Text = dtmusteriler.Rows[SiraNo]["gidecekurun"].ToString();
                txtAdres.Text = dtmusteriler.Rows[SiraNo]["adres"].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "INSERT INTO `musteriler`(`ad`, `soyad`, `telefonno`, `gidecekurun`, `adres`) VALUES (@ad,@soyad,@telefonno,@gidecekurun,@adres);";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@ad", txtAd.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@soyad", txtSoyad.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@telefonno", txtNo.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@gidecekurun", txtUrun.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@adres", txtAdres.Text.Trim().ToString());


                kmt.ExecuteNonQuery();
                SiraNo = dtmusteriler.Rows.Count;
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

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged_1;

            try
            {
                string sorgu = "UPDATE `musteriler` SET `ad` = @ad,`soyad` = @soyad, `telefonno` = @telefonno, `gidecekurun` = @gidecekurun, `adres` = @adres WHERE `musteriler`.`id` = @id; ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());
                kmt.Parameters.AddWithValue("@id", txtId.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@ad", txtAd.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@soyad", txtSoyad.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@telefonno", txtNo.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@gidecekurun", txtUrun.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@adres", txtAdres.Text.Trim().ToString());


                kmt.ExecuteNonQuery();
                SiraNo = dataGridView1.CurrentRow.Index;

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
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged_1;
            try
            {
                string sorgu = "DELETE FROM `musteriler` WHERE `musteriler`.`id` = @id; ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@id", txtId.Text.Trim().ToString());
                DialogResult cevap = MessageBox.Show("Kayıt Silinecek !", "Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.OK)
                {
                    kmt.ExecuteNonQuery();
                    SiraNo = 0;
                    DataGridiDoldur();
                }
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
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged_1;
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                SiraNo = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows[SiraNo].Selected = true;
                if (SiraNo >= 0)
                {
                    VerileriDoldur();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtNo.Text = "";
            txtUrun.Text = "";
            txtAdres.Text = "";
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

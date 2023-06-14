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
    public partial class Form1 : Form
    {
        VeriTabaniIslemleri veriTabani = new VeriTabaniIslemleri();
        DataTable dturunler = new DataTable();
        DataTable dtkategoriler = new DataTable();
        MySqlCommand kmt = new MySqlCommand();
        int SiraNo = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataGridiDoldur();
            VerileriDoldur();
            dataGridView1.Columns.Remove("resim");
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            VerileriDoldur();
        }

        private void DataGridiDoldur()
        {
            dturunler.Clear();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from urunler", veriTabani.Baglan());
            adapter.Fill(dturunler);
            dataGridView1.DataSource = dturunler;
            adapter.Dispose();
            veriTabani.BaglantiyiKapat();

            dtkategoriler.Clear();
            cbKategori.Items.Clear();
            MySqlDataAdapter adtr = new MySqlDataAdapter("SELECT * FROM `kategoriler`", veriTabani.Baglan());
            adtr.Fill(dtkategoriler);
            for (int i = 0; i < dtkategoriler.Rows.Count; i++)
            {
                cbKategori.Items.Add(dtkategoriler.Rows[i]["kategori"].ToString());
            }

            adtr.Dispose();

            veriTabani.BaglantiyiKapat();

            dataGridView1.Columns["id"].HeaderText = "ID";
            dataGridView1.Columns["urunadi"].HeaderText = "ÜrünAdi";
            dataGridView1.Columns["urunkategorisi"].HeaderText = "ÜrünKatergorisi";
            dataGridView1.Columns["urunmodeli"].HeaderText = "ÜrünModeli";
            dataGridView1.Columns["urunserino"].HeaderText = "ÜrünSeriNo";
            dataGridView1.Columns["urunadedi"].HeaderText = "ÜrünAdedi";
            dataGridView1.Columns["urunfiyati"].HeaderText = "ÜrünFiyatı";
            dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
            dataGridView1.Columns["uruntarihi"].HeaderText = "Tarihi";
        }

        private void VerileriDoldur()
        {
            if (SiraNo >= 0 && SiraNo < dturunler.Rows.Count)
            {
                txtId.Text = dturunler.Rows[SiraNo]["id"].ToString();
                txtAd.Text = dturunler.Rows[SiraNo]["urunadi"].ToString();
                cbKategori.Text = dturunler.Rows[SiraNo]["urunkategorisi"].ToString();
                txtModel.Text = dturunler.Rows[SiraNo]["urunmodeli"].ToString();
                txtSeriNo.Text = dturunler.Rows[SiraNo]["urunserino"].ToString();
                txtAded.Text = dturunler.Rows[SiraNo]["urunadedi"].ToString();
                txtFiyat.Text = dturunler.Rows[SiraNo]["urunfiyati"].ToString();
                rAciklama.Text = dturunler.Rows[SiraNo]["aciklama"].ToString();
                dtTarih.Text = dturunler.Rows[SiraNo]["uruntarihi"].ToString();
                string resim = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resimler\\";
                if (dturunler.Rows[SiraNo]["resim"].ToString() == "")
                {
                    resim += "ii.jpg";
                    pictureBox1.Image = Image.FromFile(resim);
                }
                else
                {
                    resim += dturunler.Rows[SiraNo]["resim"].ToString();
                    pictureBox1.Image = Image.FromFile(resim);
                }


               
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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
        private void btnSil_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            try
            {
                string sorgu = "DELETE FROM `urunler` WHERE `urunler`.`id` = @id; ";

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
           
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            try
            {
                string sorgu = "UPDATE `urunler` SET `urunadi` = @urunadi,`urunkategorisi` = @urunkategorisi, `urunmodeli` = @urunmodeli, `urunserino` = @urunserino, `urunadedi` = @urunadedi,`urunfiyati` = @urunfiyati,`uruntarihi` = @uruntarihi, `aciklama` = @aciklama WHERE `urunler`.`id` = @id; ";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());
                kmt.Parameters.AddWithValue("@id", txtId.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunadi", txtAd.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunkategorisi", cbKategori.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunmodeli", txtModel.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunserino", txtSeriNo.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunadedi", txtAded.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunfiyati", txtFiyat.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@aciklama", rAciklama.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@uruntarihi", dtTarih.Value.ToString("yy-MM-dd"));

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
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "INSERT INTO `urunler`(`urunadi`, `urunkategorisi`, `urunmodeli`, `urunserino`, `urunadedi`,`urunfiyati`,`uruntarihi`, `aciklama`) VALUES (@urunadi,@urunkategorisi,@urunmodeli,@urunserino,@urunadedi,@urunfiyati,@uruntarihi,@aciklama);";

                kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                kmt.Parameters.AddWithValue("@urunadi", txtAd.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunkategorisi", cbKategori.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunmodeli", txtModel.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunserino", txtSeriNo.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunadedi", txtAded.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@urunfiyati", txtFiyat.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@aciklama", rAciklama.Text.Trim().ToString());
                kmt.Parameters.AddWithValue("@uruntarihi", dtTarih.Value.ToString("yy-MM-dd"));


                kmt.ExecuteNonQuery();
                SiraNo = dturunler.Rows.Count;
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

        private void btnResimGuncelle_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            OpenFileDialog op = new OpenFileDialog();
            string yol = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resimler\\";
            op.Title = "Resim Seç"; 
            op.Filter = "Dosya Türleri|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            DialogResult? kontrol = op.ShowDialog();
            if (kontrol != null && kontrol == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(op.FileName);
                if (!Directory.Exists(yol))
                {
                    Directory.CreateDirectory(yol);
                }

                int rSayi = new Random().Next(10000);
                string resimYolu = yol + rSayi + Path.GetFileName(op.FileName);
                File.Copy(op.FileName, resimYolu, true);

                string[] diziResim = resimYolu.Split('\\');

                try
                {
                    string sorgu = "UPDATE `urunler` SET `resim` = @resim WHERE `urunler`.`id` = @id; ";

                    kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                    kmt.Parameters.AddWithValue("@id", txtId.Text.Trim().ToString());
                    kmt.Parameters.AddWithValue("@resim", diziResim[diziResim.Length - 1].ToString());

                    kmt.ExecuteNonQuery();

                    DataGridiDoldur();
                   
                    MessageBox.Show("Resim Güncelleme Başarılı", "Resim Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
            else
            {
                MessageBox.Show("Resim seçilmedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

        }

        private void btnResimSil_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            if (dturunler.Rows[SiraNo]["resim"].ToString().Trim() != "")
            {
                try
                {
                    string sorgu = "UPDATE `urunler` SET `resim` = @resim WHERE `urunler`.`id` = @id; ";

                    kmt = new MySqlCommand(sorgu, veriTabani.Baglan());

                    kmt.Parameters.AddWithValue("@id", txtId.Text.Trim().ToString());
                    kmt.Parameters.AddWithValue("@resim", "");

                    kmt.ExecuteNonQuery();

                    string resim = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resimler\\" + dturunler.Rows[SiraNo]["resim"].ToString();
                    if (File.Exists(resim))
                    {
                        string resimNoImage = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Resimler\\ii.jpg";
                        pictureBox1.Image = Image.FromFile(resimNoImage);
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(resim);
                    }

                    DataGridiDoldur();
                 
                    MessageBox.Show("Resim Silme Başarılı", "Resim Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  

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
            }
            else
            {
             
                MessageBox.Show("Henüz bir resim tanımlanmadı !", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSeriNo.Text = "";
            txtAded.Text = "";
            txtFiyat.Text = "";
            txtModel.Text = "";
            dtTarih.Text = "";
            cbKategori.Text = "";
            rAciklama.Text = "";
        }

        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            FormKategori form4 = new FormKategori();
            form4.ShowDialog();
            DataGridiDoldur();
            VerileriDoldur();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

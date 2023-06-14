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

namespace UrunStok
{
    class VeriTabaniIslemleri
    {
        MySqlConnection baglanti;

        public MySqlConnection Baglan()
        {
            baglanti = new MySqlConnection("Server=localhost; Database=urunstok; Uid=root; Pwd=");
            MySqlConnection.ClearPool(baglanti);
            baglanti.Open();
            return baglanti;
        }

        public void BaglantiyiKapat()
        {
            baglanti.Close();
        }
    }
}

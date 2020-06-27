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
using System.Collections;

namespace hammaddeUretim
{
    public partial class hammaddeAlis : Form
    {
        public hammaddeAlis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        string[] geciciHammaddeAdi=new string[20];
        string geciciHammaddeMiktari;
        string geciciID;
        string geciciFiyat;
        private void button1_Click(object sender, EventArgs e)
        {
             
              int i = 0,k=0;
        string tempHammaddeMiktari = "";

            baglanti.Open();

            komut.Connection = baglanti;
            komut.CommandText = "select hammaddeMiktari from hammadde where hammaddeID = '" + uretici.id + "' ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                tempHammaddeMiktari = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select stokHammaddeAdi from ureticiStok";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                geciciHammaddeAdi[i] = oku[0].ToString();
                i++;
            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();

            komut.Connection = baglanti;
            komut.CommandText = "select stokHammaddeMiktari from ureticiStok where stokHammaddeAdi= '" + uretici.geciciHammaddeAdi + "'";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                geciciHammaddeMiktari = oku[0].ToString();

            }
            oku.Close();
            baglanti.Close();


            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select stokID from ureticiStok where stokHammaddeAdi= '" + uretici.geciciHammaddeAdi + "'";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                geciciID = oku[0].ToString();

            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select topAlisFiyati from ureticiStok where stokHammaddeAdi= '" + uretici.geciciHammaddeAdi + "'";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                geciciFiyat = oku[0].ToString();

            }
            oku.Close();
            baglanti.Close();


            baglanti.Open();

            if (Convert.ToInt32(textBox1.Text) <= int.Parse(uretici.stokHammaddeMiktari))
            {
                try
                {
                    DialogResult guncelle;
                    guncelle = MessageBox.Show("Satın Almak İstediğinizden Eminmisiniz", "Uyarı ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (guncelle == DialogResult.Yes && uretici.id != "")
                    {
                        string satirGuncelle = uretici.id;

                        string sorgu = "Update hammadde set hammaddeMiktari=@1 where hammaddeID= " + satirGuncelle + " ";

                        komut = new SqlCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@1", (int.Parse(tempHammaddeMiktari) - (Convert.ToInt32(textBox1.Text))).ToString());
                        komut.ExecuteNonQuery();
                        dgvDoldur();


                        string firmaSorgu = "INSERT INTO sepet(firmaID,hammaddeID,alisMaliyeti,stokDurumu) values (@1,@2,@3,@4)";
                        komut = new SqlCommand(firmaSorgu, baglanti);
                        komut.Parameters.AddWithValue("@1", uretici.tempFirmaid);
                        komut.Parameters.AddWithValue("@2", uretici.id);
                        komut.Parameters.AddWithValue("@3", (((Convert.ToInt32(textBox1.Text) * Convert.ToInt32(uretici.temphammaddeSatis)) + Convert.ToInt32(uretici.kargoFiyati)).ToString()));
                        komut.Parameters.AddWithValue("@4", textBox1.Text);
                        komut.ExecuteNonQuery();

                        MessageBox.Show("Hammadde Başarıyla Alındı.");

                      
                        for (int j = 0; j < i + 1; j++)
                        {
                            if (geciciHammaddeAdi[j] == uretici.geciciHammaddeAdi)
                            {
                                k++;

                                string satirGuncelle2 = uretici.geciciHammaddeAdi;

                                string sorgu2 = "Update ureticiStok set stokHammaddeMiktari=@1,topAlisFiyati=@2,ortalamaAlisFiyati=@3 where stokID= " + geciciID + " ";
                              
                                komut = new SqlCommand(sorgu2, baglanti);
                                komut.Parameters.AddWithValue("@1", (int.Parse(geciciHammaddeMiktari) + (Convert.ToInt32(textBox1.Text))).ToString());
                                komut.Parameters.AddWithValue("@2", (int.Parse(geciciFiyat) + ((Convert.ToInt32(textBox1.Text) * Convert.ToInt32(uretici.temphammaddeSatis)) + Convert.ToInt32(uretici.kargoFiyati))).ToString());
                                komut.Parameters.AddWithValue("@3", ((((Convert.ToInt32(textBox1.Text) * Convert.ToInt32(uretici.temphammaddeSatis)) + Convert.ToInt32(uretici.kargoFiyati))) /  (Convert.ToInt32(textBox1.Text))).ToString());
                                komut.ExecuteNonQuery();
                                break;
                               
                            }
                        }
                        for (int j = 0; j < i + 1; j++)
                        {
                            if (geciciHammaddeAdi[j] != uretici.geciciHammaddeAdi && k==0)
                            {

                                string stokSorgu = "INSERT INTO ureticiStok(stokHammaddeAdi,stokHammaddeMiktari,topAlisFiyati,ortalamaAlisFiyati) values (@1,@2,@3,@4)";
                                komut = new SqlCommand(stokSorgu, baglanti);
                                komut.Parameters.AddWithValue("@1", uretici.geciciHammaddeAdi);
                                komut.Parameters.AddWithValue("@2", textBox1.Text);
                                komut.Parameters.AddWithValue("@3", (((Convert.ToInt32(textBox1.Text) * Convert.ToInt32(uretici.temphammaddeSatis)) + Convert.ToInt32(uretici.kargoFiyati)).ToString()));
                                komut.Parameters.AddWithValue("@4", ((((Convert.ToInt32(textBox1.Text) * Convert.ToInt32(uretici.temphammaddeSatis)) + Convert.ToInt32(uretici.kargoFiyati)) / Convert.ToInt32(textBox1.Text)).ToString()));
                                komut.ExecuteNonQuery();
                                break;

                            }
                        }
                           
                           
                        



                    }
                    else
                    {
                        MessageBox.Show("İşlemi İptal Edildi ");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata!" + ex.Message);
                }
            }
            else
                MessageBox.Show("Stokta Yeterli kadar Ürün Yok!!");
          

            baglanti.Close();
            this.Close();


        }


        public void dgvDoldur()
        {

            string sorgu = "select firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID order by satisFiyati";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            uretici f2 = (uretici)Application.OpenForms["uretici"];
            f2.dataGridView1.DataSource = ds;
        }

        private void hammaddeAlis_FormClosed(object sender, FormClosedEventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void hammaddeAlis_Load(object sender, EventArgs e)
        {
          
        }


    }
}

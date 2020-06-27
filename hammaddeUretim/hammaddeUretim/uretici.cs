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
    public partial class uretici : Form
    {
        public uretici()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        SqlCommand komut = new SqlCommand();
        SqlCommand komut2 = new SqlCommand();
        SqlDataReader oku;

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;
            if (comboBox1.Text == "Hepsi" && comboBox2.Text == "Hepsi")
            {
                da = new SqlDataAdapter("select firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID where hammaddeMiktari > 0 order by satisFiyati ", baglanti);
            }
            else if (comboBox1.Text == "Hepsi" && comboBox2.Text != "Hepsi")
            {
                da = new SqlDataAdapter("select firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID where  hammaddeAdi in('" + comboBox2.Text + "') and hammaddeMiktari > 0 order by satisFiyati  ", baglanti);
            }
            else if (comboBox1.Text != "Hepsi" && comboBox2.Text == "Hepsi")
            {
                da = new SqlDataAdapter("select firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID where  firmaAdi in('" + comboBox1.Text + "') and hammaddeMiktari > 0 order by satisFiyati  ", baglanti);
            }
            else
            {
                da = new SqlDataAdapter("select firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID where firmaAdi in('" + comboBox1.Text + "') and hammaddeAdi in('" + comboBox2.Text + "') and hammaddeMiktari > 0 order by satisFiyati  ", baglanti);
            }




            DataTable dt = new DataTable();
            baglanti.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void uretici_Load(object sender, EventArgs e)
        {
            doldur();
            this.Height = 374;

            komut.Connection = baglanti;
            komut2.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = "select distinct firmaAdi from firma";
            komut2.CommandText = "select distinct hammaddeAdi from hammadde";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());
            }
            oku.Close();
            oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku[0].ToString());
            }
            baglanti.Close();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        hammaddeAlis hammaddeAl = new hammaddeAlis();
        public static string id;
        public static string tempFirmaid;
        public static string temphammaddeSatis;
        public static string tempUlke;
        public static string kargoFiyati;
        public static string stokHammaddeMiktari;
        public static string geciciHammaddeAdi;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            hammaddeAl = new hammaddeAlis();
            hammaddeAl.Show();
            string tempFirmaAdi = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            string temphammaddeAdi = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
            geciciHammaddeAdi = temphammaddeAdi;
   
            baglanti.Open();
            komut.Connection = baglanti;        
            komut.CommandText = "select hammaddeID from hammadde inner join firma on hammadde.firmaID=firma.firmaID  where firmaAdi in ('" + tempFirmaAdi + "') and hammaddeAdi in ('" + temphammaddeAdi + "') ";   

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                id = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();

            baglanti.Open();
            komut2.Connection = baglanti;
            komut2.CommandText = "select firma.firmaID from firma inner join hammadde on hammadde.firmaID=firma.firmaID  where firmaAdi in ('" + tempFirmaAdi + "') ";
            oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                tempFirmaid = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();



            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select hammadde.satisFiyati from hammadde inner join firma on hammadde.firmaID=firma.firmaID  where firmaAdi in ('" + tempFirmaAdi + "') and hammaddeAdi in ('" + temphammaddeAdi + "') ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                temphammaddeSatis = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();


            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select firma.ulke from firma inner join hammadde on hammadde.firmaID=firma.firmaID  where firmaAdi in ('" + tempFirmaAdi + "') and hammaddeAdi in ('" + temphammaddeAdi + "') ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                tempUlke = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();


            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select kargo.kargoFiyat from kargo inner join firma on kargo.kargoID=firma.kargoID  where ulke in ('" + tempUlke + "') ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                kargoFiyati = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();


            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select hammadde.hammaddeMiktari from hammadde inner join firma on hammadde.firmaID=firma.firmaID  where firmaAdi in ('" + tempFirmaAdi + "') ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                stokHammaddeMiktari = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();
          

        }
        sepet sayfaSepet;
        private void button2_Click(object sender, EventArgs e)
        {
            sayfaSepet = new sepet();
            sayfaSepet.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Height = 731;
            button7.Visible = true;
            button6.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Height = 374;
            button7.Visible = false;
            button6.Visible = true;
        }

        int textDoldur = 0;
        int sayac = 0;
        private void button3_Click(object sender, EventArgs e)
        {

            ArrayList ortFiyat = new ArrayList();

            ArrayList bilesenKont = new ArrayList();
            ArrayList harf = new ArrayList();
            ArrayList sayi = new ArrayList();
            ArrayList veriTabani = new ArrayList();

            ArrayList deger = new ArrayList();
            ArrayList karakter = new ArrayList();
            ArrayList gec = new ArrayList();
            ArrayList hamisimleri = new ArrayList();
            ArrayList deger2 = new ArrayList();
            ArrayList miktar = new ArrayList();

            string bilesen;
            bilesen = textBox2.Text;

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select stokHammaddeAdi from ureticiStok ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                bilesenKont.Add(oku[0].ToString());

            }
            oku.Close();
            baglanti.Close();

            for (int i = 0; i < bilesenKont.ToArray().Length; i++)
            {
                string tmpdeger = (string)bilesenKont[i];
                veriTabani.Add(tmpdeger[0]);
            }


           
            int a = 0, b = 0;
            try
            {
                if (!Char.IsNumber(bilesen[0]))
                {
                    for (int i = 0; i < bilesen.Length; i++)
                    {
                        if (!Char.IsNumber(bilesen[i]))
                        {
                            a++;
                            harf.Add(bilesen[i]);
                            if (a == 2 && b == 0) { sayi.Add("1"); b = 0; }

                        }
                        else
                        {
                            b++;
                            int sy = i; string tmp = "";
                            while (Char.IsNumber(bilesen[sy]))
                            {
                                tmp += bilesen[sy]; sy++;
                                if (sy >= bilesen.Length) break;
                            }
                            i = sy - 1;
                            sayi.Add(tmp);
                        }
                    }
                    if (harf.ToArray().Length != sayi.ToArray().Length)
                        sayi.Add("1");

                }
                else
                {
                    MessageBox.Show("Bileşen ismini yanlış girdiniz! Kontrol ediniz.");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata: Bileşenler Girilmemiş !!", ex.Message);
            }

         

            for (int i = 0; i < harf.ToArray().Length; i++)
            {
                if (veriTabani.IndexOf(harf[i]) != -1)
                {

                    karakter.Add(harf[i]);
                    gec.Add(i);
                    deger.Add(sayi[i]);

                }
            }


            try
            {
                for (int i = 0; i < bilesenKont.ToArray().Length; i++)
                {
                    string s = (string)bilesenKont[i];
                    for (int j = 0; j < karakter.ToArray().Length; j++)
                    {

                        if (s[0].ToString() == karakter[j].ToString())
                        {
                            hamisimleri.Add(bilesenKont[i]);
                            deger2.Add(deger[Convert.ToInt32(gec[j])]);

                        }

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata:", ex.Message);
            }

            baglanti.Open();
            for (int i = 0; i < hamisimleri.ToArray().Length; i++)
            {

                komut.Connection = baglanti;
                komut.CommandText = "select stokHammaddeMiktari,ortalamaAlisFiyati from ureticiStok where stokHammaddeAdi= '" + hamisimleri[i].ToString() + "' ";

                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    miktar.Add(oku[0].ToString());
                    ortFiyat.Add(oku[1].ToString());

                }
                oku.Close();

            }
            baglanti.Close();


            baglanti.Open();
            
                for (int i = 0; i < hamisimleri.ToArray().Length; i++)
                { 
               if(harf.ToArray().Length == karakter.ToArray().Length)
               {
                    string sorgu = "Update ureticiStok set stokHammaddeMiktari=@1 where stokHammaddeAdi= '" + hamisimleri[i].ToString() + "'  ";

                    if (Convert.ToInt32(miktar[i]) >= Convert.ToInt32(deger2[i]))
                    {
                        komut = new SqlCommand(sorgu, baglanti);
                        komut.Parameters.AddWithValue("@1", Convert.ToInt32(miktar[i]) - Convert.ToInt32(deger2[i]));
                        komut.ExecuteNonQuery();
                        sayac++;
                    }
                    else { MessageBox.Show("Yeterli hammaddeniz yok Lütfen Satın Alın!! "); break; }
                }
             
                   

                }
                       
            baglanti.Close();

            baglanti.Open();
            if(sayac != 0)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
                {
                    if (harf.ToArray().Length == karakter.ToArray().Length)
                    {

                        try
                        {

                            string sorgu = "INSERT INTO uretilenUrunler(urunAdi,bilesenleri,uretimTarihi,rafOmru,iscilikMaliyeti,topMaliyet,satisFiyati) values (@1,@2,@3,@4,@5,@6,@7)";
                            komut = new SqlCommand(sorgu, baglanti);
                            komut.Parameters.AddWithValue("@1", textBox1.Text);
                            komut.Parameters.AddWithValue("@2", textBox2.Text);
                            komut.Parameters.AddWithValue("@3", textBox3.Text);
                            komut.Parameters.AddWithValue("@4", textBox4.Text);
                            komut.Parameters.AddWithValue("@5", textBox5.Text);
                            komut.Parameters.AddWithValue("@6", textBox6.Text);
                            komut.Parameters.AddWithValue("@7", textBox7.Text);
                            komut.ExecuteNonQuery();
                            doldur();
                            MessageBox.Show("Kimyasal ürün Başarıyla Gerçekleştirildi.");
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Hata !" + ex.Message);
                        }
                    }
                    else MessageBox.Show("Böyle bi hammaddeniz yok!! üretim gerçekleştirilemiyor.");

                }
                else MessageBox.Show("Boş geçilmemesi gereken alanlar var!! Kontrol ediniz.");
            }
           

            baglanti.Close();

        }

        void doldur()
        {

            string sorgu = "select * from uretilenUrunler";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView2.DataSource = ds;
        }
    

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.ToUpper();
            textBox2.SelectionStart = textBox2.Text.Length;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        
            
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            ArrayList ortFiyat = new ArrayList();

            ArrayList bilesenKont = new ArrayList();
            ArrayList harf = new ArrayList();
            ArrayList sayi = new ArrayList();
            ArrayList veriTabani = new ArrayList();

            ArrayList deger = new ArrayList();
            ArrayList karakter = new ArrayList();
            ArrayList gec = new ArrayList();
            ArrayList hamisimleri = new ArrayList();
            ArrayList deger2 = new ArrayList();
            ArrayList miktar = new ArrayList();

            string bilesen;
            bilesen = textBox2.Text;

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select stokHammaddeAdi from ureticiStok ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                bilesenKont.Add(oku[0].ToString());

            }
            oku.Close();
            baglanti.Close();

            for (int i = 0; i < bilesenKont.ToArray().Length; i++)
            {
                string tmpdeger = (string)bilesenKont[i];
                veriTabani.Add(tmpdeger[0]);
            }


            bilesen = textBox2.Text;
            int a = 0, b = 0;
            if (!Char.IsNumber(bilesen[0]))
            {
                for (int i = 0; i < bilesen.Length; i++)
                {
                    if (!Char.IsNumber(bilesen[i]))
                    {
                        a++;
                        harf.Add(bilesen[i]);
                        if (a == 2 && b == 0) { sayi.Add("1"); b = 0; }

                    }
                    else
                    {
                        b++;
                        int sy = i; string tmp = "";
                        while (Char.IsNumber(bilesen[sy]))
                        {
                            tmp += bilesen[sy]; sy++;
                            if (sy >= bilesen.Length) break;
                        }
                        i = sy - 1;
                        sayi.Add(tmp);
                    }
                }
                if (harf.ToArray().Length != sayi.ToArray().Length)
                    sayi.Add("1");

            }
            else
            {
                MessageBox.Show("Bileşen ismini yanlış girdiniz! Kontrol ediniz.");
            }


            for (int i = 0; i < harf.ToArray().Length; i++)
            {
                if (veriTabani.IndexOf(harf[i]) != -1)
                {

                    karakter.Add(harf[i]);
                    gec.Add(i);
                    deger.Add(sayi[i]);

                }
            }


            try
            {
                for (int i = 0; i < bilesenKont.ToArray().Length; i++)
                {
                    string s = (string)bilesenKont[i];
                    for (int j = 0; j < karakter.ToArray().Length; j++)
                    {

                        if (s[0].ToString() == karakter[j].ToString())
                        {
                            hamisimleri.Add(bilesenKont[i]);
                            deger2.Add(deger[Convert.ToInt32(gec[j])]);

                        }

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata:", ex.Message);
            }

            baglanti.Open();
            for (int i = 0; i < hamisimleri.ToArray().Length; i++)
            {

                komut.Connection = baglanti;
                komut.CommandText = "select stokHammaddeMiktari,ortalamaAlisFiyati from ureticiStok where stokHammaddeAdi= '" + hamisimleri[i].ToString() + "' ";

                oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    miktar.Add(oku[0].ToString());
                    ortFiyat.Add(oku[1].ToString());

                }
                oku.Close();

            }
            baglanti.Close();

            for (int i = 0; i < deger2.ToArray().Length; i++)
            {
                textDoldur += Convert.ToInt32(Convert.ToInt32(ortFiyat[i]) * Convert.ToInt32(deger2[i]));
            }
            textDoldur += Convert.ToInt32(textBox5.Text);


            textBox6.Text = textDoldur.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox7.Text = (textDoldur + ((textDoldur * 20) / 100)).ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.ToUpper();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            textDoldur = 0;
        }
        satilanUrunler kar = new satilanUrunler();
        private void button5_Click(object sender, EventArgs e)
        {
            kar = new satilanUrunler();
            kar.Show();
        }
    }
}

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

namespace hammaddeUretim
{
   
    public partial class musteri : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");

        SqlCommand komut = new SqlCommand();
        SqlCommand komut2 = new SqlCommand();
        SqlDataReader oku;
        public musteri()
        {
            InitializeComponent();
        }

        private void musteri_Load(object sender, EventArgs e)
        {
           
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = "select distinct urunAdi from uretilenUrunler";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());
            }
            oku.Close();
            baglanti.Close();

            doldur();
            doldur2();
            comboBox1.SelectedIndex = 0;

        }
        void doldur()
        {

            string sorgu = "select urunID,urunAdi,bilesenleri,uretimTarihi,rafOmru,satisFiyati from uretilenUrunler";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
        }


        void doldur2()
        {

            string sorgu = "select musteriAdi,adres,urunAdi,bilesen,uretimTarihi,rafOmru,satisFiyati from musteri ";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView2.DataSource = ds;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

           
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string urun = "",bilesen="",iscilik="",maliyet="",satisfiyat="",rafomru="",uretimTarihi="";

            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = "select urunAdi,bilesenleri,iscilikMaliyeti,topMaliyet,satisFiyati,uretimTarihi,rafOmru from uretilenUrunler where urunID = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                urun=oku[0].ToString();
                bilesen = oku[1].ToString();
                iscilik = oku[2].ToString();
                maliyet = oku[3].ToString();
                satisfiyat = oku[4].ToString();
                uretimTarihi= oku[5].ToString();
                rafomru = oku[6].ToString();
            }
            oku.Close();
            baglanti.Close();

            // MessageBox.Show(urun + " " + bilesen + " " + iscilik + " " + maliyet + " " + satisfiyat);


            if (textBox1.Text != "" && textBox2.Text != "")
            {
                DialogResult sil;
                sil = MessageBox.Show("Ürünü Almak istediğinizden Eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (sil == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                {

                    baglanti.Open();
                    string Sorgu = "INSERT INTO musteri(musteriAdi,adres,urunAdi,bilesen,uretimTarihi,rafOmru,satisFiyati) values (@1,@2,@3,@4,@5,@6,@7)";
                    komut2 = new SqlCommand(Sorgu, baglanti);
                    komut2.Parameters.AddWithValue("@1", textBox1.Text);
                    komut2.Parameters.AddWithValue("@2", textBox2.Text);
                    komut2.Parameters.AddWithValue("@3", urun);
                    komut2.Parameters.AddWithValue("@4", bilesen);
                    komut2.Parameters.AddWithValue("@5", uretimTarihi);
                    komut2.Parameters.AddWithValue("@6", rafomru);
                    komut2.Parameters.AddWithValue("@7", satisfiyat);
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("Ürün Başarıyla Alındı.");
                    doldur2();
                    baglanti.Close();


                    baglanti.Open();
                    string satirSil = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string sorgu = "delete from uretilenUrunler where urunID=" + satirSil + " ";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.ExecuteNonQuery();
                    doldur();
                    baglanti.Close();


                    baglanti.Open();
                    string Sorgu2 = "INSERT INTO karTablosu(satilanUrun,satilanBilesen,iscilik,toplamMaliyet,satisFiyati,satisKari) values (@1,@2,@3,@4,@5,@6)";
                    komut2 = new SqlCommand(Sorgu2, baglanti);
                    komut2.Parameters.AddWithValue("@1", urun);
                    komut2.Parameters.AddWithValue("@2", bilesen);
                    komut2.Parameters.AddWithValue("@3", iscilik);
                    komut2.Parameters.AddWithValue("@4", maliyet);
                    komut2.Parameters.AddWithValue("@5", satisfiyat);
                    komut2.Parameters.AddWithValue("@6", ((Convert.ToInt32(satisfiyat) +Convert.ToInt32(iscilik)) - Convert.ToInt32(maliyet)));
                    komut2.ExecuteNonQuery();                   
                    baglanti.Close();



                }
                else
                {
                    MessageBox.Show("İşlem İptal edildi.");
                }
            }
            else MessageBox.Show("Boş geçilmemesi gereken Alanlar var.");



        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da;

            if(comboBox1.Text == "HEPSİ")
            {
                da = new SqlDataAdapter("select urunID,urunAdi,bilesenleri,uretimTarihi,rafOmru,satisFiyati from uretilenUrunler ", baglanti);
            }
            else
            {
                da = new SqlDataAdapter("select urunID,urunAdi,bilesenleri,uretimTarihi,rafOmru,satisFiyati from uretilenUrunler where urunAdi in ('" + comboBox1.Text + "') ", baglanti);
            }
           
            DataTable dt = new DataTable();
            baglanti.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}

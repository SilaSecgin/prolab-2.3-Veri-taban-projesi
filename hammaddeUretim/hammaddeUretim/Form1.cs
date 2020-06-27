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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        SqlCommand komut=new SqlCommand();
        SqlDataReader oku;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string tekrarfirmaAdi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string tekrarHammadde = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            baglanti.Open();
            if (comboBox1.Text != "")
            {
                if (tekrarfirmaAdi != comboBox1.Text || tekrarHammadde != textBox4.Text)
                {
                    if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
                    {
                        try
                        {

                            string sorgu = "INSERT INTO hammadde(hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati,firmaID) values (@1,@2,@3,@4,@5,@6)";
                            komut = new SqlCommand(sorgu, baglanti);
                            komut.Parameters.AddWithValue("@1", textBox4.Text);
                            komut.Parameters.AddWithValue("@2", textBox5.Text);
                            komut.Parameters.AddWithValue("@3", textBox6.Text);
                            komut.Parameters.AddWithValue("@4", textBox7.Text);
                            komut.Parameters.AddWithValue("@5", textBox8.Text);
                            komut.Parameters.AddWithValue("@6", textBox1.Text);
                            komut.ExecuteNonQuery();
                            doldur();
                            MessageBox.Show("İşlem Başarıyla Gerçekleştirildi.");
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Hata !" + ex.Message);
                        }


                    }
                    else
                        MessageBox.Show("Boş geçilmemesi gereken Alanlar var!! Tekrar Deneyin.");
                }
                else
                    MessageBox.Show("Seçilen Firmaya ait aynı Hammadde var ekleme Yapamazsınız Lütfen güncelleme yapın.");

               
            }
            else
                MessageBox.Show("Firma yı Boş geçemezsiniz !! Lütfen Firmayı seçin.");

               
        

            baglanti.Close();
        }

        void doldur()
        {
            
            string sorgu = "select hammaddeID,firmaAdi,ulke,sehir,hammaddeAdi,hammaddeMiktari,uretimTarihi,rafOmru,satisFiyati from firma inner join hammadde on firma.firmaID=hammadde.firmaID order by satisFiyati";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
           
            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = "select distinct(firmaAdi) from firma";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());
            }
            oku.Close();
            baglanti.Close();
            doldur();          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                     
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select firmaID from firma where firmaAdi = '" + comboBox1.Text + "' ";

            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textBox1.Text = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();

        }
        firmaEkle firma;
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            firma = new firmaEkle();
            firma.Show();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            comboBox1.Text= dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value.ToString();
           
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            try
            {
                DialogResult guncelle;
                guncelle = MessageBox.Show("Hammadde Bilgilerinizi Güncellemek İstediğinizden Eminmisiniz", "Uyarı ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (guncelle == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                {
                    string satirGuncelle = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    string sorgu = "Update hammadde set hammaddeAdi=@1,hammaddeMiktari=@2,uretimTarihi=@3,rafOmru=@4,satisFiyati=@5 where hammaddeID= " + satirGuncelle + " ";

                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@1", textBox4.Text);
                    komut.Parameters.AddWithValue("@2", textBox5.Text);
                    komut.Parameters.AddWithValue("@3", textBox6.Text);
                    komut.Parameters.AddWithValue("@4", textBox7.Text);
                    komut.Parameters.AddWithValue("@5", textBox8.Text);
                    komut.ExecuteNonQuery();
                    doldur();
                    MessageBox.Show(" Hammadde Bilgileri Başarı İle Güncellendi.");


                }
                else
                {
                    MessageBox.Show("Güncelleme İşlemi İptal Edildi ");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Güncelleme Hatası" + ex.Message);
            }

            baglanti.Close();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
           
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            DialogResult sil;
            sil = MessageBox.Show("Hammadde bilgileri Silinecektir Silmek istediğinizden Eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sil == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
            {
                string satirSil = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sorgu = "delete from hammadde where hammaddeID=" + satirSil + " ";
                komut = new SqlCommand(sorgu, baglanti);
                komut.ExecuteNonQuery();
                doldur();
                MessageBox.Show("Hammadde bilgileri Başarı İle Silindi.");

            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal edildi.");
            }
            baglanti.Close();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.ToUpper();
            textBox4.SelectionStart = textBox4.Text.Length;
        }
    }
}

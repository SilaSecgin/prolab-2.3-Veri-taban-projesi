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
    public partial class firmaEkle : Form
    {
        public firmaEkle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        SqlCommand komut;
        Form1 yeniTedarik;
        private void button1_Click(object sender, EventArgs e)
        {
            string tekrarfirmaAdi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string tekrarUlke = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string tekrarSehir = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            baglanti.Open();
            if (tekrarfirmaAdi != textBox1.Text || tekrarUlke != textBox2.Text || tekrarSehir != textBox3.Text)
            {
                if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "")
                {
                    try
                    {
                        string firmaSorgu = "INSERT INTO firma(firmaAdi,ulke,sehir,kargoID) values (@1,@2,@3,@4)";
                        komut = new SqlCommand(firmaSorgu, baglanti);
                        komut.Parameters.AddWithValue("@1", textBox1.Text);
                        komut.Parameters.AddWithValue("@2", textBox2.Text);
                        komut.Parameters.AddWithValue("@3", textBox3.Text);
                        if (textBox2.Text == "Türkiye") komut.Parameters.AddWithValue("@4", "1");
                        else komut.Parameters.AddWithValue("@4", "2");
                        komut.ExecuteNonQuery();
                        firmaDoldur();
                        MessageBox.Show("Firma Başarıyla Eklendi.");

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("Aynı firmadan var!! Tekrar deneyin.");
          
            baglanti.Close();

            this.Close();
            yeniTedarik = new Form1();
            yeniTedarik.Show();

        }

      public void firmaDoldur()
        {
            string dgvsorgu = "Select firmaID,firmaAdi,ulke,sehir from firma";
            SqlDataAdapter da = new SqlDataAdapter(dgvsorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "firma");
            dataGridView1.DataSource = ds.Tables[0];

            

        }

        private void firmaEkle_Load(object sender, EventArgs e)
        {
            
            firmaDoldur();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();


            try
            {
                DialogResult guncelle;
                guncelle = MessageBox.Show("Firma Bilgilerinizi Güncellemek İstediğinizden Eminmisiniz", "Uyarı ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (guncelle == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                {
                    string satirGuncelle = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    string sorgu = "Update firma set firmaAdi=@1,ulke=@2,sehir=@3,kargoID=@4 where firmaID= " + satirGuncelle + " ";

                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@1", textBox1.Text);
                    komut.Parameters.AddWithValue("@2", textBox2.Text);
                    komut.Parameters.AddWithValue("@3", textBox3.Text);
                    if (textBox2.Text == "Türkiye") komut.Parameters.AddWithValue("@4", "1");
                    else komut.Parameters.AddWithValue("@4", "2");

                    komut.ExecuteNonQuery();
                    firmaDoldur();
                    MessageBox.Show(" Firma Bilgileri Başarı İle Güncellendi.");


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

            this.Close();
            yeniTedarik = new Form1();
            yeniTedarik.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            DialogResult sil;
            sil = MessageBox.Show("Firma Silinecektir Silmek istediğinizden Eminmisiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (sil == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
            {
                string satirSil = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string sorgu = "delete from firma where firmaID=" + satirSil + " ";
                komut = new SqlCommand(sorgu, baglanti);
                komut.ExecuteNonQuery();
                firmaDoldur();
                MessageBox.Show("Firma Başarı İle Silindi.");

            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal edildi.");
            }
            baglanti.Close();

            this.Close();
            yeniTedarik = new Form1();
            yeniTedarik.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            yeniTedarik = new Form1();
            yeniTedarik.Show();
        }
    }
}

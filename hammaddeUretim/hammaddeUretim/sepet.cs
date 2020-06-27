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
    public partial class sepet : Form
    {
        public sepet()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        SqlCommand komut = new SqlCommand();
        SqlDataReader oku;
        
      
        private void sepet_Load(object sender, EventArgs e)
        {          
           sepetDoldur();
            sepetDoldur2();


            komut.Connection = baglanti;
            baglanti.Open();
            komut.CommandText = "select sum(alisMaliyeti) from sepet";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                label4.Text = oku[0].ToString();
            }
            oku.Close();
            baglanti.Close();

        }


        public void sepetDoldur()
        {

            string sorgu = "SELECT firmaAdi,sehir,hammaddeAdi,alisMaliyeti,stokDurumu FROM firma INNER JOIN hammadde ON firma.firmaID = hammadde.firmaID INNER JOIN sepet ON hammadde.hammaddeID = sepet.hammaddeID";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
        }
       
        public void sepetDoldur2()
        {

            string sorgu = "SELECT stokHammaddeAdi,stokHammaddeMiktari,topAlisFiyati,ortalamaAlisFiyati from ureticiStok";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);   
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView2.DataSource = ds;
        }


     
    }
}

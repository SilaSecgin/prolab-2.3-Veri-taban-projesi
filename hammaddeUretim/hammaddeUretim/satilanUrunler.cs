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
    public partial class satilanUrunler : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.; initial Catalog=ureticiVT; Integrated Security=true");
        public satilanUrunler()
        {
            InitializeComponent();
        }

        private void satilanUrunler_Load(object sender, EventArgs e)
        {
            string sorgu = "select * from karTablosu";
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
        }
    }
}

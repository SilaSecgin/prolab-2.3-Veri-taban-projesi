using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hammaddeUretim
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        Form1 tedarikci = new Form1();
        uretici uretim = new uretici();
        musteri mus = new musteri();
        private void button1_Click(object sender, EventArgs e)
        {
            tedarikci = new Form1();
            tedarikci.Show();
            uretim.Close();
            mus.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uretim = new uretici();
            uretim.Show();
            tedarikci.Close();
            mus.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mus = new musteri();
            mus.Show();
            uretim.Close();
            tedarikci.Close();

        }
    }
}

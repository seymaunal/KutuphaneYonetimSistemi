using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneSistemi
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kitap_Kayit fr = new Kitap_Kayit();
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeKayit fr = new UyeKayit();
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OduncKitap fr = new OduncKitap();
            fr.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UyeListeleme fr = new UyeListeleme();
            fr.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KitapListeleme fr = new KitapListeleme();
            fr.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OduncKitapListeleme fr = new OduncKitapListeleme();
            fr.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OduncKitapİade fr = new OduncKitapİade();
            fr.Show();
            this.Hide();
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }

       
    }
}

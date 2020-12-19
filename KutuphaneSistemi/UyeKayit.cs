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

namespace KutuphaneSistemi
{
    public partial class UyeKayit : Form
    {
        public UyeKayit()
        {
            InitializeComponent();
        }

       
        sqlbaglantisi bgl = new sqlbaglantisi();

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text!="")
            {
                SqlCommand komut = new SqlCommand("insert into uyekayit(tc,adsoyad,dogumtarihi,telefon,email)values(@tc,@adsoyad,@dogumtarihi,@telefon,@email)", bgl.baglanti());
                komut.Parameters.AddWithValue("@tc", textBox1.Text);
                komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                komut.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@telefon", textBox4.Text);
                komut.Parameters.AddWithValue("@email", textBox5.Text);

                komut.ExecuteNonQuery();

                MessageBox.Show("Üye Kayıt İşlemi Yapıldı.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //kayıt işleminde sonra textlerin içinin temizlenmesi
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Üye Bilgilerini Eksiksiz Doldurmalısınız'", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //iptal butonu
        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }

        private void UyeKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
